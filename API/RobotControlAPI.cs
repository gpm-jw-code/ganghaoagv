using GangHaoAGV.Communiation;
using GangHaoAGV.Models.ControlModels.Responses;
using GangHaoAGV.Models.StateModels.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.API
{
    public class RobotControlAPI : APIBase
    {
        public RobotControlAPI(agvTcpClient agvTcpClient) : base(agvTcpClient)
        {
            apiType = Enums.API_TYPE.ROBOT_CONTROL;
        }
        public async Task<robotControlRelocRes_12002> ReLoc()
        {
            agvReturnState ret = await APIExcute(CreateAPICmdBytes(1, new robotControlRelocReq_2002()
            {
            }));
            if (!ret.isReviced)
                return new robotControlRelocRes_12002() { acturallyRecieved = false };
            return JsonConvert.DeserializeObject<robotControlRelocRes_12002>(ret.dataJson);
        }
        /// <summary>
        /// 重定位請求2002
        /// </summary>
        /// <returns></returns>
        public async Task<robotControlRelocRes_12002> ReLoc(double x, double y, double angle, bool home)
        {
            agvReturnState ret = await APIExcute(CreateAPICmdBytes(1, new robotControlRelocReq_2002()
            {
                x = x,
                y = y,
                angle = angle,
                home = home,
            }));
            if (!ret.isReviced)
                return new robotControlRelocRes_12002() { acturallyRecieved = false };
            return JsonConvert.DeserializeObject<robotControlRelocRes_12002>(ret.dataJson);
        }

        /// <summary>
        /// 確認定位正確
        /// </summary>
        /// <returns></returns>
        public async Task<robotControlConfirmlocRes_12003> ConfirmLoc()
        {
            agvReturnState ret = await APIExcute(CreateAPICmdBytes(1, 2003));
            if (!ret.isReviced)
                return new robotControlConfirmlocRes_12003() { acturallyRecieved = false };
            return JsonConvert.DeserializeObject<robotControlConfirmlocRes_12003>(ret.dataJson);
        }
    }
}
