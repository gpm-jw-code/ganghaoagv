using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.StateModels.Responses
{
    public class robotStatusBlockRes_11006 : ResModelBase
    {
        public bool blocked { get; set; }
        public int block_reason { get; set; }
        public double block_x { get; set; }
        public double block_y { get; set; }
        public int block_id { get; set; }
        public bool slowed { get; set; }
        public int slow_reason { get; set; }
        public double slow_x { get; set; }
        public double slow_y { get; set; }
        public int slow_id { get; set; }
    }
}
