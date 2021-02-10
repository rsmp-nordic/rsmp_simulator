using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Security.Authentication;

namespace nsRSMPGS
{

    public class cTcpSocketClient
    {

        public int ReconnectInterval;
        public bool ConnectAutomatically;

        public cTcpSocket TcpSocket;

        public TcpClient tcpClient = null;

        private cTcpSocketClientThread ClientThread;

        public IPAddress ServerAddress = null;
        public int ServerPort;
        public bool PleaseTryToConnect = false;

        public bool ThreadIsRunning = false;

        public cTcpSocketClient(cTcpSocket tcpSocket, string sServerAddress, int iReconnectInterval, bool bConnectAutomatically)
        {

            TcpSocket = tcpSocket;
            ReconnectInterval = iReconnectInterval;
            ConnectAutomatically = bConnectAutomatically;

            try
            {

                IPAddress[] ips;
                ips = Dns.GetHostAddresses(cHelper.Item(sServerAddress, 0, ':'));

                // Lookup some IPv4 address (fails in Windows 7 otherwise)
                foreach (IPAddress ip in ips)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ServerAddress = ip;
                        break;
                    }
                }

                /*
                    IPHostEntry host = Dns.GetHostEntry(cHelper.Item(sServerAddress, 0, ':'));

                    // Lookup some IPv4 address (fails in Windows 7 otherwise)
                    foreach (IPAddress ip in host.AddressList)
                    {
                      if (ip.AddressFamily == AddressFamily.InterNetwork)
                      {
                        ServerAddress = ip;
                  break;
                      }
                    }
                */

