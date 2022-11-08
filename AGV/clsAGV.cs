using GangHaoAGV.API;
using GangHaoAGV.Models.StateModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GangHaoAGV.AGV
{
    /// <summary>
    /// 這是一台車
    /// </summary>
    public class cAGV
    {
        internal Communiation.agvTcpClient stateConnection { get; set; }
        public Communiation.agvTcpClient controlConnection { get; set; }
        public Communiation.agvTcpClient mapConnection { get; set; }

        public Models.AGVModels.ConectionModel connectionParams = new Models.AGVModels.ConectionModel();

        public bool StatesPortConnected { get; private set; } = false;
        public bool ControlPortConnected { get; private set; } = false;
        public bool MapPortConnected { get; private set; } = false;

        /// <summary>
        /// 狀態
        /// </summary>
        public clsSTATES STATES { get; set; }
        /// <summary>
        /// 控制
        /// </summary>
        public clsControl CONTROL { get; set; }
        /// <summary>
        /// 導航
        /// </summary>
        public clsMap NAVIGATIOR { get; set; }
        public cAGV()
        {
            ConnectionInitialze();
        }
        public cAGV(string IP, bool autoFetchStateData = true)
        {
            connectionParams.IP = IP;
            ConnectionInitialze(autoFetchStateData);
        }

        public void ConnectionInitialze(bool autoFetchStateData = true)
        {
            string agvIP = connectionParams.IP;

            stateConnection = new Communiation.agvTcpClient(agvIP, 19204);
            controlConnection = new Communiation.agvTcpClient(agvIP, 19205);
            mapConnection = new Communiation.agvTcpClient(agvIP, 19206);

            StatesPortConnected = stateConnection.TryConnect(out string err_msg_state);
            ControlPortConnected = controlConnection.TryConnect(out string err_msg_control);
            MapPortConnected = mapConnection.TryConnect(out string err_msg_map);

            STATES = new clsSTATES(stateConnection, autoFetchStateData);
            STATES.OnConnected += STATES_OnConnected;
            STATES.OnDisConnected += STATES_OnDisConnected;
            CONTROL = new clsControl(controlConnection, STATES);
            NAVIGATIOR = new clsMap(mapConnection, STATES);
        }

        private void STATES_OnDisConnected(object sender, EventArgs e)
        {
            StatesPortConnected = false;
        }

        private void STATES_OnConnected(object sender, EventArgs e)
        {
            StatesPortConnected = true;
        }

        private void MapConnection_OnDisconnect(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ControlConnection_OnDisconnect(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }



    }
}
