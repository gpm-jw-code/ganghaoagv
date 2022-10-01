using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.StateModels.Responses
{
    public class robotStatusSpeedRes_11005 : ResModelBase
    {
        public double vx { get; set; }
        public double vy { get; set; }
        public double w { get; set; }
        public double steer { get; set; }
        public double spin { get; set; }
        public double r_vx { get; set; }
        public double r_vy { get; set; }
        public double r_w { get; set; }
        public double r_steer { get; set; }
        public double r_spin { get; set; }
        public double[] steer_angles { get; set; }
        public double[] r_steer_angles { get; set; }
        public bool is_stop { get; set; }

    }
}
