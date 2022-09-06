using GangHaoAGV.Models.Order;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GangHaoAGV.API
{
    public class ServerAPI
    {
        public string baseUrl;

        /// <summary>
        /// 創建訂單
        /// </summary>
        /// <param name="setOrderModel"></param>
        /// <returns></returns>
        public async Task<ResponseBase> SetOrder(SetOrder setOrderModel)
        {
            string json = await HttpPost("setOrder", setOrderModel);
            ResponseBase response = JsonConvert.DeserializeObject<ResponseBase>(json);
            return response;
        }

        /// <summary>
        /// 查詢訂單狀態
        /// </summary>
        /// <param name="orderID"></param>
        public async Task<OrderDetails> QueryOrderState(string orderID)
        {
            string json = await HttpGet($"orderDetails/{orderID}");
            return JsonConvert.DeserializeObject<OrderDetails>(json);
        }

        private async Task<string> HttpPost(string apiRoute, object data)
        {
            string returnJson = "{}";
            if (baseUrl == null)
                throw new Exception("Server Url is null");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                string apiRouteFullPath = String.Join("/", baseUrl, apiRoute);
                StringContent httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiRouteFullPath, httpContent);
                returnJson = await response.Content.ReadAsStringAsync();
            }

            return returnJson;
        }
        private async Task<string> HttpGet(string path)
        {
            string returnJson = "{}";

            if (baseUrl == null)
                throw new Exception("Server Url is null");
            using (HttpClient client = new HttpClient())
            {
                string apiRouteFullPath = String.Join("/", baseUrl, path);
                HttpResponseMessage response = await client.GetAsync(apiRouteFullPath);
                returnJson = await response.Content.ReadAsStringAsync();
            }
            return returnJson;
        }

    }
}
