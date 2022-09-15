using System;
using System.Threading;
using System.Threading.Tasks;

namespace GangHaoAGV.AGV
{
    /// <summary>
    /// 這是狀態
    /// </summary>
    public class clsSTATES : ITcpHandshakeAble
    {
        public API.RobotStateAPI API;
        public Communiation.agvTcpClient conn { get; set; }
        private bool reconnectingFlag = false;
        internal event EventHandler OnConnected;
        internal event EventHandler OnDisConnected;
        public clsSTATES(Communiation.agvTcpClient conn)
        {
            this.conn = conn;
            this.conn.OnDisconnect += _conn_OnDisconnect;
            API = new API.RobotStateAPI(conn);
            StateFetchWork();
            if (!this.conn.connected)
                ReconnectWork();
        }
        public Models.StateModels.Responses.robotStatusInfoRes_11000 statinfo { get; private set; } = new Models.StateModels.Responses.robotStatusInfoRes_11000();
        public Models.StateModels.Responses.robotStatusBatteryRes_11007 betteryState { get; private set; } = new Models.StateModels.Responses.robotStatusBatteryRes_11007();
        public Models.StateModels.Responses.robotStatusTaskStatusPackageRes_11110 taskStatusPakage { get; private set; } = new Models.StateModels.Responses.robotStatusTaskStatusPackageRes_11110();
        public Models.StateModels.Responses.robotStatusLocRes_11004 locationInfo { get; private set; } = new Models.StateModels.Responses.robotStatusLocRes_11004();
        /// <summary>
        /// 重定位狀態
        /// </summary>
        public Models.StateModels.Responses.robotStatusRelocRes_11021 relocState { get; private set; } = new Models.StateModels.Responses.robotStatusRelocRes_11021();
        private async Task StateFetchWork()
        {
            while (true)
            {
                await Task.Delay(100);
                if (!conn.connected)
                    continue;
                try
                {
                    statinfo = await API.GetRobotStatusInfo();
                    betteryState = await API.GetRobotBattery();
                    taskStatusPakage = await API.GetTaskStatusPackage();
                    locationInfo = await API.GetRobotLocation();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("StateFetchWork {0}:{1} {2}", conn.host, conn.port, ex.Message);
                    return;
                }
            }
        }

        /// <summary>
        /// 把目前的狀態拋出來
        /// </summary>
        public void Download()
        {

        }

        private async Task ReconnectWork()
        {
            while (!conn.TryConnect(out string err_msg))
            {
                reconnectingFlag = true;
                Console.WriteLine("ReconnectWork {0}:{1} {2}", conn.host, conn.port, err_msg);
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            OnConnected?.Invoke(this, EventArgs.Empty);
            reconnectingFlag = false;
        }

        private async void _conn_OnDisconnect(object sender, System.EventArgs e)
        {
            OnDisConnected?.Invoke(this, EventArgs.Empty);
            if (reconnectingFlag)
                return;
            reconnectingFlag = true;
            ReconnectWork();
        }

    }
}