                ServerPort = Int32.Parse(cHelper.Item(sServerAddress, 1, ':'));
                if (ServerPort > 0 && ServerPort < 65536 && ServerAddress != null)
                {
                    ThreadIsRunning = true;
                    ClientThread = new cTcpSocketClientThread(this);
                    PleaseTryToConnect = ConnectAutomatically;
                    new Thread(new ThreadStart(ClientThread.RunThread)).Start();
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Created connection thread for server " + sServerAddress);
                }
            }
            catch (SocketException se)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not create client thread: " + se.Message);
            }

        }

        ~cTcpSocketClient()
        {
            Shutdown();
        }

        public void Shutdown()
        {

            Disconnect();

            while (ThreadIsRunning)
            {
                Thread.Sleep(10);
            }
        }

        public void Connect()
        {
            PleaseTryToConnect = true;
        }

        public void SetConnectBehaviour(bool bConnectAutomatically)
        {
            ConnectAutomatically = bConnectAutomatically;
        }

        public void NagleAlgorithm(bool bDisable)
        {
            lock (this)
            {
                if (ClientThread != null)
                {
                    if (ClientThread.tcpClient != null)
                    {
                        if (ClientThread.socketStream != null)
                        {
                            ClientThread.tcpClient.NoDelay = bDisable;
                        }
                    }
                }
            }
        }

        public void Disconnect()
        {
            lock (this)
            {
                PleaseTryToConnect = false;
                if (cTcpHelper.CloseAndDeleteStreamAndSocket(ref ClientThread.socketStream, ref ClientThread.tcpClient))
                {
                    RSMPGS.MainForm.BeginInvoke(RSMPGS.MainForm.DelegateSocketWasClosed);
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "We disconnected from RSMP client");
                }
            }
        }

        public int ConnectionStatus()
        {
            int iConnectionStatus = cTcpSocket.ConnectionStatus_Unknown;

            lock (this)
            {
                if (ClientThread.tcpClient == null)
                {
                    iConnectionStatus = cTcpSocket.ConnectionStatus_Disconnected;
                }
                else
                {
                    if (ClientThread.tcpClient.Connected)
                    {
                        if (ClientThread.socketStream != null)
                        {
                            if (ClientThread.socketStream.IsConnected())
                            {
                                iConnectionStatus = cTcpSocket.ConnectionStatus_Connected;
                            }
                            else
                            {
                                iConnectionStatus = cTcpSocket.ConnectionStatus_Connecting;
                            }
                        }
                        else
                        {
                            iConnectionStatus = cTcpSocket.ConnectionStatus_Connecting;
                        }
                    }
                    else
                    {
                        iConnectionStatus = cTcpSocket.ConnectionStatus_Connecting;
                    }
                }
            }
            return iConnectionStatus;
        }

        public string RemoteHost()
        {
            if (ClientThread != null && (ConnectionStatus() == cTcpSocket.ConnectionStatus_Connected || ConnectionStatus() == cTcpSocket.ConnectionStatus_Connecting))
            {
                return ClientThread.sClientIP;
            }
            else
            {
                return "";
            }
        }

        public bool SendJSonPacket(string PacketType, string MessageId, string SendString)
        {

            bool bSuccess = false;

            lock (this)
            {
                if (ClientThread != null)
                {
                    if (ClientThread.tcpClient != null)
                    {
                        if (ClientThread.socketStream != null)
                        {
                            RSMPGS.SysLog.AddJSonDebugData(cSysLogAndDebug.Direction_Out, PacketType, SendString);
                            bSuccess = ClientThread.SendString(SendString);
                        }
                    }
                }
            }
            return bSuccess;
        }

        public bool SendRawString(string sSendRawString)
        {

            bool bSuccess = false;

            lock (this)
            {
                if (ClientThread != null)
                {
                    if (ClientThread.tcpClient != null)
                    {
                        if (ClientThread.socketStream != null)
                        {
                            bSuccess = ClientThread.SendString(sSendRawString);
                        }
                    }
                }
            }
            return bSuccess;
        }


    }

    //
    // Connect and client thread
    //
    class cTcpSocketClientThread
    {

        public const int BUFLENGTH = 2048000;

        public cTcpSocketClient SocketClient;

        public TcpClient tcpClient;
        public cSocketStream socketStream;

        byte[] inBuffer = new byte[BUFLENGTH];
        int inBufferLength;

        //    public int TimeoutTimer;
        public int KeepAliveTimer;

        public string sClientIP;

        public Encoding encoding = Encoding.Default;
        //public 

        public cTcpSocketClientThread(cTcpSocketClient tssSocketClient)
        {
            //
            // Remember our parent object
            //
            SocketClient = tssSocketClient;
            encoding = Encoding.GetEncoding("Windows-1252");
        }

        public bool SendString(string SendString)
        {

            bool bSuccess = false;

            lock (this)
            {
                if (socketStream != null)
                {
                    try
                    {
                        byte[] SendBytes = Encoding.UTF8.GetBytes(SendString);
                        byte[] bFormFeed = new byte[] { (byte)0x0c };

                        RSMPGS.Statistics["TxPackets"]++;
                        RSMPGS.Statistics["TxBytes"] += SendBytes.GetLength(0);

                        switch (SocketClient.TcpSocket.WrapMethod)
                        {
                            case cTcpHelper.WrapMethod_None:
                                //                RSMPGS.SysLog.AddRawDebugData(true, cSysLogAndDebug.Direction_Out, false, SendBytes, 0, SendBytes.GetLength(0));
                                SendByteArray(SendBytes, 0, SendBytes.GetLength(0), true, false);
                                break;
                            case cTcpHelper.WrapMethod_LengthPrefix:
                                // Determine packet length
                                byte[] PacketLength = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(SendBytes.GetLength(0)));
                                SendByteArray(PacketLength, 0, PacketLength.GetLength(0), true, true);
                                SendByteArray(SendBytes, 0, SendBytes.GetLength(0), false, false);
                                RSMPGS.Statistics["TxBytes"] += PacketLength.GetLength(0);
                                break;
                            case cTcpHelper.WrapMethod_FormFeed:
                                SendByteArray(SendBytes, 0, SendBytes.GetLength(0), true, false);
                                SendByteArray(bFormFeed, 0, 1, false, false);
                                RSMPGS.Statistics["TxBytes"] += 1;
                                break;
                        }
                        bSuccess = true;
                    }
                    catch (Exception e)
                    {
                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not send data: " + e.Message);
                        SocketClient.Disconnect();
                    }
                }
            }

            return bSuccess;

        }

        public bool SendByteArray(byte[] SendBuffer, int iOffset, int iLength, bool bNewPacket, bool bForceHexCode)
        {

            bool bSendSplitPackets = RSMPGS.MainForm.ToolStripMenuItem_SplitPackets.Checked;

            int iSendLength;

            try
            {
                if (bSendSplitPackets)
                {
                    Random Rnd = new Random();
                    while (iLength > 0)
                    {
                        iSendLength = Rnd.Next(1, 10);
                        if (iSendLength > iLength)
                        {
                            iSendLength = iLength;
                        }
                        RSMPGS.SysLog.AddRawDebugData(bNewPacket, cSysLogAndDebug.Direction_Out, bForceHexCode, SendBuffer, iOffset, iSendLength);
                        socketStream.Write(SendBuffer, iOffset, iSendLength);
                        iOffset += iSendLength;
                        iLength -= iSendLength;
                        bNewPacket = false;
                        Thread.Sleep(10);
                    }
                }
                else
                {
                    RSMPGS.SysLog.AddRawDebugData(bNewPacket, cSysLogAndDebug.Direction_Out, bForceHexCode, SendBuffer, iOffset, iLength);
                    socketStream.Write(SendBuffer, iOffset, iLength);
                }
                return true;
            }
            catch (SocketException e)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not send data: " + e.Message);
                SocketClient.Disconnect();
                return false;
            }
            catch (Exception e)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not send data: " + e.Message);
                SocketClient.Disconnect();
                return false;
            }
        }

        public void RunThread()
        {

            sClientIP = SocketClient.ServerAddress.ToString() + ":" + SocketClient.ServerPort.ToString();

            while (SocketClient.TcpSocket.ThreadShouldRun)
            {
                if (SocketClient.PleaseTryToConnect)
                {
                    ConnectToServerAndMaintainStream();
                    SocketClient.PleaseTryToConnect = false;
                }

                // Wait before we try to reconnect
                for (int iDelay = 0; iDelay < SocketClient.ReconnectInterval && SocketClient.TcpSocket.ThreadShouldRun == true && SocketClient.PleaseTryToConnect == false; iDelay += 100)
                {
                    Thread.Sleep(100);
                    if (SocketClient.ConnectAutomatically == false)
                    {
                        iDelay = 0;
                    }
                }
                if (SocketClient.ConnectAutomatically)
                {
                    SocketClient.PleaseTryToConnect = true;
                }
            }

            SocketClient.Disconnect();

            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Client socket was closed (" + sClientIP + ")");

            lock (SocketClient)
            {
                SocketClient.ThreadIsRunning = false;
            }

        }

        private void ConnectToServerAndMaintainStream()
        {

            try
            {
                tcpClient = new TcpClient();
                try
                {
                    tcpClient.Connect(SocketClient.ServerAddress, SocketClient.ServerPort);
                }
                catch (Exception e)
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not connect (" + sClientIP + "): " + e.Message);
                    SocketClient.Disconnect();
                    return;
                }

                KeepAliveTimer = 0;

                inBufferLength = 0;
                socketStream = new nsRSMPGS.cSocketStream(cHelper.IsSettingChecked("UseEncryption"));

                if (socketStream.InitializeAsClientAndGetStream(tcpClient, RSMPGS.EncryptionSettings))
                {

                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "We connected to RSMP client " + sClientIP);

                    tcpClient.NoDelay = RSMPGS.MainForm.ToolStripMenuItem_DisableNagleAlgorithm.Checked;

                    RSMPGS.MainForm.BeginInvoke(RSMPGS.MainForm.DelegateSocketWasConnected);

                    while (SocketClient.TcpSocket.ThreadShouldRun)
                    {
                        if (RSMPGS.RSMPConnection.ReadBytesAndParsePacket(ref socketStream, ref tcpClient, ref inBuffer, ref inBufferLength) == false)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (socketStream != null)
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Some problem occured (" + sClientIP + "), closed socket: " + e.Message);
                }
            }
            SocketClient.Disconnect();

        }

    }

}