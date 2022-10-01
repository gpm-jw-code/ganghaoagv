using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.MapModels.Requests
{
    public class robotMapTargetPathReq_3053 : ModelBase
    {
        public robotMapTargetPathReq_3053()
        {
            NO = 3053;
        }
        public robotMapTargetPathReq_3053(string id)
        {
            NO = 3053;
            this.id = id;
        }
        public string id { get; set; }
    }
}
