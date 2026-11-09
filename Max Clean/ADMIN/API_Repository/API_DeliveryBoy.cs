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
    public class API_DeliveryBoy
    {

        public dynamic GetDeliveryBoyList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "DeliveryBoy"));
                return res["ObjectResult"].ToObject<List<DeliveryBoy_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetActiveDeliveryBoyList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "DeliveryBoy/0/0"));
                return res["ObjectResult"].ToObject<List<DeliveryBoy_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic AddDeliveryBoy(string ObjDeliveryBoy)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "DeliveryBoy", "POST", ObjDeliveryBoy));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public dynamic UpdateDeliveryBoy(string objDeliveryBoy)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "DeliveryBoy", "PUT", objDeliveryBoy));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public dynamic DeleteDeliveryBoy(int DeliveryBoyID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "DeliveryBoy/" + DeliveryBoyID, "DELETE", ""));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetDeliveryBoyID(int DeliveryBoyID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "DeliveryBoy/" + DeliveryBoyID));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}