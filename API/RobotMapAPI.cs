using GangHaoAGV.Communiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.API
{
    public class RobotMapAPI : APIBase
    {
        public RobotMapAPI(agvTcpClient agvTcpClient) : base(agvTcpClient)
        {
            apiType = Enums.API_TYPE.ROBOT_MAP;
        }
    }
}
