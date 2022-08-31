using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.StateModels.Responses
{
    /// <summary>
    /// 機器人信息
    /// </summary>
    public class robotStatusInfoRes_11000 : ResModelBase
    {
        public string EqName { get; set; } = "GangHaoAGV_001";
        public int Version { get; set; } = 102;
        public double z { get; set; } = 3;
    }
}
