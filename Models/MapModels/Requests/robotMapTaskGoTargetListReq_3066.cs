using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.MapModels.Requests
{
    public class robotMapTaskGoTargetListReq_3066 : ModelBase
    {

        public robotMapTaskGoTargetListReq_3066() : base()
        {
            NO = 3066;
        }
        public List<robotMapTaskGoTargetReq_3051> move_task_list { get; set; } = new List<robotMapTaskGoTargetReq_3051>();
    }

}
