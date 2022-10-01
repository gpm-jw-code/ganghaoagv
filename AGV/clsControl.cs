using GangHaoAGV.Communiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task Reloc()
        {
            await ControlAPI.ReLoc();//發送重定位請求
            await WaitRelocComplete(); //等待重定位完成
            await ControlAPI.ConfirmLoc();//確認定位正確

        }


        /// <summary>
        /// 等待重定位完成(使用1021 req 讀狀態)
        /// </summary>
        /// <returns></returns>
        private async Task WaitRelocComplete()
        {
            while ((await StateAPI.GetRelocState()).reloc_status == (int)Models.StateModels.Responses.robotStatusRelocRes_11021.RELOC_STATE.RELOCING)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
