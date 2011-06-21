namespace PT_Sguil
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Security.Authentication;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    internal static class SguildConnection
    {
        private static TcpClient _client;
        private static string _dataBuffer;
        private static byte[] _readBuffer = new byte[ConfigurationSupport.bufferSize];
        internal static SslStream _sslStreamReader;
        private static StreamWriter _sslStreamWriter;
        private static string errorMsg;
        internal static bool isConnected;

        internal delegate void IncomingPcapFileProgressEventHandler(object sender, EventArgs e, int pcapPercentage);
        internal static event IncomingPcapFileProgressEventHandler IncomingPcapFileProgress;

        internal static void CloseConnection()
        {
            isConnected = false;
            if (_sslStreamReader != null)
            {
                _sslStreamReader.Dispose();
            }
            if (_sslStreamWriter != null)
            {
                _sslStreamWriter.Dispose();
            }
            if (_client != null)
            {
                _client.Close();
            }
        }

        private static void OnReceivedData(IAsyncResult ar)
        {
            try
            {
                if (_sslStreamReader.CanRead)
                {
                    int currentBytesRead = _sslStreamReader.EndRead(ar);
                    if (currentBytesRead > 0)
                    {
                        ReadServerMessage(currentBytesRead);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        internal static string OpenConnection()
        {
            errorMsg = string.Empty;
            _dataBuffer = string.Empty;
            int result = 0;
            int.TryParse(ConfigurationSupport.currentPort, out result);
            try
            {
                _client = new TcpClient(ConfigurationSupport.currentHost, result);
                string str = QuickRead(null);
                if (str != ConfigurationSupport.version)
                {
                    errorMsg = errorMsg + "Mismatched versions." + Environment.NewLine;
                    errorMsg = errorMsg + string.Format("SERVER: ({0})" + Environment.NewLine, str);
                    errorMsg = errorMsg + string.Format("CLIENT: ({0})" + Environment.NewLine, ConfigurationSupport.version);
                    CloseConnection();
                }
                if (_client.Connected)
                {
                    StreamWriter writer = new StreamWriter(_client.GetStream());
                    writer.WriteLine(string.Format("VersionInfo {{{0}}}", ConfigurationSupport.version));
                    writer.Flush();
                    _sslStreamReader = new SslStream(_client.GetStream(), false, new RemoteCertificateValidationCallback(CertificateValidationCallBack));
                    try
                    {
                        _sslStreamReader.AuthenticateAsClient(ConfigurationSupport.currentHost, null, SslProtocols.Ssl3, false);
                    }
                    catch (AuthenticationException exception)
                    {
                        errorMsg = errorMsg + "SSL Authentication Error." + Environment.NewLine;
                        errorMsg = errorMsg + exception.ToString();
                    }
                    _sslStreamWriter = new StreamWriter(_sslStreamReader);
                    _sslStreamWriter.AutoFlush = true;
                    _sslStreamWriter.WriteLine(string.Format("ValidateUser {0} {1}", ConfigurationSupport.currentUsername, ConfigurationSupport.currentPassword));
                    string str2 = QuickRead(_sslStreamReader);
                    if (str2 == "UserID INVALID")
                    {
                        CloseConnection();
                        errorMsg = "Invalid USERNAME and/or PASSWORD";
                    }
                    else
                    {
                        isConnected = true;
                        ConfigurationSupport.userID = str2.Split(new char[0])[1];
                    }
                }
            }
            catch (Exception ex)
            {
                isConnected = false;
                errorMsg = string.Format("Could not connect to {0}:{1}", ConfigurationSupport.currentHost, ConfigurationSupport.currentPort);
            }
            if (isConnected)
            {
                _readBuffer = new byte[0x100];
                _sslStreamReader.BeginRead(_readBuffer, 0, _readBuffer.Length, new AsyncCallback(SguildConnection.OnReceivedData), _client.GetStream());
            }
            return errorMsg;
        }

        private static string QuickRead(SslStream sslStream)
        {
            _readBuffer = new byte[ConfigurationSupport.bufferSize];
            int offset = 0;
            int count = 0;
            StringBuilder builder = new StringBuilder();
            if (sslStream != null)
            {
                do
                {
                    count = sslStream.Read(_readBuffer, 0, _readBuffer.Length);
                    System.Text.Decoder decoder = Encoding.ASCII.GetDecoder();
                    char[] chars = new char[decoder.GetCharCount(_readBuffer, 0, count)];
                    decoder.GetChars(_readBuffer, 0, count, chars, 0);
                    builder.Append(chars);
                    if (builder.ToString().IndexOf("\n") != -1)
                    {
                        break;
                    }
                }
                while (count != 0);
            }
            else
            {
                do
                {
                    int num3 = _client.GetStream().Read(_readBuffer, offset, _readBuffer.Length - offset);
                    offset += num3;
                }
                while (_client.GetStream().DataAvailable);
                builder.Append(Encoding.ASCII.GetString(_readBuffer, 0, offset));
            }
            return builder.ToString().TrimEnd(new char[] { '\r', '\n' });
        }

        private static void ReadServerMessage(int currentBytesRead)
        {
            string msg = Encoding.ASCII.GetString(_readBuffer);
            if (msg.Length > currentBytesRead)
            {
                msg = msg.Substring(0, currentBytesRead);
            }
            if (!msg.EndsWith("\n"))
            {
                _dataBuffer = _dataBuffer + msg;
                _readBuffer = new byte[ConfigurationSupport.bufferSize];
                _sslStreamReader.BeginRead(_readBuffer, 0, _readBuffer.Length, new AsyncCallback(SguildConnection.OnReceivedData), _sslStreamReader);
            }
            else
            {
                SynchronizationContext synchronizationContext;
                if (_dataBuffer.Length > 0)
                {
                    msg = _dataBuffer + msg;
                    _dataBuffer = string.Empty;
                }
                msg = msg.TrimEnd(new char[] { '\r', '\n' });
                _readBuffer = new byte[ConfigurationSupport.bufferSize];
                if (msg.StartsWith("WiresharkDataPcap"))
                {
                    string strData = msg.Replace("WiresharkDataPcap ", string.Empty);
                    synchronizationContext = AsyncOperationManager.SynchronizationContext;
                    Program.MainForm.IncomingPcapFile(strData);
                    AsyncOperationManager.SynchronizationContext = synchronizationContext;
                    _readBuffer = new byte[ConfigurationSupport.bufferSize];
                    _sslStreamReader.BeginRead(_readBuffer, 0, _readBuffer.Length, new AsyncCallback(SguildConnection.OnReceivedData), _sslStreamReader);
                }
                else
                {
                    _sslStreamReader.BeginRead(_readBuffer, 0, _readBuffer.Length, new AsyncCallback(SguildConnection.OnReceivedData), _sslStreamReader);
                    synchronizationContext = AsyncOperationManager.SynchronizationContext;
                    SguildCommands.ServerCommandRcvd(msg);
                    AsyncOperationManager.SynchronizationContext = synchronizationContext;
                }
            }
        }

        internal static void ReceivePcapFile(object _pcapInfo)
        {
            int num2;
            string[] strArray = (string[]) _pcapInfo;
            _readBuffer = new byte[ConfigurationSupport.bufferSize];
            int num = 0;
            string str = strArray[0];
            int.TryParse(strArray[1], out num2);
            int num3 = num2;
            int num4 = 0;
            int count = 0;
            int pcapPercentage = 0;
            FileStream stream = File.Open(string.Format(@"{0}\{1}", ConfigurationSupport.wiresharkStorageDir, str), FileMode.Create);
            try
            {
                do
                {
                    if (num3 > _readBuffer.Length)
                    {
                        count = _readBuffer.Length;
                    }
                    else
                    {
                        count = num3;
                    }
                    num = _sslStreamReader.Read(_readBuffer, 0, _readBuffer.Length);
                    stream.Write(_readBuffer, 0, count);
                    num4 += count;
                    num3 -= num;
                    double num7 = num4;
                    double num8 = num2;
                    double d = (num7 / num8) * 100.0;
                    pcapPercentage = (int) Math.Truncate(d);
                    IncomingPcapFileProgress(null, EventArgs.Empty, pcapPercentage);
                }
                while (num3 > 0);
                stream.Flush();
            }
            catch (UnauthorizedAccessException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            catch (IOException exception2)
            {
                MessageBox.Show(exception2.ToString());
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }
        }

        internal static void SendToSguild(string msg)
        {
            if (isConnected)
            {
                _sslStreamWriter.WriteLine(msg);
            }
        }

        private static bool CertificateValidationCallBack(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return ((sslPolicyErrors == SslPolicyErrors.None) || true);
        }
    }
}

