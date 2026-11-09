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
    public class API_Category
    {
        
        public dynamic GetCategoryList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Category"));
                return res["ObjectResult"].ToObject<List<Category_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic Getsortedlist(string length, string start, string sortColumn, string sortColumnDir, string draw,string search)
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Category/"+length+"/"+start+"/"+"/"+sortColumn+"/"+sortColumnDir+"/"+draw+"/"+ search));
                return res["ObjectResult"].ToObject<Base_Model>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetActiveCategory()
        {
            try
            {
                WebClient client = new WebClient();
         
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Category/0/0"));
                return res["ObjectResult"].ToObject<List<Category_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetCategoryByServiceID(int ServiceID)
        {
            try
            {
                WebClient client = new WebClient();

                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Category/"+ ServiceID + "/0/0"));
                return res["ObjectResult"].ToObject<List<Shop_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddCategory(string Objcategory)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Category", "POST", Objcategory));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateCategory(string Objcategory)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Category", "PUT", Objcategory));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic DeleteCategory(int CategoryId)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Category/" + CategoryId, "DELETE", ""));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetCategory(int CategoryId)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Category/" + CategoryId));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetCategoryProductsByServiceID(int ServiceID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Category/"+ ServiceID + "/0/0"));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}