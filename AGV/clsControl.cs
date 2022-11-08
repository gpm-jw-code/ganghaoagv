using GangHaoAGV.Communiation;
using GangHaoAGV.Models.ControlModels.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GangHaoAGV.Models.StateModels.Responses.robotStatusRelocRes_11021;

namespace GangHaoAGV.AGV
{
    public class clsControl : IStateFetchAble, ITcpHandshakeAble
    {
        public agvTcpClient conn { get; set; }
        public clsSTATES State { get; set; }
        public API.RobotControlAPI ControlAPI { get; }

        public clsControl(agvTcpClient conn, clsSTATES State)
        {
            this.conn = conn;
            this.State = State;
            ControlAPI = new API.RobotControlAPI(conn);
        }

        /// <summary>
        /// 重定位
        /// </summary>
        public async Task<bool> Reloc()
        {
            Console.WriteLine("3秒後進行重定位");
            await Task.Delay(3000);
            if (State.relocState.reloc_status == 1)
            {
                Console.WriteLine($"目前狀態為SUCCESS，不必進行重定位，狀態為可導航");
                return true;
            }

            double x = State.locationInfo.x;
            double y = State.locationInfo.y;
            double angle = State.locationInfo.angle;
            bool home = false;
            robotControlRelocRes_12002 reLocRes = await ControlAPI.ReLoc(x, y, angle, home);//發送重定位請求
            if (reLocRes.ret_code != 0)
            {
                Console.WriteLine($"重定位請求失敗:Error_code:{reLocRes.ret_code}");
                return false;
            }

            RELOC_STATE status = await WaitRelocComplete(); //等待重定位完成
            //注意: 定位状态(1021) 用于指示当前机器人定位状态是否正确
            //定位状态如果为 COMPLETED, 说明重定位已经完成，但是用户没有确认，此时需要通过(2003) 来进行确认或重新进行定位。
            //用户确认定位正确后，定位状态会变为 SUCCESS
            if (status == RELOC_STATE.COMPLETED)
            {
                var ret = await ControlAPI.ConfirmLoc();//確認定位正確
                if (ret.ret_code != 0)
                    return false;
                Stopwatch sw = Stopwatch.StartNew();
                while (State.relocState.reloc_status != 1)
                {
                    if (sw.ElapsedMilliseconds > 5000)
                        return false;
                    await Task.Delay(1000);
                }
                return true;
            }
            else
            {

                return false;
            }
        }


        /// <summary>
        /// 等待重定位完成(使用1021 req 讀狀態)
        /// </summary>
        /// <returns></returns>
        private async Task<RELOC_STATE> WaitRelocComplete()
        {

            int status = -1;

            while ((status = State.relocState.reloc_status) != (int)RELOC_STATE.COMPLETED)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            //while (true)
            //{
            //    var _status = State.relocState.reloc_status;
            //    await Task.Delay(TimeSpan.FromSeconds(1));
            //}
            switch (status)
            {
                case 0: return RELOC_STATE.FAILED;
                case 1: return RELOC_STATE.SUCCESS;
                case 2: return RELOC_STATE.RELOCING;
                case 3: return RELOC_STATE.COMPLETED;
                default: return RELOC_STATE.RELOCING;
            }

        }
    }
}
