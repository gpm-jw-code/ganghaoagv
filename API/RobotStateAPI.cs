using GangHaoAGV.Communiation;
using GangHaoAGV.Models;
using GangHaoAGV.Models.MapModels.Requests;
using GangHaoAGV.Models.StateModels.Requests;
using GangHaoAGV.Models.StateModels.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.API
{
    public class RobotStateAPI : APIBase
    {

        public RobotStateAPI(agvTcpClient agvTcpClient) : base(agvTcpClient)
        {
            apiType = Enums.API_TYPE.ROBOT_STATE;
        }

        public async Task<string> GetStateJsonResponse(ushort cmbNo, object reqData = null, bool saveAsJsonFile = false)
        {
            byte[] apiData = reqData == null ? CreateAPICmdBytes(1, cmbNo) : CreateAPICmdBytes(1, reqData);
            agvReturnState revState = await APIExcute(apiData, cmbNo);
            if (!revState.isReviced)
            {
                return JsonConvert.SerializeObject(new ResModelBase(cmbNo) { acturallyRecieved = false });
            }
            if (saveAsJsonFile)
            {
                string folder = Path.Combine(Environment.CurrentDirectory, "GangHaoAPITest");
                Directory.CreateDirectory(folder);
                string filePath = Path.Combine(folder, $"{cmbNo + 10000}.json");
                File.WriteAllText(filePath, revState.dataJson);
            }
            return revState.dataJson == "null" ? JsonConvert.SerializeObject(new ResModelBase(cmbNo) { acturallyRecieved = false }) : revState.dataJson;
        }

        public async Task<robotStatusInfoRes_11000> GetRobotStatusInfo()
        {
            string json = await GetStateJsonResponse(1000);
            try
            {

                var objectRet = JsonConvert.DeserializeObject<robotStatusInfoRes_11000>(json);
                objectRet.json_reply = json;
                return objectRet;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 1020請求 robot_status_task_req
        /// </summary>
        /// <returns>robotStatusRelocRes_11020</returns>
        public async Task<robotStatusTaskRes_11020> GetRobotStatusTask()
        {
            string json = await GetStateJsonResponse(1020);
            var objectRet = JsonConvert.DeserializeObject<robotStatusTaskRes_11020>(json);
            objectRet.json_reply = json;
            return objectRet;
        }

        public async Task<robotStatusRunRes_11002> GetRobotStatusRun()
        {
            string json = await GetStateJsonResponse(1002);
            var objectRet = JsonConvert.DeserializeObject<robotStatusRunRes_11002>(json);
            objectRet.json_reply = json;
            return objectRet;
        }


        public async Task<robotStatusLocRes_11004> GetRobotLocation()
        {
            string json = await GetStateJsonResponse(1004);
            var objectRet = JsonConvert.DeserializeObject<robotStatusLocRes_11004>(json);
            objectRet.json_reply = json;
            return objectRet;
        }


        public async Task<robotStatusSpeedRes_11005> GetRobotSpeed()
        {
            string json = await GetStateJsonResponse(1005);
            var objectRet = JsonConvert.DeserializeObject<robotStatusSpeedRes_11005>(json);
            objectRet.json_reply = json;
            return objectRet;
        }

        public async Task<robotStatusBlockRes_11006> GetRobotBlock()
        {
            string json = await GetStateJsonResponse(1006);
            var objectRet = JsonConvert.DeserializeObject<robotStatusBlockRes_11006>(json);
            objectRet.json_reply = json;
            return objectRet;
        }


        public async Task<robotStatusBatteryRes_11007> GetRobotBattery()
        {
            string json = await GetStateJsonResponse(1007);
            var objectRet = JsonConvert.DeserializeObject<robotStatusBatteryRes_11007>(json);
            objectRet.json_reply = json;
            return objectRet;
        }

        public async Task<robotStatusLaserRes_11009> GetRobotLaser()
        {
            string json = await GetStateJsonResponse(1009);
            var objectRet = JsonConvert.DeserializeObject<robotStatusLaserRes_11009>(json);
            objectRet.json_reply = json;
            return objectRet;
        }
        public async Task<robotStatusPathRes_11010> GetRobotPath()
        {
            string json = await GetStateJsonResponse(1010);
            var objectRet = JsonConvert.DeserializeObject<robotStatusPathRes_11010>(json);
            objectRet.json_reply = json;
            return objectRet;
        }

        public async Task<robotStatusAlarmRes_11050> GetAlarms()
        {
            string json = await GetStateJsonResponse(1050);
            var objectRet = JsonConvert.DeserializeObject<robotStatusAlarmRes_11050>(json);
            objectRet.json_reply = json;
            return objectRet;
        }

        /// <summary>
        /// [1110] 查詢機器人任務狀態 (robot_status_task_status_package_req)
        /// </summary>
        /// <returns></returns>
        public async Task<robotStatusTaskStatusPackageRes_11110> GetTaskStatusPackage(string[] task_ids = null)
        {
            robotStatusTaskStatusPackageReq_1110 req = new robotStatusTaskStatusPackageReq_1110()
            {
                task_ids = task_ids == null ? new string[] { } : task_ids
            };
            string json = await GetStateJsonResponse(1110, req);
            var objectRet = JsonConvert.DeserializeObject<robotStatusTaskStatusPackageRes_11110>(json);
            objectRet.json_reply = json;
            return objectRet;
        }


        /// <summary>
        /// [1021] 取得目前重定位的狀態
        /// </summary>
        /// <returns></returns>
        public async Task<robotStatusRelocRes_11021> GetRelocState()
        {
            string json = await GetStateJsonResponse(1021);
            var objectRet = JsonConvert.DeserializeObject<robotStatusRelocRes_11021>(json);
            objectRet.json_reply = json;
            return objectRet;
        }


        /// <summary>
        ///  [1300] 查询机器人载入的地图以及储存的地图
        /// </summary>
        /// <returns></returns>
        public async Task<robotStatusMapRes_11300> GetRobotMaps()
        {
            string json = await GetStateJsonResponse(1300);
            var objectRet = JsonConvert.DeserializeObject<robotStatusMapRes_11300>(json);
            objectRet.json_reply = json;
            return objectRet;
        }

        /// <summary>
        ///  [1301] 查詢機器人當前載入地圖中的站點資訊 (robot_status_station)
        /// </summary>
        /// <returns></returns>
        public async Task<robotStatusStationRes_11301> GetRobotStations()
        {
            string json = await GetStateJsonResponse(1301);
            var objectRet = JsonConvert.DeserializeObject<robotStatusStationRes_11301>(json);
            objectRet.json_reply = json;
            return objectRet;
        }
    }
}
