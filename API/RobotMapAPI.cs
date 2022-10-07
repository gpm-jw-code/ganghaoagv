using GangHaoAGV.Communiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GangHaoAGV.Models.MapModels.Requests;
using GangHaoAGV.Models.MapModels.Responses;
using Newtonsoft.Json;
using GangHaoAGV.Models;

namespace GangHaoAGV.API
{
    public class RobotMapAPI : APIBase
    {
        public RobotMapAPI(agvTcpClient agvTcpClient) : base(agvTcpClient)
        {
            apiType = Enums.API_TYPE.ROBOT_MAP;
        }

        public async Task<ResModelBase> PauseNavigate()
        {
            try
            {
                agvReturnState revState = await APIExcute(CreateAPICmdBytes(1, 3001), useNewConnection: true);
                if (!revState.isReviced)
                {
                    return new ResModelBase(13001) { acturallyRecieved = false, conection_connected_inner = !revState.disconnected };
                }

                var jsonObj = JsonConvert.DeserializeObject<ResModelBase>(revState.dataJson);
                jsonObj.NO = 13001;
                jsonObj.json_reply = revState.dataJson;
                return jsonObj;
            }
            catch (Exception ex)
            {
                return new ResModelBase(13001)
                {
                    ret_code = 400,
                    err_msg = ex.Message,
                };
            }
        }

        public async Task<ResModelBase> ResumeNavigate()
        {
            try
            {
                agvReturnState revState = await APIExcute(CreateAPICmdBytes(1, 3002), useNewConnection: true);
                if (!revState.isReviced)
                {
                    return new ResModelBase(13002) { acturallyRecieved = false, conection_connected_inner = !revState.disconnected };
                }

                var jsonObj = JsonConvert.DeserializeObject<ResModelBase>(revState.dataJson);
                jsonObj.NO = 13002;
                jsonObj.json_reply = revState.dataJson;
                return jsonObj;
            }
            catch (Exception ex)
            {
                return new ResModelBase(13002)
                {
                    ret_code = 400,
                    err_msg = ex.Message,
                };
            }
        }
        public async Task<robotMapTaskGoTargetRes_13051> GoTarget(robotMapTaskGoTargetReq_3051 task)
        {
            try
            {
                agvReturnState revState = await APIExcute(CreateAPICmdBytes(1, task), useNewConnection: true);

                if (!revState.isReviced)
                {
                    return new robotMapTaskGoTargetRes_13051() { acturallyRecieved = false, conection_connected_inner = !revState.disconnected };
                }

                var jsonObj = JsonConvert.DeserializeObject<robotMapTaskGoTargetRes_13051>(revState.dataJson);
                jsonObj.json_reply = revState.dataJson;
                return jsonObj;

            }
            catch (Exception ex)
            {
                return new robotMapTaskGoTargetRes_13051()
                {
                    ret_code = 400,
                    acturallyRecieved = false,
                    conection_connected_inner = false,
                    err_msg = ex.Message
                };

            }

        }

        internal async Task<robotMapTargetPathRes_13053> GetPath(string targetID)
        {
            try
            {
                agvReturnState revState = await APIExcute(CreateAPICmdBytes(1, new robotMapTargetPathReq_3053(targetID)), useNewConnection: true);

                if (!revState.isReviced)
                {
                    return new robotMapTargetPathRes_13053() { acturallyRecieved = false, conection_connected_inner = !revState.disconnected };
                }

                var jsonObj = JsonConvert.DeserializeObject<robotMapTargetPathRes_13053>(revState.dataJson);
                jsonObj.json_reply = revState.dataJson;
                return jsonObj;

            }
            catch (Exception ex)
            {
                return new robotMapTargetPathRes_13053()
                {
                    ret_code = 400,
                    acturallyRecieved = false,
                    conection_connected_inner = false,
                    err_msg = ex.Message
                };

            }
        }

        public async Task<robotMapTaskGoTargetListRes_13066> GoTargetList(robotMapTaskGoTargetListReq_3066 req)
        {
            try
            {
                agvReturnState revState = await APIExcute(CreateAPICmdBytes(1, req), useNewConnection: true);

                if (!revState.isReviced)
                {
                    return new robotMapTaskGoTargetListRes_13066() { acturallyRecieved = false, conection_connected_inner = !revState.disconnected };
                }
                var jsonObj = JsonConvert.DeserializeObject<robotMapTaskGoTargetListRes_13066>(revState.dataJson);
                jsonObj.json_reply = revState.dataJson;
                return jsonObj;
            }
            catch (Exception ex)
            {
                return new robotMapTaskGoTargetListRes_13066()
                {
                    ret_code = 400,
                    acturallyRecieved = false,
                    conection_connected_inner = false,
                    err_msg = ex.Message
                };
            }
        }


        public async Task<robotMapTaskGoTargetRes_13003> TaskCancel()
        {
            agvReturnState revState = await APIExcute(CreateAPICmdBytes(1, 3003), useNewConnection: true);
            if (revState.isReviced)
            {
                var jsonObj = JsonConvert.DeserializeObject<robotMapTaskGoTargetRes_13003>(revState.dataJson);
                jsonObj.json_reply = revState.dataJson;
                return jsonObj;
            }
            else
            {
                return new robotMapTaskGoTargetRes_13003() { acturallyRecieved = false, conection_connected_inner = !revState.disconnected };
            }
        }
    }
}
