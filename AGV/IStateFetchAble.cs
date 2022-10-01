using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.AGV
{
    public interface IStateFetchAble
    {
        API.RobotStateAPI StateAPI { get; set; }
    }
}
