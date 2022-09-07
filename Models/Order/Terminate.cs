using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.Order
{

    public class TerminateBase
    {
        /// <summary>
        /// 指示执行此任务的机器人后续是否接单的标识，true=执行此任务的机器人在此任务终止后不再接单，false=此任务终止后依然可以继续接单。可不填，默认为true
        /// </summary>
        public bool disableVehicle { get; set; } = true;

    }

    /// <summary>
    /// 終止特定訂單模型
    /// </summary>
    public class TerminateSingleTask : TerminateBase
    {
        public TerminateSingleTask(string id, bool getNewOrderable)
        {
            this.id = id;
            disableVehicle = !getNewOrderable;
        }

        /// <summary>
        /// Task id
        /// </summary>
        public string id { get; set; }

    }

    /// <summary>
    /// 終止多個特定訂單模型
    /// </summary>
    public class TerminateMultiTask : TerminateBase
    {
        public TerminateMultiTask(List<string> idList, bool getNewOrderable)
        {
            this.idList = idList;
            disableVehicle = !getNewOrderable;
        }
        /// <summary>
        /// Task ids
        /// </summary>
        public List<string> idList { get; set; } = new List<string>();
    }

    /// <summary>
    /// 終止特定agv的當前訂單
    /// </summary>
    public class TerminateVehiclesCurrentTask : TerminateBase
    {
        public TerminateVehiclesCurrentTask(List<string> vehicles, bool getNewOrderable, bool clearAllOrder)
        {
            this.vehicles = vehicles;
            disableVehicle = !getNewOrderable;
            clearAll = clearAllOrder;
        }

        public List<string> vehicles { get; set; } = new List<string>();
        public bool clearAll { get; set; } = false;
    }
}
