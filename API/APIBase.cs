using GangHaoAGV.Communiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GangHaoAGV.Models;
using Newtonsoft.Json;

namespace GangHaoAGV.API
{
    public class APIBase
    {
        protected Enums.API_TYPE apiType;

        protected agvTcpClient _agvTcpClient { get; set; }
        public string ConnectionErrorMessage { get; private set; }
        public APIBase()
        {

        }
        public APIBase(agvTcpClient agvTcpClient)
        {
            this._agvTcpClient = agvTcpClient;
        }
        /// <summary>
        /// 生成狀態API指令報文(帶資料)
        /// </summary>
        /// <param name="cmdNo">序號</param>
        /// <param name="data">數據模型,內含報文類型編號</param>
        /// <returns></returns>
        public byte[] CreateAPICmdBytes(ushort cmdNo, object data)
        {
            string dataJson = JsonConvert.SerializeObject(data);
            byte[] dataBytes = Encoding.ASCII.GetBytes(dataJson);
            int datalengLint = dataBytes.Length;
            byte[] dataLength = BitConverter.GetBytes(datalengLint);
            byte[] noBytes = BitConverter.GetBytes(cmdNo); //序號
            byte[] type = ((ModelBase)data).NOByte;

            byte[] output = new byte[16 + datalengLint];
            output[0] = 0x5A;
            output[1] = 0x01;
            output[2] = noBytes[1];
            output[3] = noBytes[0];
            output[4] = dataLength[3];
            output[5] = dataLength[2];
            output[6] = dataLength[1];
            output[7] = dataLength[0];
            output[8] = type[1];
            output[9] = type[0];
            output[10] = 0;
            output[11] = 0;
            output[12] = 0;
            output[13] = 0;
            output[14] = 0;
            output[15] = 0;
            Array.Copy(dataBytes, 0, output, 16, datalengLint);
            return output;
        }

        /// <summary>
        /// 生成狀態API指令報文(不帶資料)
        /// </summary>
        /// <param name="cmdNo">序號</param>
        /// <param name="modelno">報文類型 (編號)</param>
        /// <returns></returns>
        public byte[] CreateAPICmdBytes(ushort cmdNo, ushort modelno)
        {
            byte[] noBytes = BitConverter.GetBytes(cmdNo); //序號
            byte[] type = BitConverter.GetBytes(modelno);
            byte[] output = new byte[16];
            output[0] = 0x5A;
            output[1] = 0x01;
            output[2] = noBytes[1];
            output[3] = noBytes[0];
            output[4] = 0;
            output[5] = 0;
            output[6] = 0;
            output[7] = 0;
            output[8] = type[1];
            output[9] = type[0];
            output[10] = 0;
            output[11] = 0;
            output[12] = 0;
            output[13] = 0;
            output[14] = 0;
            output[15] = 0;
            return output;
        }

        /// <summary>
        /// 發送API
        /// </summary>
        /// <param name="conn">通訊物件</param>
        /// <param name="apiContextBytes">API報文</param>
        public async Task<agvReturnState> APIExcute(byte[] apiContextBytes, int cmdNoMark = -1, bool useNewConnection = false)
        {
            if (useNewConnection)
            {
                var agvTcpClient = new agvTcpClient(_agvTcpClient.host, _agvTcpClient.port);
                bool connected = agvTcpClient.TryConnect(out string errmsg);
                if (connected)
                {
                    var response = await agvTcpClient.Send(apiContextBytes, cmdNoMark);
                    agvTcpClient.Close();
                    return response;
                }
                else
                    return new agvReturnState { };
            }
            else
            {

                if (_agvTcpClient == null)
                    throw new NotImplementedException("連線尚未實作");
                return await _agvTcpClient.Send(apiContextBytes, cmdNoMark);
            }
        }

        public Object DeserializeObject(agvReturnState agvReturn)
        {
            return JsonConvert.DeserializeObject<Object>(agvReturn.dataJson);
        }
    }
}
