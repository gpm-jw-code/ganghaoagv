using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.StateModels.Responses
{
    /// <summary>
    /// 查詢機器人載入的地圖以及儲存的地圖的回應
    /// </summary>
    public class robotStatusStationRes_11301 : ResModelBase
    {
        public Station[] stations { get; set; }
        public class Station
        {
            public string desc { get; set; }
            public string id { get; set; }
            public string type { get; set; }
            public double x { get; set; }
            public double y { get; set; }
            public double r { get; set; }
        }
        //{"create_on":"2022-09-19T23:20:05.386Z","ret_code":0,
        //"stations":[
        //  {"desc":"","id":"LM1","r":-3.1416,"type":"LocationMark","x":2.277,"y":-0.065},
        //  {"desc":"","id":"LM2","r":-3.1416,"type":"LocationMark","x":-2.783,"y":-0.065},{"desc":"","id":"LM3","r":1.5708,"type":"LocationMark","x":-2.783,"y":1.236},{"desc":"","id":"LM5","r":-0.0,"type":"LocationMark","x":0.65,"y":-0.868},{"desc":"","id":"LM6","r":0.0304,"type":"LocationMark","x":3.619,"y":-1.274}]}
    }
}
