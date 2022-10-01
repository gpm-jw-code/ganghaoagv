using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.StateModels.Responses
{
    /// <summary>
    /// 機器人導航狀態、導航站點、導航相關路徑等. REQ:1020 的回覆
    /// </summary>
    public class robotStatusTaskRes_11020 : ResModelBase
    {
        /// <summary>
        /// 0 = NONE, 
        /// 1 = WAITING(目前不可能出現該狀態), 
        /// 2 = RUNNING, 
        /// 3 = SUSPENDED, 
        /// 4 = COMPLETED, 
        /// 5 = FAILED, 
        /// 6 = CANCELED
        /// </summary>
        public int task_status { get; set; }
        /// <summary>
        /// 導航類型, 
        /// 0 = 沒有導航, 
        /// 1 = 自由導航到任意點, 
        /// 2 = 自由導航到網站, 
        /// 3 = 路徑導航到網站, 
        /// 7 = 平動轉動, 
        /// 100 = 其他
        /// </summary>
        public int task_type { get; set; }
        public string target_id { get; set; }
        public double[] target_point { get; set; } = new double[3] { 0, 0, 0 };
        public List<string> finished_path { get; set; } = new List<string>();
        public List<string> unfinished_path { get; set; } = new List<string>();
        public string move_status_info { get; set; }
        public object[] containers { get; set; }

        public bool isNavigating => task_status == 2;
        public bool isFinish => task_status == 4;
    }
}
