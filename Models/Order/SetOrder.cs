using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.Order
{
    public class SetOrder
    {
        public string id { get; set; }
        public string vehicle { get; set; }
        public string group { get; set; }
        public List<string> keyRoute { get; set; }
        public int priority { get; set; }
        public List<Block> blocks { get; set; }
        public bool complete { get; set; }
    }
}
