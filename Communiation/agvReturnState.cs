using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Communiation
{
    public class agvReturnState
    {
        public bool disconnected { get; set; } = false;
        public byte[] agvReturnDatBytes { get; internal set; } = new byte[0];
        /// <summary>
        /// 數據區長度
        /// </summary>
        public int dataLen { get; internal set; }
        public bool isReviced => agvReturnDatBytes.Length != 0;

        public string dataJson
        {
            get
            {
                if (isReviced)
                {
                    try
                    {
                        return Encoding.ASCII.GetString(agvReturnDatBytes, 16, dataLen);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
                else
                    return "";
            }
        }

    }
}
