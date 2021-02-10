using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace nsRSMPGS
{

    public class cEncryptionSettings
    {
        public SslProtocols sslProtocols = SslProtocols.Default; // Should be .None in .NET 4.7
        public bool CheckCertificateRevocationList = false;
        public bool IgnoreCertificateErrors = false;

        //#if _RSMPGS1
        public string ServerName = "";
        public string ClientCertificateFile = "";
        public string ClientCertificateFilePassword = "";
        public bool AuthenticateAsClientUsingCertificate = false;
        //#endif

        //#if _RSMPGS2
        public bool RequireClientCertificate = false;
        public string ServerCertificateFile = "";
        public string ServerCertificateFilePassword = "";
        //#endif

    }

    public class cSocketStream
    {

        private bool UseEncryption = false;

        private Object lockObject = new object();
        private NetworkStream networkStream = null;
        private SslStream sslStream = null;

        private bool IgnoreCertificateErrors = false;
        private X509Certificate2Collection ClientCertificates = new X509Certificate2Collection();

        private bool bSSLStreamIsOk = false;

        public cSocketStream(bool UseEncryption)
        {
            this.UseEncryption = UseEncryption;
        }

        public bool IsConnected()
        {
            lock (lockObject)
            {
                if (networkStream != null)
                {
                    return true;
                }
                if (sslStream != null)
                {
                    if (bSSLStreamIsOk)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public X509Certificate2 CreateCertificate(string sCertificateFile, string sPassword)
        {

            try
            {
                X509Certificate2 certificate = new X509Certificate2(sCertificateFile, sPassword);
                return certificate;
            }
            catch (Exception e)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not parse certificate file, error: {0}", e.ToString());
                return null;
            }

        }

        public bool InitializeAsClientAndGetStream(TcpClient tcpClient, cEncryptionSettings EncryptionSettings)
        {

            bool bSuccess = false;

            bSSLStreamIsOk = false;

            this.IgnoreCertificateErrors = EncryptionSettings.IgnoreCertificateErrors;

            ClientCertificates.Clear();

            if (UseEncryption)
            {

                if (EncryptionSettings.AuthenticateAsClientUsingCertificate)
                {

                    X509Certificate2 certificate = CreateCertificate(EncryptionSettings.ClientCertificateFile, EncryptionSettings.ClientCertificateFilePassword);

                    if (certificate == null)
                    {
                        return false;
                    }

                    ClientCertificates.Add(certificate);

                }

                sslStream = new SslStream(tcpClient.GetStream(), false, new RemoteCertificateValidationCallback(ServerCertificateValidationCallback), new LocalCertificateSelectionCallback(SelectLocalCertificate));

                try
                {
                    if (EncryptionSettings.AuthenticateAsClientUsingCertificate)
                    {
                        sslStream.AuthenticateAsClient(EncryptionSettings.ServerName, ClientCertificates, EncryptionSettings.sslProtocols, EncryptionSettings.CheckCertificateRevocationList);
                    }
                    else
                    {
                        sslStream.AuthenticateAsClient(EncryptionSettings.ServerName, null, EncryptionSettings.sslProtocols, EncryptionSettings.CheckCertificateRevocationList);
                    }
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Authentication succeeded");
                    LogSecurityLevel(sslStream);
                    LogSecurityServices(sslStream);
                    LogCertificateInformation(sslStream);
                    bSSLStreamIsOk = true;
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Authentication failed, error: {0}", e.ToString());
                }
            }
            else
            {
                try
                {
                    networkStream = tcpClient.GetStream();
                    bSuccess = true;
                }
                catch (AuthenticationException e)
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not get network stream, error: {0}", e.ToString());
                }
            }

            return bSuccess;

        }

        public bool InitializeAsServerAndGetStream(TcpClient tcpClient, cEncryptionSettings EncryptionSettings)
        {

            bool bSuccess = false;

            bSSLStreamIsOk = false;

            this.IgnoreCertificateErrors = EncryptionSettings.IgnoreCertificateErrors;

            if (UseEncryption)
            {

                X509Certificate2 certificate = CreateCertificate(EncryptionSettings.ServerCertificateFile, EncryptionSettings.ServerCertificateFilePassword);

                if (certificate == null)
                {
                    return false;
                }
                sslStream = new SslStream(tcpClient.GetStream(), false, new RemoteCertificateValidationCallback(ClientCertificateValidationCallback), null);
                //SslStream sslStream = new SslStream(tcpClient.GetStream(), false);

                try
                {
                    sslStream.AuthenticateAsServer(certificate, EncryptionSettings.RequireClientCertificate, EncryptionSettings.sslProtocols, EncryptionSettings.CheckCertificateRevocationList);
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Authentication succeeded");
                    LogSecurityLevel(sslStream);
                    LogSecurityServices(sslStream);
                    LogCertificateInformation(sslStream);
                    bSSLStreamIsOk = true;
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Authentication failed, error: {0}", e.ToString());
                    //if (e.InnerException != null)
                    //{
                    //  Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                    //}
                    //Console.WriteLine("Authentication failed - closing the connection.");
                }
            }
            else
            {
                try
                {
                    networkStream = tcpClient.GetStream();
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not get network stream, error: {0}", e.ToString());
                }
            }

            return bSuccess;

        }

        //
        // Called by the client SslStream if the server requires a client certificate
        //
        private X509Certificate SelectLocalCertificate(object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
        {

            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Server is requesting a local certificate");

            if (acceptableIssuers != null && acceptableIssuers.Length > 0 && localCertificates != null && localCertificates.Count > 0)
            {
                // Use the first certificate that is from an acceptable issuer.
                foreach (X509Certificate certificate in localCertificates)
                {
                    string issuer = certificate.Issuer;
                    if (Array.IndexOf(acceptableIssuers, issuer) != -1)
                    {
                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Returning a acceptable issuer's certificate: {0}", certificate.Issuer.ToString());
                        return certificate;
                    }
                }
            }
            if (localCertificates != null && localCertificates.Count > 0)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Returning first local certificate: {0}", localCertificates[0].Issuer.ToString());
                return localCertificates[0];
            }
            if (ClientCertificates.Count > 0)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Returning selected client certificate: {0}", ClientCertificates[0].Issuer.ToString());
                return ClientCertificates[0];
            }

            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Did not find any local certificate to return to server");

            return null;
        }

        // The following method is invoked by the RemoteCertificateValidationDelegate.
        private bool ClientCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }
            else
            {
                if (chain != null && chain.ChainStatus != null && chain.ChainStatus.Length > 0)
                {
                    LogChainStatusInfo(chain.ChainStatus);
                }

                if (IgnoreCertificateErrors)
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Client certificate could not be validated, error: {0}. We will ignore the error.", sslPolicyErrors.ToString());
                    return true;
                }
                else
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Client certificate could not be validated, error: {0}. Closing connection.", sslPolicyErrors.ToString());
                    return false;
                }
            }
        }

        private bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }
            else
            {
                if (chain != null && chain.ChainStatus != null && chain.ChainStatus.Length > 0)
                {
                    LogChainStatusInfo(chain.ChainStatus);
                }

                if (IgnoreCertificateErrors)
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Server certificate could not be validated, error: {0}. We will ignore the error.", sslPolicyErrors.ToString());
                    return true;
                }
                else
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Server certificate could not be validated, error: {0}. Closing connection.", sslPolicyErrors.ToString());
                    return false;
                }
            }
        }

        private void LogChainStatusInfo(X509ChainStatus[] chainStatus)
        {
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "X509 Certificate Chain status length: {0}", chainStatus.Length);

            foreach (X509ChainStatus ch in chainStatus)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Chain status: {0}, Info: {1}", ch.Status, ch.StatusInformation);
            }
        }

        public void LogSecurityLevel(SslStream stream)
        {
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Cipher: {0}, strength: {1}", stream.CipherAlgorithm, stream.CipherStrength);
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Hash: {0}, strength: {1}", stream.HashAlgorithm, stream.HashStrength);
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Key exchange: {0}, strength: {1}", stream.KeyExchangeAlgorithm, stream.KeyExchangeStrength);
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Protocol: {0}", stream.SslProtocol);
        }

        public void LogSecurityServices(SslStream stream)
        {
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Is authenticated: {0} ( as server: {1})", stream.IsAuthenticated, stream.IsServer);
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "IsSigned: {0}", stream.IsSigned);
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Is Encrypted: {0}", stream.IsEncrypted);
        }

        public void DisplayStreamProperties(SslStream stream)
        {
            Console.WriteLine("Can read: {0}, write {1}", stream.CanRead, stream.CanWrite);
            Console.WriteLine("Can timeout: {0}", stream.CanTimeout);
        }

        public static void LogCertificateInformation(SslStream stream)
        {
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Certificate revocation list checked: {0}", stream.CheckCertRevocationStatus);

            X509Certificate localCertificate = stream.LocalCertificate;
            if (stream.LocalCertificate != null)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Local cert was issued to {0}, and is valid from {1} until {2}.",
                    localCertificate.Subject,
                    localCertificate.GetEffectiveDateString(),
                    localCertificate.GetExpirationDateString());
            }
            else
            {
                Console.WriteLine("Local certificate is null.");
            }
            // Display the properties of the client's certificate.
            X509Certificate remoteCertificate = stream.RemoteCertificate;
            if (stream.RemoteCertificate != null)
            {
                Console.WriteLine("Remote cert was issued to {0} and is valid from {1} until {2}.",
                    remoteCertificate.Subject,
                    remoteCertificate.GetEffectiveDateString(),
                    remoteCertificate.GetExpirationDateString());
            }
            else
            {
                Console.WriteLine("Remote certificate is null.");
            }
        }

        public void Close()
        {
            lock (lockObject)
            {
                if (networkStream != null)
                {
                    try
                    {
                        networkStream.Close();
                        networkStream = null;
                    }
                    catch
                    {
                    }
                }
                if (sslStream != null)
                {
                    try
                    {
                        sslStream.Close();
                        sslStream = null;
                    }
                    catch
                    {
                    }
                }
            }
        }

        public void Write(byte[] buffer, int offset, int size)
        {
            lock (lockObject)
            {
                if (UseEncryption)
                {
                    sslStream.Write(buffer, offset, size);
                }
                else
                {
                    networkStream.Write(buffer, offset, size);
                }
            }
        }

        public int Read(byte[] buffer, int offset, int size)
        {
            if (UseEncryption)
            {
                return sslStream.Read(buffer, offset, size);
            }
            else
            {
                return networkStream.Read(buffer, offset, size);
            }
        }

    }


}
