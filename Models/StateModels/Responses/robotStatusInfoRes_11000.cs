using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.StateModels.Responses
{
    /// <summary>
    /// 機器人信息
    /// </summary>
    public class robotStatusInfoRes_11000 : ResModelBase
    {
        public string id { get; set; }
        public string vehicle_id { get; set; }
        public string robot_note { get; set; }
        public string version { get; set; }
        public string model { get; set; }
        public string dsp_version { get; set; }
        public string gyro_version { get; set; }
        public string map_version { get; set; }
        public string model_version { get; set; }
        public string netprotocol_version { get; set; }
        public string modbus_version { get; set; }
        public string current_map { get; set; }
        public string current_map_md5 { get; set; }
        public string model_md5 { get; set; }
        public string ssid { get; set; }
        public double rssi { get; set; }
        public string ap_addr { get; set; }
        public string current_ip { get; set; }
        public string MAC { get; set; }
        public string echoid_type { get; set; }
        public string echoid { get; set; }
        public Features features { get; set; } //TODO Confirm object member type

        public class Features
        {

        }

    }
}
