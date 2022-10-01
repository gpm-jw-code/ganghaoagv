using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.StateModels.Responses
{
    /// <summary>
    /// 查詢機器人任務狀態的回應
    /// </summary>
    public class robotStatusTaskStatusPackageRes_11110 : ResModelBase
    {
        public string closest_target { get; set; }
        public string source_name { get; set; }
        public string target_name { get; set; }
        public double percentage { get; set; }
        public TaskStatus[] task_status_list { get; set; }
        public string info { get; set; }

        public class TaskStatus
        {
            public string task_id { get; set; }

            /// <summary>
            /// 任務狀態
            ///  StatusNone = 0;
            ///  Waiting = 1;
            ///  Running = 2;
            ///  Suspended = 3;
            ///  Completed = 4;
            ///  Failed = 5;
            ///  Canceled = 6;
            ///  OverTime = 7;
            ///  NotFound = 404;
            /// </summary>
            public int status { get; set; }

        }

    }
}
