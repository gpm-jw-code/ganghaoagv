using GangHaoAGV.Communiation;
using GangHaoAGV.Models;
using GangHaoAGV.Models.StateModels.Requests;
using GangHaoAGV.Models.StateModels.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.API
{
    public class RobotStateAPI : APIBase
    {

        public RobotStateAPI(Enums.API_TYPE apiType) : base(apiType)
        {

        }

        public RobotStateAPI(agvTcpClient agvTcpClient)
        {
            _agvTcpClient = agvTcpClient;
        }

        /// <summary>
        /// 生成狀態API指令報文
        /// </summary>
        /// <param name="cmdNo">序號</param>
        /// <param name="modelno">報文類型 (編號)</param>
        /// <returns></returns>
        public byte[] CreateAPICmdBytes(ushort cmdNo, ushort modelno)
        {
            byte[] noBytes = BitConverter.GetBytes(cmdNo); //序號
            byte[] type = BitConverter.GetBytes(modelno);
            byte[] output = new byte[16];
            output[0] = 0x5A;
            output[1] = 0x01;
            output[2] = noBytes[1];
            output[3] = noBytes[0];
            output[4] = 0;
            output[5] = 0;
            output[6] = 0;
            output[7] = 0;
            output[8] = type[1];
            output[9] = type[0];
            output[10] = 0;
            output[11] = 0;
            output[12] = 0;
            output[13] = 0;
            output[14] = 0;
            output[15] = 0;
            return output;
        }

        public async Task<robotStatusInfoRes_11000> GetRobotStatusInfo()
        {
            agvReturnState revState = await APIExcute(CreateAPICmdBytes(1, 1000));
            if (!revState.isReviced)
            {
                return new robotStatusInfoRes_11000() { acturallyRecieved = false, conection_connected_inner = !revState.disconnected };
            }

            return JsonConvert.DeserializeObject<robotStatusInfoRes_11000>(revState.dataJson);
        }


        public async Task<robotStatusRunRes_11002> GetRobotStatusRun()
        {
            agvReturnState revState = await APIExcute(CreateAPICmdBytes(1, 1002));
            if (!revState.isReviced)
                return new robotStatusRunRes_11002() { acturallyRecieved = false };
            return JsonConvert.DeserializeObject<robotStatusRunRes_11002>(revState.dataJson);
        }


        public async Task<robotStatusLocRes_11004> GetRobotLocation()
        {
            agvReturnState revState = await APIExcute(CreateAPICmdBytes(1, 1004));
            if (!revState.isReviced)
                return new robotStatusLocRes_11004() { acturallyRecieved = false };
            return JsonConvert.DeserializeObject<robotStatusLocRes_11004>(revState.dataJson);
        }


        public async Task<robotStatusSpeedRes_11005> GetRobotSpeed()
        {
            agvReturnState revState = await APIExcute(CreateAPICmdBytes(1, 1005));
            if (!revState.isReviced)
                return new robotStatusSpeedRes_11005() { acturallyRecieved = false };
            return JsonConvert.DeserializeObject<robotStatusSpeedRes_11005>(revState.dataJson);
        }

        public async Task<robotStatusBlockRes_11006> GetRobotBlock()
        {
            agvReturnState revState = await APIExcute(CreateAPICmdBytes(1, 1006));
            if (!revState.isReviced)
                return new robotStatusBlockRes_11006() { acturallyRecieved = false };
            return JsonConvert.DeserializeObject<robotStatusBlockRes_11006>(revState.dataJson);
        }


        public async Task<robotStatusBatteryRes_11007> GetRobotBattery()
        {
            agvReturnState revState = await APIExcute(CreateAPICmdBytes(1, 1007));
            if (!revState.isReviced)
                return new robotStatusBatteryRes_11007() { acturallyRecieved = false };
            return JsonConvert.DeserializeObject<robotStatusBatteryRes_11007>(revState.dataJson);
        }

        public async Task<robotStatusLaserRes_11009> GetRobotLaser()
        {
            agvReturnState revState = await APIExcute(CreateAPICmdBytes(1, 1009));
            if (!revState.isReviced)
                return new robotStatusLaserRes_11009() { acturallyRecieved = false };
            return JsonConvert.DeserializeObject<robotStatusLaserRes_11009>(revState.dataJson);
        }
        public async Task<robotStatusPathRes_11010> GetRobotPath()
        {
            agvReturnState revState = await APIExcute(CreateAPICmdBytes(1, 1010));
            if (!revState.isReviced)
                return new robotStatusPathRes_11010() { acturallyRecieved = false };
            return JsonConvert.DeserializeObject<robotStatusPathRes_11010>(revState.dataJson);
        }


        public async Task<robotStatusTaskStatusPackageRes_11110> GetTaskStatusPackage()
        {
            agvReturnState revState = await APIExcute(CreateAPICmdBytes(1, 1110));
            if (!revState.isReviced)
                return new robotStatusTaskStatusPackageRes_11110() { acturallyRecieved = false };
            return JsonConvert.DeserializeObject<robotStatusTaskStatusPackageRes_11110>(revState.dataJson);
        }
    }
}
