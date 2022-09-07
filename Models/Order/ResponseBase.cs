using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models.Order
{
    public class ResponseBase
    {
        /// <summary>
        /// 40000	RDSCORE_NOT_ACTIVATED	RDSCore 未激活;
        /// 50000	JSON_PARSE_ERROR HTTP 请求数据格式不为 JSON;
        /// 50001	JSON_FORMAT_ERROR HTTP 请求数据 JSON 字段缺失或者字段类型错误;
        /// 50002	REQ_UNAVAILABLE 不支持该请求;
        /// 50003	REQ_NOT_ACCEPTABLE 该请求不可接受
        /// </summary>
        public int code { get; set; }
        public int create_on { get; set; }
        public string msg { get; set; }
    }

    public class ResponseBaseWithStringCreateOn : ResponseBase
    {

        public new string create_on { get; set; }
    }
}
