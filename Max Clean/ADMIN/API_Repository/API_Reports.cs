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
    public class API_Reports
    {
        public dynamic GetOrders(string obj)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Reports", "POST", obj));
                return res["ObjectResult"].ToObject<List<PurchaseOrder_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetInventory(int obj)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Reports/"+  obj));
                return res["ObjectResult"].ToObject<List<PurchaseOrder_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetInventoryManagement(int obj1, int obj2)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Reports/" + obj1+"/" +obj2));
                return res["ObjectResult"].ToObject<List<PurchaseOrder_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}