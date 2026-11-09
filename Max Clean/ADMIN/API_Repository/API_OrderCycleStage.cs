using API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ADMIN.API_Repository
{
    public class API_OrderCycleStage
    {


        public dynamic GetOrderCycleList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "OrderCycle"));
                return res["ObjectResult"].ToObject<List<orderCycle_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetActiveOrderCycleList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "OrderCycle/0/0"));
                return res["ObjectResult"].ToObject<List<orderCycle_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic AddOrderCycle(string ObjOrderCycle)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "OrderCycle", "POST", ObjOrderCycle));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public dynamic UpdateOrderCycle(string ObjOrderCycle)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "OrderCycle", "PUT", ObjOrderCycle));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public dynamic DeleteOrderCycle(int OrderCycleID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "OrderCycle/" + OrderCycleID, "DELETE", ""));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetOrderCycleID(int OrderCycleID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "OrderCycle/" + OrderCycleID));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}