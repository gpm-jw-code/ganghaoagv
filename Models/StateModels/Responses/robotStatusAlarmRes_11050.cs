using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.StateModels.Responses
{
    public class robotStatusAlarmRes_11050 : ResModelBase
    {
        public enum ALARM_TYPE
        {
            Fatal, Error, Warning, Notice
        }

        public Dictionary<string, object>[] fatals { get; set; } = new Dictionary<string, object>[] { };
        public Dictionary<string, object>[] errors { get; set; } = new Dictionary<string, object>[] { };
        public Dictionary<string, object>[] warnings { get; set; } = new Dictionary<string, object>[] { };
        public Dictionary<string, object>[] notices { get; set; } = new Dictionary<string, object>[] { };
    }
}
