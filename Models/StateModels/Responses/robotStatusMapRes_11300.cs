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
    public class robotStatusMapRes_11300 : ResModelBase
    {
        public string current_map { get; set; }
        public string current_map_md5 { get; set; }
        public string[] maps { get; set; }
    }
}
