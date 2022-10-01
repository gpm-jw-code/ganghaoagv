using GangHaoAGV.API;
using GangHaoAGV.Communiation;
using GangHaoAGV.Models.MapModels.Requests;
using GangHaoAGV.Models.MapModels.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GangHaoAGV.AGV
{
    public class clsMap : clsControl
    {

        /// <summary>
        /// 抵達目標點位事件
        /// </summary>
        public event EventHandler<robotMapTaskGoTargetReq_3051> OnReachPoint;


        public class MapReqResult
        {
            public bool Success { get; set; }
            public string ErrMsg { get; set; }

            public string[] Path { get; set; }
        }

        public API.RobotMapAPI mapAPI;

        public clsMap(agvTcpClient conn, RobotStateAPI StateAPI) : base(conn, StateAPI)
        {
            mapAPI = new RobotMapAPI(this.conn);
        }


        public async Task<string[]> GetPath(string targetID)
        {
            var res = await mapAPI.GetPath(targetID);
            if (res.ret_code == 0)
                return res.path;
            else
                return new string[] { };
        }

        public async Task<MapReqResult> GoTarget(robotMapTaskGoTargetReq_3051 task)
        {
            var path = await GetPath(task.id);
            return await Task.Run(async () =>
            {
                robotMapTaskGoTargetRes_13051 res = mapAPI.GoTarget(task).Result;
                if (res.ret_code != 0)
                {
                    return new MapReqResult { Success = false, ErrMsg = res.err_msg, Path = path };
                }
                return new MapReqResult() { Success = true, Path = path };
            });
        }

        public async Task<bool> TaskCancel()
        {
            robotMapTaskGoTargetRes_13003 ret = await mapAPI.TaskCancel();
            return ret.ret_code == 0;
        }

        /// <summary>
        /// 連續導航，叫機器人去多個站點
        /// </summary>
        /// <param name="taskList"></param>
        /// <returns></returns>
        public async Task<MapReqResult> GoTargets(robotMapTaskGoTargetListReq_3066 task_3066)
        {
            //TODO 任務完成確認
            //await WaitNavigatTaskFinish();

            return await Task.Run(async () =>
             {
                 mapAPI.GoTargetList(task_3066);
                 Console.WriteLine($"導航結束({task_3066.move_task_list.Count}個站點)");
                 return new MapReqResult { Success = true, ErrMsg = "" };
             });
        }

        /// <summary>
        /// 呼叫api req 1020確認導航完成
        /// </summary>
        /// <returns></returns>
        private async Task WaitTaskFinish(robotMapTaskGoTargetReq_3051 task, CancellationTokenSource waitCn)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));

            _ = Task.Run(async () =>
            {
                while (!waitCn.IsCancellationRequested)
                {
                    Console.WriteLine($"等待導航結束...(taskID:{task.task_id} / 初始點位:{task.source_id} /目標點位:{task.id})");
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }

            }, waitCn.Token);

            bool isNavigating = true;
            while (isNavigating)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1000));
                var naviState = await StateAPI.GetRobotStatusTask();
                Console.WriteLine(JsonConvert.SerializeObject(naviState, Formatting.Indented));
                isNavigating = naviState.task_status == 2;
            }
            waitCn.Cancel();
        }

        private async Task WaitNavigatTaskFinish()
        {
            while ((await StateAPI.GetRobotStatusTask()).isNavigating)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(300));
            }

        }

    }
}
