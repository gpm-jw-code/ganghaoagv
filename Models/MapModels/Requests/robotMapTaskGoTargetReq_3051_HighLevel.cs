using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.MapModels.Requests
{
    public class robotMapTaskGoTargetReq_3051_HighLevel : robotMapTaskGoTargetReq_3051
    {
        public robotMapTaskGoTargetReq_3051_HighLevel(string taskID, string sourceID, string destinID) : base(taskID, sourceID, destinID)
        {
        }

        public double angle { get; set; }
        public string method { get; set; } = "forward";
        public double max_speed { get; set; }
        public double max_wspeed { get; set; }
        public double max_acc { get; set; }
        public double max_wacc { get; set; }
        public double duration { get; set; }
        public bool spin { get; set; }
        public int delay { get; set; } = 0;
        public int rot_dir { get; set; }
        public string skill_name { get; set; }


    }

}
