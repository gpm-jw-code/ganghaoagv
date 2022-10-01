using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.StateModels.Responses
{
    /// <summary>
    /// 機器人位置
    /// </summary>
    public class robotStatusLocRes_11004 : ResModelBase
    {
        public double x { get; set; }
        public double y { get; set; }
        public double angle { get; set; }
        public double confidence { get; set; }
        public string current_station { get; set; }
        public string last_station { get; set; }
    }
}
