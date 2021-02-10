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

namespace nsRSMPGS
{

    public class cTcpSocketServer
    {

        public cTcpSocket TcpSocket;

        public int ListenPort;

        public ArrayList ServerSockets = new ArrayList();

        public TcpListener listener = null;

        public cTcpSocketServer(cTcpSocket tcpSocket, int iListenPort)
        {
            TcpSocket = tcpSocket;
            ListenPort = iListenPort;


            if (iListenPort < 1 || iListenPort > 65535)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Portnumber is invalid");
            }
            else
            {
                try
                {
                    listener = new TcpListener(IPAddress.Any, ListenPort);
                    listener.Start();
                    lock (this)
                    {
                        cTcpSocketServerThread ServerThread = new cTcpSocketServerThread(this);
                        new Thread(new ThreadStart(ServerThread.RunThread)).Start();
                        ServerSockets.Add(ServerThread);
                    }
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Waiting for connection at port " + ListenPort.ToString());
                }
                catch (SocketException se)
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not create socket server thread: " + se.Message);
                }
            }
        }

        ~cTcpSocketServer()
        {
            Shutdown();
        }

        public void Shutdown()
        {

            bool SomeThreadIsStillRunning = true;

            lock (this)
            {

                if (listener != null)
                {
                    listener.Stop();
                }

                foreach (cTcpSocketServerThread SocketServerThread in ServerSockets)
                {
                    Disconnect();
                }
            }

            while (SomeThreadIsStillRunning)
            {
                lock (this)
                {
                    SomeThreadIsStillRunning = ServerSockets.Count > 0;
                }
                Thread.Sleep(10);
            }
        }

        public bool SendJSonPacket(string PacketType, string MessageId, string SendString)
        {

            bool bSuccess = false;

            lock (this)
            {
                foreach (cTcpSocketServerThread SocketServerThread in ServerSockets)
                {
                    if (SocketServerThread.tcpClient != null)
                    {
                        if (SocketServerThread.socketStream != null)
                        {
                            RSMPGS.SysLog.AddJSonDebugData(cSysLogAndDebug.Direction_Out, PacketType, SendString);
                            bSuccess = SocketServerThread.SendString(SendString);
                        }
                    }
                }
            }
            return bSuccess;
        }

        public void NagleAlgorithm(bool bDisable)
        {
            lock (this)
            {
                foreach (cTcpSocketServerThread SocketServerThread in ServerSockets)
                {
                    if (SocketServerThread.tcpClient != null)
                    {
                        SocketServerThread.tcpClient.NoDelay = bDisable;
                    }
                }
            }
        }

        public void Disconnect()
        {
            lock (this)
            {

                foreach (cTcpSocketServerThread SocketServerThread in ServerSockets)
                {
                    if (cTcpHelper.CloseAndDeleteStreamAndSocket(ref SocketServerThread.socketStream, ref SocketServerThread.tcpClient))
                    {
                        RSMPGS.MainForm.BeginInvoke(RSMPGS.MainForm.DelegateSocketWasClosed);
                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "We disconnected from RSMP client");
                    }
                }
            }
        }

        public bool SendRawString(string sSendRawString)
        {
            bool bSuccess = false;
            lock (this)
            {
                foreach (cTcpSocketServerThread SocketServerThread in ServerSockets)
                {
                    bSuccess = SocketServerThread.SendString(sSendRawString);
                }
            }
            return bSuccess;
        }

        public int ConnectionStatus()
        {
            int iConnectionStatus = cTcpSocket.ConnectionStatus_Unknown;

            lock (this)
            {
                if (ServerSockets.Count > 0)
                {
                    cTcpSocketServerThread SocketServerThread = (cTcpSocketServerThread)ServerSockets[0];
                    if (SocketServerThread.tcpClient == null)
                    {
                        iConnectionStatus = cTcpSocket.ConnectionStatus_Disconnected;
                    }
                    else
                    {
                        if (SocketServerThread.tcpClient.Connected)
                        {
                            if (SocketServerThread.socketStream != null)
                            {
                                if (SocketServerThread.socketStream.IsConnected())
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
            }
            return iConnectionStatus;
        }

        public string RemoteClient()
        {
            string sRemoteClient = "";
            if (ServerSockets.Count > 0)
            {
                cTcpSocketServerThread SocketServerThread = (cTcpSocketServerThread)ServerSockets[0];
                if (ConnectionStatus() == cTcpSocket.ConnectionStatus_Connected || ConnectionStatus() == cTcpSocket.ConnectionStatus_Connecting)
                {
                    sRemoteClient = SocketServerThread.sClientIP;
                }
            }
            return sRemoteClient;
        }
    }

    //
    // Listen and server thread
    //
    // Creates a new listen thread if we get a connection
    //
    class cTcpSocketServerThread
    {

        private const int BUFLENGTH = 2048000;

        public cTcpSocketServer SocketServer;

        public TcpClient tcpClient;
        public cSocketStream socketStream;

        //		public int TimeoutTimer;
        public int KeepAliveTimer;

        byte[] inBuffer = new byte[BUFLENGTH];
        int inBufferLength;

        public Encoding encoding;

        public string sClientIP = "";

        public cTcpSocketServerThread(cTcpSocketServer tssSocketServer)
        {
            //
            // Remember our parent object
            //
            SocketServer = tssSocketServer;
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

                        switch (SocketServer.TcpSocket.WrapMethod)
                        {
                            case cTcpHelper.WrapMethod_None:
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
                        SocketServer.Disconnect();
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
            catch (Exception e)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not send data: " + e.Message);
                SocketServer.Disconnect();
                return false;
            }
        }

        public void RunThread()
        {

            try
            {

                tcpClient = SocketServer.listener.AcceptTcpClient();

                lock (SocketServer)
                {

                    KeepAliveTimer = 0;

                    socketStream = new nsRSMPGS.cSocketStream(cHelper.IsSettingChecked("UseEncryption"));

                    cTcpSocketServerThread ServerThread = new cTcpSocketServerThread(SocketServer);
                    new Thread(new ThreadStart(ServerThread.RunThread)).Start();
                    SocketServer.ServerSockets.Add(ServerThread);
                }

                if (socketStream.InitializeAsServerAndGetStream(tcpClient, RSMPGS.EncryptionSettings))
                {

                    encoding = Encoding.GetEncoding("Windows-1252");

                    sClientIP = ((IPEndPoint)(tcpClient.Client.RemoteEndPoint)).Address.ToString() + ":" + ((IPEndPoint)(tcpClient.Client.RemoteEndPoint)).Port.ToString();

                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Client connection was accepted " + sClientIP);

                    tcpClient.NoDelay = RSMPGS.MainForm.ToolStripMenuItem_DisableNagleAlgorithm.Checked;

                    RSMPGS.MainForm.BeginInvoke(RSMPGS.MainForm.DelegateSocketWasConnected);

                    while (SocketServer.TcpSocket.ThreadShouldRun)
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

            SocketServer.Disconnect();

            if (sClientIP.Length > 0)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Client connection " + sClientIP + " was closed");
            }

            lock (SocketServer)
            {
                SocketServer.ServerSockets.Remove(this);
            }

        }
    }
}

