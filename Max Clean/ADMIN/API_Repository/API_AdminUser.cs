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
    public class API_AdminUser
    {
        public dynamic GetAdminUserList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "AdminUser"));
                return res["ObjectResult"].ToObject<List<AdminUser_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic AddAdminUser(string Objadminser)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "AdminUser", "POST", Objadminser));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateAdminUser(string Objadminuser)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "AdminUser", "PUT", Objadminuser));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic DeleteAdminUser(int UserID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "AdminUser/" + UserID, "DELETE", ""));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetAdminUser(int UserID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "AdminUser/" + UserID));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetActiveAdminUser()
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "AdminUser/0/0"));
                return res["ObjectResult"].ToObject<List<AdminUser_Model>>();
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}