using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.StateModels.Responses
{
    public class robotStatusBatteryRes_11007 : ResModelBase
    {
        /// <summary>
        /// 剩餘電池趴數
        /// </summary>
        public double battery_level { get; set; }
        public double battery_temp { get; set; }
        public bool charging { get; set; }
        public double voltage { get; set; }
        public double current { get; set; }
        public double max_charge_voltage { get; set; }
        public double max_charge_current { get; set; }
        public bool manual_charge { get; set; }
        public int battery_cycle { get; set; }
        public string battery_user_data { get; set; }
    }
}
