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
    public class API_ContactUs
    {
        public dynamic GetContactUsList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "ContactUs"));
                return res["ObjectResult"].ToObject<List<ContactUs_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic AddContactUs(string Objcontactus)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "ContactUs", "POST", Objcontactus));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateContactUs(string Objcontactus)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "ContactUs", "PUT", Objcontactus));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic DeleteContactUs(int ContactUsID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "ContactUs/" + ContactUsID, "DELETE", ""));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetContactUs(int ContactUsID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "ContactUs/" + ContactUsID));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}