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
    public class API_Subscriber
    {
        public dynamic GetSubcriberList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Subscriber"));
                return res["ObjectResult"].ToObject<List<Subscriber_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetActiveRoleList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Subscriber/0/0"));
                return res["ObjectResult"].ToObject<List<Role_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic AddSubcriber(string Objsubscriber)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Subscriber", "POST", Objsubscriber));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public dynamic UpdateSubcriber(string Objsubscriber)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Subscriber", "PUT", Objsubscriber));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public dynamic DeleteSubcriber(int SubscriberID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Subscriber/" + SubscriberID, "DELETE", ""));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetSubcriber(int SubscriberID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Subscriber/" + SubscriberID));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}