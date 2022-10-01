using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.StateModels.Responses
{
    public class robotStatusRunRes_11002 : ResModelBase
    {
        public double odo { get; set; }
        public double today_odo { get; set; }
        public double time { get; set; }
        public double total_time { get; set; }
        public double controller_temp { get; set; }
        public double controller_humi { get; set; }
        public double controller_voltage { get; set; }
    }
}
