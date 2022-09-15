using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.AGV
{
    internal interface ITcpHandshakeAble
    {
        Communiation.agvTcpClient conn { get; set; }
    }
}
