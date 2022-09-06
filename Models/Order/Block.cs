using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.Order
{
    public class Block
    {
        public string blockId { get; set; }
        public string location { get; set; }
        public string operation { get; set; }
        public string scriptName { get; set; }
        //public ScriptArgs scriptArgs { get; set; }
        public Dictionary<string, object> operationArgs { get; set; }
        //public ScriptArgs script_args { get; set; }
    }
}
