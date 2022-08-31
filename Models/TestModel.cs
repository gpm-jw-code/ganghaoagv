using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models
{
    public class TestModel : ModelBase
    {
        public double x { get; set; } = 10.0;
        public double y { get; set; } = 3.0;
        public int angle { get; set; } = 0;
        public TestModel() : base()
        {

        }
        public TestModel(ushort no)
        {
            NO = no;
        }
    }
}
