using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace GangHaoAGV.Communiation
{
    public class agvTcpClient
    {
        /// <summary>
        /// 斷線事件
        /// </summary>
        public event EventHandler OnDisconnect;
        private Socket tcpSocket;
        public readonly string host;
        public readonly int port;
        private CancellationTokenSource waitRevDoneCTSK;
        public bool connected { get; private set; }
        public agvTcpClient(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        internal bool TryConnect(out string errMsg)
        {
            errMsg = null;
            try
            {
                tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                tcpSocket.ReceiveBufferSize = 163840;
                tcpSocket.Connect(host, port);
                connected = true;
                Console.WriteLine($"{host}:{port} Connected");
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                connected = false;
                Console.WriteLine($"{host}:{port} Connect Fail:{errMsg}");
                return false;
            }
        }
        private agvReturnState _agvReturnState = new agvReturnState();
        internal async Task<agvReturnState> Send(byte[] apiContextBytes, int cmdNoMark = -1)
        {
            clsSocketState socketState = new clsSocketState(tcpSocket, 8192);
            try
            {
                waitRevDoneCTSK = new CancellationTokenSource();
                tcpSocket.BeginReceive(socketState.buffer, 0, socketState.bufferSize, SocketFlags.None, new AsyncCallback(RecieveCallBackHandle), socketState);
                tcpSocket.Send(apiContextBytes, apiContextBytes.Length, SocketFlags.None);
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(15), waitRevDoneCTSK.Token);
                }
                catch (TaskCanceledException ex)
                {

                }
                finally
                {
                    if (!waitRevDoneCTSK.IsCancellationRequested)
                    {
                        Console.WriteLine("{0}:{1}:{2}-Timeout了", host, port, cmdNoMark == -1 ? "-" : cmdNoMark.ToString());
                    }
                }
                return socketState.dataState;
            }
            catch (SocketException ex)
            {
                OnDisconnect?.Invoke(this, null);
                connected = false;
                socketState.dataState.disconnected = true;
                Console.WriteLine("{0}:{1}-{2}", host, port, ex.Message);
                return socketState.dataState;
            }
        }

        private void RecieveCallBackHandle(IAsyncResult ar)
        {
            try
            {
                clsSocketState state = (clsSocketState)ar.AsyncState;
                int recieveLen = state.socket.EndReceive(ar);
                List<string> strAry = state.buffer.Select(b => b.ToString("X2")).ToList();
                if (strAry.Contains("5A"))
                {
                    int startIndex = strAry.IndexOf("5A");
                    //數據區長度位置索引 = startIndex + 4  
                    int dataLenStartIndex = startIndex + 4;
                    byte[] dataLenBytes = new byte[4] { state.buffer[dataLenStartIndex], state.buffer[dataLenStartIndex + 1], state.buffer[dataLenStartIndex + 2], state.buffer[dataLenStartIndex + 3] };
                    int dataLen = BitConverter.ToInt32(dataLenBytes.Reverse().ToArray(), 0);
                    //所以總封包長度
                    int totalLen = 16 + dataLen;
                    if (totalLen != recieveLen)
                    {

                    }
                    
                    state.dataState.agvReturnDatBytes = new ArraySegment<byte>(state.buffer, 0, totalLen).ToArray();
                    state.dataState.dataLen = dataLen;
                    waitRevDoneCTSK.Cancel();
                }
            }
            catch (SocketException ex)
            {
                OnDisconnect?.Invoke(this, null);
                _agvReturnState.disconnected = true;
                waitRevDoneCTSK.Cancel();
                Console.WriteLine("{0}:{1}-{2}", host, port, ex.Message);

            }
            catch (Exception ex)
            {
                OnDisconnect?.Invoke(this, null);
                _agvReturnState.disconnected = true;
                waitRevDoneCTSK.Cancel();
                Console.WriteLine("{0}:{1}-{2}", host, port, ex.Message);
            }
        }

        internal void Close()
        {
            try
            {
                tcpSocket.Disconnect(false);
                tcpSocket.Close();
                tcpSocket.Dispose();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
