using GangHaoAGV.Models.StateModels.Responses;
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
        public bool stateFetching { get; private set; } = false;
        internal event EventHandler OnConnected;
        internal event EventHandler OnDisConnected;
        public clsSTATES(Communiation.agvTcpClient conn, bool autoFetchStateData)
        {
            this.conn = conn;
            this.conn.OnDisconnect += _conn_OnDisconnect;

            API = new API.RobotStateAPI(conn);

            if (autoFetchStateData)
                StartStatesDataSync();

            if (!this.conn.connected)
                ReconnectWork();
        }

        public robotStatusInfoRes_11000 statinfo { get; private set; } = new robotStatusInfoRes_11000();
        public robotStatusBatteryRes_11007 betteryState { get; private set; } = new robotStatusBatteryRes_11007();
        public robotStatusTaskStatusPackageRes_11110 taskStatusPakage { get; private set; } = new robotStatusTaskStatusPackageRes_11110();
        public robotStatusLocRes_11004 locationInfo { get; private set; } = new robotStatusLocRes_11004();

        public robotStatusMapRes_11300 mapLoadInfo { get; private set; } = new robotStatusMapRes_11300();
        public robotStatusStationRes_11301 stationLoadInfo { get; private set; } = new robotStatusStationRes_11301();

        public robotStatusAlarmRes_11050 alarms { get; private set; } = new robotStatusAlarmRes_11050();
        public robotStatusTaskRes_11020 navi_task_state { get; private set; } = new robotStatusTaskRes_11020();
        /// <summary>
        /// 重定位狀態
        /// </summary>
        public robotStatusRelocRes_11021 relocState { get; private set; } = new robotStatusRelocRes_11021();

        public dynamic NativeDatas
        {
            get
            {
                Dictionary<string, object> nativeData = new Dictionary<string, object>()
                {
                    {"robot_status_info-1000", statinfo},
                    {"robot_status_loc-1004", locationInfo},
                    {"robot_status_battery-1007", betteryState},
                    {"robot_status_task_11020", navi_task_state},
                    {"robot_status_reloc-1021", relocState},
                    {"robot_status_task_status_package-1110", taskStatusPakage},
                    {"robot_status_map-1300", mapLoadInfo},
                    {"robot_status_station-1301", stationLoadInfo},
                    {"robot_status_alarms_11050", alarms},
                };
                return nativeData;
            }
        }

        public async Task StartStatesDataSync()
        {
            StateFetchsWork();

        }
        public string currentTaskid { get; set; } = null;
        private async Task StateFetchsWork()
        {
            _ = Task.Run(() =>
            {

                while (true)
                {
                    stateFetching = true;
                    Thread.Sleep(150);
                    if (!conn.connected)
                        continue;
                    string fetch_item = "GetRobotStatusInfo";
                    try
                    {
                        relocState = API.GetRelocState().Result;
                        Thread.Sleep(150);
                        statinfo = API.GetRobotStatusInfo().Result;
                        Thread.Sleep(150);
                        fetch_item = "GetRobotBattery";
                        betteryState = API.GetRobotBattery().Result;
                        Thread.Sleep(150);


                        fetch_item = "GetTaskStatusPackage";

                        if (currentTaskid != null)
                            taskStatusPakage = API.GetTaskStatusPackage(new string[] { currentTaskid }).Result;
                        else
                            taskStatusPakage = API.GetTaskStatusPackage().Result;

                        Thread.Sleep(150);
                        fetch_item = "GetRobotLocation";
                        locationInfo = API.GetRobotLocation().Result;
                        Thread.Sleep(150);
                        fetch_item = "GetRobotMaps";
                        mapLoadInfo = API.GetRobotMaps().Result;
                        Thread.Sleep(150);
                        fetch_item = "GetRobotStations";
                        stationLoadInfo = API.GetRobotStations().Result;
                        Thread.Sleep(150);
                        fetch_item = "GetAlarms";
                        alarms = API.GetAlarms().Result;
                        Thread.Sleep(150);
                        fetch_item = "navi_task_state";
                        navi_task_state = API.GetRobotStatusTask().Result;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("StateFetchWork({4}) ERROR! {0}:{1} \r\n{2} \r\n{3}", conn.host, conn.port, ex.StackTrace, ex.Message, fetch_item);
                        throw ex;
                    }
                }

            });
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
