using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models
{
    public class ResModelBase : ModelBase
    {
        public int ret_code { get; set; }
        public string create_on { get; set; }
        public string err_msg { get; set; } = "";
        public string json_reply { get; set; }
        public bool conection_connected_inner { get; set; } = true;

        public ResModelBase() : base()
        {
            var name = GetType().Name;
            ushort.TryParse(name.Split('_')[1], out NO);
        }

        public ResModelBase(ushort cmdNo) : base()
        {
            NO = cmdNo;
        }

    }
}
