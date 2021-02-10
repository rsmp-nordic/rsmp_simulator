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
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;

namespace nsRSMPGS
{

    public class cTcpSocket
    {

        public int WrapMethod;
        public int PacketTimeout;

        public bool ThreadShouldRun = true;

        public int ConnectionMethod;

        public const int ConnectionMethod_SocketServer = 0;
        public const int ConnectionMethod_SocketClient = 1;

        cTcpSocketServer TcpSocketServer = null;
        cTcpSocketClient TcpSocketClient = null;

        public const int ConnectionStatus_Unknown = -1;
        public const int ConnectionStatus_Disconnected = 0;
        public const int ConnectionStatus_Connecting = 1;
        public const int ConnectionStatus_Connected = 2;

        public cTcpSocket(int iConnectionType, string sServerAddress, int iReconnectInterval,
            bool bConnectAutomatically, int iPortNumber, int iPacketTimeout, int iWrapMethod)
        {

            ConnectionMethod = iConnectionType;
            WrapMethod = iWrapMethod;
            PacketTimeout = iPacketTimeout;

            switch (ConnectionMethod)
            {
                case ConnectionMethod_SocketServer:
                    TcpSocketServer = new cTcpSocketServer(this, iPortNumber);
                    break;
                case ConnectionMethod_SocketClient:
                    TcpSocketClient = new cTcpSocketClient(this, sServerAddress, iReconnectInterval, bConnectAutomatically);
                    break;
                default:
                    break;
            }

        }

        public void Shutdown()
        {
            ThreadShouldRun = false;
            switch (ConnectionMethod)
            {
                case ConnectionMethod_SocketServer:
                    TcpSocketServer.Shutdown();
                    break;
                case ConnectionMethod_SocketClient:
                    TcpSocketClient.Shutdown();
                    break;
            }
        }

        public void Connect()
        {

            switch (ConnectionMethod)
            {
                case ConnectionMethod_SocketClient:
                    TcpSocketClient.Connect();
                    break;
            }
        }

        public void SetConnectBehaviour(bool bConnectAutomatically)
        {
            switch (ConnectionMethod)
            {
                case ConnectionMethod_SocketClient:
                    TcpSocketClient.SetConnectBehaviour(bConnectAutomatically);
                    break;
            }
        }

        public void NagleAlgorithm(bool bDisable)
        {
            switch (ConnectionMethod)
            {
                case ConnectionMethod_SocketServer:
                    TcpSocketServer.NagleAlgorithm(bDisable);
                    break;
                case ConnectionMethod_SocketClient:
                    TcpSocketClient.NagleAlgorithm(bDisable);
                    break;
            }
        }

        public void Disconnect()
        {

            switch (ConnectionMethod)
            {
                case ConnectionMethod_SocketServer:
                    TcpSocketServer.Disconnect();
                    break;
                case ConnectionMethod_SocketClient:
                    TcpSocketClient.Disconnect();
                    break;
            }
        }

        public int ConnectionStatus()
        {
            int iConnectionStatus = cTcpSocket.ConnectionStatus_Unknown;
            switch (ConnectionMethod)
            {
                case ConnectionMethod_SocketServer:
                    iConnectionStatus = TcpSocketServer.ConnectionStatus();
                    break;
                case ConnectionMethod_SocketClient:
                    iConnectionStatus = TcpSocketClient.ConnectionStatus();
                    break;
            }
            return iConnectionStatus;
        }

        public string ListenPort()
        {
            string sListenPort = "(unknown)";
            switch (ConnectionMethod)
            {
                case ConnectionMethod_SocketServer:
                    sListenPort = TcpSocketServer.ListenPort.ToString();
                    break;
            }
            return sListenPort;
        }

        public string RemoteServerOrClientIP()
        {
            string sRemoteServerOrClientIP = "";
            switch (ConnectionMethod)
            {
                case ConnectionMethod_SocketServer:
                    sRemoteServerOrClientIP = TcpSocketServer.RemoteClient();
                    break;
                case ConnectionMethod_SocketClient:
                    sRemoteServerOrClientIP = TcpSocketClient.RemoteHost();
                    break;
            }
            return sRemoteServerOrClientIP;
        }

        public bool SendJSonPacket(string PacketType, string MessageId, string SendString)
        {

            bool bSendJSonPacket = false;

            if (RSMPGS.JSon.bInitialNegotiationIsFinished == false)
            {
                if (cHelper.IsSettingChecked("DropBytesInNegotiationPackets10"))
                {
                    Random rnd = new Random();

                    int iCharactersToRemove = SendString.Length / 10;

                    for (int iCount = 0; iCount < iCharactersToRemove && SendString.Length > 0; iCount++)
                    {
                        SendString = SendString.Remove(rnd.Next(0, SendString.Length - 1), 1);
                    }
                }
            }

            lock (this)
            {
                switch (ConnectionMethod)
                {
                    case ConnectionMethod_SocketServer:
                        bSendJSonPacket = TcpSocketServer.SendJSonPacket(PacketType, MessageId, SendString);
                        break;
                    case ConnectionMethod_SocketClient:
                        bSendJSonPacket = TcpSocketClient.SendJSonPacket(PacketType, MessageId, SendString);
                        break;
                }
            }
            return bSendJSonPacket;
        }

