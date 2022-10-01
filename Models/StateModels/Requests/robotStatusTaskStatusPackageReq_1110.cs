using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.MapModels.Requests
{
    public class robotStatusTaskStatusPackageReq_1110 : ModelBase
    {

        public robotStatusTaskStatusPackageReq_1110() : base()
        {
            NO = 1110;
        }
        public string[] task_ids { get; set; } = new string[] { };
    }

}
