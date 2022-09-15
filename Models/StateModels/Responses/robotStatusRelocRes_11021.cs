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
        public enum RELOC_STATE
        {
            COMPLETED, SUCCESS, RUNNING
        }
        public RELOC_STATE state { get; set; } = RELOC_STATE.RUNNING;
    }
}
