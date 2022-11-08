using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.ControlModels.Responses
{
    public class robotControlRelocReq_2002 : ModelBase
    {
        public robotControlRelocReq_2002()
        {
            NO = 2002;
        }
       
        public double x { get; set; } = 0;
        public double y { get; set; } = 0;
        public double angle { get; set; } = 0;
        public bool home { get; set; } = false;
    }
}
