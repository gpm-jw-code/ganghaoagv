using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.Order
{
    public class OrderDetails : SetOrder
    {
        public int createTime { get; set; }
        public int terminalTime { get; set; }

        public List<object> errors { get; set; }
        public string msg { get; set; }
        public List<object> notices { get; set; }
        public string state { get; set; }
        public List<object> warnings { get; set; }
    }
}
