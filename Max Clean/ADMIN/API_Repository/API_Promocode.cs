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
    public class API_Promocode
    {

        public dynamic Getsortedlist(string length, string start, string sortColumn, string sortColumnDir, string draw, string search)
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Promocode/" + length + "/" + start + "/" + "/" + sortColumn + "/" + sortColumnDir + "/" + draw + "/" + search));
                return res["ObjectResult"].ToObject<Base_Model>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetActivePromocode()
        {
            try
            {
                WebClient client = new WebClient();

                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Promocode/0/0"));
                return res["ObjectResult"].ToObject<List<Promocode_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public dynamic AddPromocode(string ObjPromocode)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Promocode", "POST", ObjPromocode));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic ValidatePromocode(string ObjPromocode)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "PromocodeValidate", "POST", ObjPromocode));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic UpdatePromocode(string ObjPromocode)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Promocode", "PUT", ObjPromocode));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic DeletePromocode(int PromocodeId)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Promocode/" + PromocodeId, "DELETE", ""));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetPromocode(int PromocodeId)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Promocode/" + PromocodeId));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}