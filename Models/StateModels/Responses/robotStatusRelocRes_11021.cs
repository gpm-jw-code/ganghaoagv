using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.StateModels.Responses
{
    /// <summary>
    /// 重定位狀態 REQ:1021 的回覆
    /// </summary>
    public class robotStatusRelocRes_11021 : ResModelBase
    {
        /// <summary>
        /// 0 = FAILED(定位失敗), 
        /// 1 = SUCCESS(定位正確),
        /// 2 = RELOCING(正在重定位), 
        /// 3 = COMPLETED(定位完成
        /// </summary>
        public enum RELOC_STATE
        {
            FAILED, SUCCESS, RELOCING, COMPLETED
        }

        /// <summary>
        /// 0 = FAILED(定位失敗), 
        /// 1 = SUCCESS(定位正確),
        /// 2 = RELOCING(正在重定位), 
        /// 3 = COMPLETED(定位完成
        /// </summary>
        public int reloc_status { get; set; }
    }
}