        public bool SendRawString(string sSendRawString)
        {
            bool bSendRawString = false;
            lock (this)
            {
                switch (ConnectionMethod)
                {
                    case ConnectionMethod_SocketServer:
                        bSendRawString = TcpSocketServer.SendRawString(sSendRawString);
                        break;
                    case ConnectionMethod_SocketClient:
                        bSendRawString = TcpSocketClient.SendRawString(sSendRawString);
                        break;
                }
            }
            return bSendRawString;
        }

        public bool ReadBytesAndParsePacket(ref cSocketStream socketStream, ref TcpClient tcpClient, ref byte[] inBuffer, ref int inBufferLength)
        {

            string sJSon;
            int iPacketLength;

            bool bSuccess = true;

            // Read the server message into a byte buffer
            int iReadBytes = socketStream.Read(inBuffer, inBufferLength, inBuffer.GetLength(0) - inBufferLength);

            if (iReadBytes <= 0)
            {
                return false;
            }

            RSMPGS.SysLog.AddRawDebugData(true, cSysLogAndDebug.Direction_In, false, inBuffer, inBufferLength, iReadBytes);

            //TimeoutTimer = 0;
            inBufferLength += iReadBytes;

            switch (WrapMethod)
            {
                case cTcpHelper.WrapMethod_None:

                    sJSon = Encoding.UTF8.GetString(inBuffer, 0, inBufferLength);
                    RSMPGS.MainForm.BeginInvoke(RSMPGS.MainForm.DelegateDecodeJSonPacket, new Object[] { sJSon });
                    inBufferLength = 0;
                    break;

                case cTcpHelper.WrapMethod_LengthPrefix:

                    // At least we need to have the length bytes
                    while (inBufferLength >= 4)
                    {
                        // Determine packet length
                        iPacketLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(inBuffer, 0));

                        // Hopefully not too big packet (or something went very wrong)
                        if (iPacketLength >= (inBuffer.GetLength(0) - 4))
                        {
                            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Packet size was reported to {0} bytes, but could be no more than {1} bytes", iPacketLength, inBuffer.GetLength(0) - 4);
                            bSuccess = false;
                        }
                        else
                        {
                            // Got enough of bytes?
                            if ((iPacketLength + 4) <= inBufferLength)
                            {
                                sJSon = Encoding.UTF8.GetString(inBuffer, 4, iPacketLength);
                                RSMPGS.MainForm.BeginInvoke(RSMPGS.MainForm.DelegateDecodeJSonPacket, new Object[] { sJSon });
                                RSMPGS.Statistics["RxPackets"]++;
                                RSMPGS.Statistics["RxBytes"] += iPacketLength + 4;
                            }
                            else
                            {
                                // Did not get enough, wait for more
                                break;
                            }
                        }
                        // Some more, do some buffer byte move or just clear the length
                        inBufferLength -= 4;
                        inBufferLength -= iPacketLength;
                        if (inBufferLength > 0)
                        {
                            Buffer.BlockCopy(inBuffer, iPacketLength + 4, inBuffer, 0, inBufferLength);
                        }
                    }
                    break;

                case cTcpHelper.WrapMethod_FormFeed:

                    // Find LF's, repeat until no more
                    bool bFoundFormFeed;

                    do
                    {
                        bFoundFormFeed = false;
                        for (iPacketLength = 0; iPacketLength < inBufferLength; iPacketLength++)
                        {
                            if (inBuffer[iPacketLength].CompareTo(0x0c) == 0)
                            {
                                sJSon = Encoding.UTF8.GetString(inBuffer, 0, iPacketLength);
                                RSMPGS.MainForm.BeginInvoke(RSMPGS.MainForm.DelegateDecodeJSonPacket, new Object[] { sJSon });
                                iPacketLength++;
                                inBufferLength -= iPacketLength;
                                if (inBufferLength > 0)
                                {
                                    Buffer.BlockCopy(inBuffer, iPacketLength, inBuffer, 0, inBufferLength);
                                }
                                RSMPGS.Statistics["RxPackets"]++;
                                RSMPGS.Statistics["RxBytes"] += iPacketLength;
                                bFoundFormFeed = true;
                                break;
                            }
                        }
                    } while (bFoundFormFeed == true && inBufferLength > 0);

                    break;
            }

            if (inBuffer.GetLength(0) == inBufferLength)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Packet size was too big (> {0} bytes)", inBuffer.GetLength(0));
                bSuccess = false;
            }

            return bSuccess;

        }

    }
}