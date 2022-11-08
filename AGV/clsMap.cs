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


        ///3.1 常规任务流程
        ///常规流程下，机器人满足如下条件，调度可以下发"路径导航"的任务：
        ///网络连接正常
        ///拥有机器人控制权
        ///状态上报正常（非离线）
        ///状态中无 emergency、fatals、errors
        ///机器人无报错代码 54004, 54025, 54013
        ///对于叉车，fork_auto_flag 为 true
        ///机器人的位置可控，在任务相应的路径上（包括在路径的端点，或者在路径上），或者在前序未完成任务的路径上。
        ///一个“路径导航”的请求中，可以包含多个任务，即批量发任务。其特点包括：
        ///任务数组中的每个任务有 task_id。任务的 task_id 是宇宙唯一的。
        ///批量发出的一组任务应是连续的，每个任务的目标点，是下一个任务的起点。每个任务的起点和目标点，都存在路径直连。（当目标点是机器人当前位置时除外）
        ///机器人将按照下发任务的顺序，依次执行。机器人不会出现任务数组中的第二个任务失败，第三个任务却成功的情况。如果第 N 个任务失败，在当前时刻，后面任务都将被机器人自动取消。
        ///对于批量发送的一组任务，调度可按照发送时的顺序依次查询每个任务状态。调度也可以通过 RBK 19301 端口里所提供的 task_status_package 对象，来查询任务状态。
        ///调度向机器人的请求中，必须包含的字段有：
        ///"task_id"，字符串，任务的宇宙唯一识别码；
        ///"id"，字符串，目标站点名称，当要去的目标位置不是一个站点，而是机器人当前位置（即原地不动时），其值固定为 "SELF_POSITION"；
        ///"source_id"，字符串，起始站点名称，当起始位置不在一个站点上，而是机器人当前位置时，其值固定为"SELF_POSITION"。
        ///"operation"字段，若出现将必须为有效值，否则将不允许出现该字段。


        /// <summary>
        /// 抵達目標點位事件
        /// </summary>
        public event EventHandler<robotMapTaskGoTargetReq_3051> OnReachPoint;




        public class NavigateReqResult
        {
            public bool Success { get; set; } = false;
            public string ErrMsg { get; set; } = "";
            public string[] Path { get; set; } = new string[0];
        }

        public API.RobotMapAPI mapAPI;

        public clsMap(agvTcpClient conn, clsSTATES State) : base(conn, State)
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

        /// <summary>
        /// 執行3051 路径导航
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<NavigateReqResult> GoTarget(robotMapTaskGoTargetReq_3051 task)
        {
            CheckAlarmState(out bool alarm_exist, out string message);

            if (alarm_exist)
            {
                return new NavigateReqResult
                {
                    Success = false,
                    ErrMsg = "Robot 存在 Alarm，不得進行導航任務"
                };
            }

            var path = await GetPath(task.id);
            return await Task.Run(async () =>
            {
                robotMapTaskGoTargetRes_13051 res = mapAPI.GoTarget(task).Result;
                if (res.ret_code != 0)
                {
                    return new NavigateReqResult { Success = false, ErrMsg = res.err_msg, Path = path };
                }
                return new NavigateReqResult() { Success = true, Path = path };
            });
        }

        public void CheckAlarmState(out bool alarm_exist, out string message)
        {
            alarm_exist = false;
            message = "";
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

        public async Task PauseNavigate()
        {
            var res = await mapAPI.PauseNavigate();
        }

        public async Task ResumeNavigate()
        {
            var res = await mapAPI.ResumeNavigate();
        }
    }
}
