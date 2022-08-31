using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.StateModels.Responses
{
    /// <summary>
    /// 機器人任務狀態
    /// </summary>
    public class robotStatusTaskStatusPackageRes_11110 : ResModelBase
    {
        public TaskStatus[] TaskList { get; set; } = new TaskStatus[]
        {
             new TaskStatus(){ TaskID="Rack-03", TaskName= "ERack03取料任務"},
             new TaskStatus(){ TaskID="Bettery-01", TaskName= "回01充電站充電"},
        };

        public class TaskStatus
        {
            public string TaskID { get; set; }
            public string TaskName { get; set; }

        }

    }
}
