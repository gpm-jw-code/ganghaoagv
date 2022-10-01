using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.MapModels.Requests
{
    public class robotMapTaskGoTargetReq_3051 : ModelBase
    {
        public robotMapTaskGoTargetReq_3051() : base()
        {
            NO = 3051;
        }

        public robotMapTaskGoTargetReq_3051(string taskID, string sourceID, string destinID)
        {
            NO = 3051;
            this.task_id = taskID;
            this.source_id = sourceID;
            this.id = destinID;
        }

        public string source_id { get; set; }
        public string id { get; set; }
        public string task_id { get; set; }

    }

}
