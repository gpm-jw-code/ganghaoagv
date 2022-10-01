using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static GangHaoAGV.Models.StateModels.Responses.robotStatusStationRes_11301;

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
        public clsSTATES(Communiation.agvTcpClient conn, bool autoFetchStateData)
        {
            this.conn = conn;
            this.conn.OnDisconnect += _conn_OnDisconnect;

            API = new API.RobotStateAPI(conn);

            if (autoFetchStateData)
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
                if (!conn.connected)
                    continue;
                try
                {
                    statinfo = await API.GetRobotStatusInfo();
                    await Task.Delay(150);
                    betteryState = await API.GetRobotBattery();
                    await Task.Delay(150);
                    taskStatusPakage = await API.GetTaskStatusPackage();
                    await Task.Delay(150);
                    locationInfo = await API.GetRobotLocation();
                    await Task.Delay(150);
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

        public List<string> GetMapNames()
        {
            var mapRes = API.GetRobotMaps().Result;
            return mapRes.maps.ToList();
        }

        /// <summary>
        /// 取得當前載入的地圖名稱以及站點資訊
        /// </summary>
        /// <returns>KeyValuePair<string, Station[]></returns>
        public async Task<KeyValuePair<string, Station[]>> GetRobotCurrentStations()
        {
            var mapRes = await API.GetRobotMaps();
            var currentMap = mapRes.current_map;
            var res = await API.GetRobotStations();
            return new KeyValuePair<string, Station[]>(currentMap, res.stations);
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
