using GangHaoAGV.Communiation;
using GangHaoAGV.Models.ControlModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GangHaoAGV.Models.StateModels.Responses.robotStatusRelocRes_11021;

namespace GangHaoAGV.AGV
{
    public class clsControl : IStateFetchAble, ITcpHandshakeAble
    {
        public agvTcpClient conn { get; set; }
        public API.RobotStateAPI StateAPI { get; set; }
        public API.RobotControlAPI ControlAPI { get; }

        public clsControl(agvTcpClient conn, API.RobotStateAPI StateAPI)
        {
            this.conn = conn;
            this.StateAPI = StateAPI;
            ControlAPI = new API.RobotControlAPI(conn);
        }

        /// <summary>
        /// 重定位
        /// </summary>
        public async Task<bool> Reloc()
        {
            robotControlRelocRes_12002 reLocRes = await ControlAPI.ReLoc();//發送重定位請求
            RELOC_STATE status = await WaitRelocComplete(); //等待重定位完成
            //注意: 定位状态(1021) 用于指示当前机器人定位状态是否正确
            //定位状态如果为 COMPLETED, 说明重定位已经完成，但是用户没有确认，此时需要通过(2003) 来进行确认或重新进行定位。
            //用户确认定位正确后，定位状态会变为 SUCCESS
            if (status == RELOC_STATE.COMPLETED)
                await ControlAPI.ConfirmLoc();//確認定位正確
            bool finish = (await StateAPI.GetRelocState()).reloc_status == 1;
            return finish;
        }


        /// <summary>
        /// 等待重定位完成(使用1021 req 讀狀態)
        /// </summary>
        /// <returns></returns>
        private async Task<RELOC_STATE> WaitRelocComplete()
        {

            int status = -1;

            while ((status = (await StateAPI.GetRelocState()).reloc_status) == (int)RELOC_STATE.RELOCING)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }

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
