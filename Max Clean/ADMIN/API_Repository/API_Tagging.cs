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
    public class API_Tagging
    {

        public dynamic GetTagsList(string length, string start, string sortColumn, string sortColumnDir, string draw, string search)
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Tagging/" + length + "/" + start + "/" + "/" + sortColumn + "/" + sortColumnDir + "/" + draw + "/" + search));
                return res["ObjectResult"].ToObject<Base_Model>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic Update(string ObjTag)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Tagging", "PUT", ObjTag));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetTagbyid(long id)
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Tagging/"+id));
                return res["ObjectResult"].ToObject<Tag_Model>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetOrderList_byStatusID(int StatusID)
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Tagging/" + StatusID+"/0"));
                return res["ObjectResult"].ToObject<List<PurchaseOrder_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic UpdateOrderStatus(string ObjOrders)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Tagging", "POST", ObjOrders));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}