using API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.API_Repository
{
    public class API_PettyCash
    {
        public dynamic GetPettyCashList(string Store)
        {
            try
            {
                var store = JsonConvert.DeserializeObject<Expenses_Model>(Store);
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "PettyCash/"+store.Store_Id ));
                return res["ObjectResult"].ToObject<List<PettyCash_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public dynamic GetActiveStoreList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Store/0/0"));
                return res["ObjectResult"].ToObject<List<PettyCash_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public dynamic AddPettyCash(string ObjPCash)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "PettyCash", "POST", ObjPCash));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public dynamic Getsortedlist(string length, string start, string sortColumn, string sortColumnDir, string draw, string search)
        //{
        //    try
        //    {
        //        WebClient client = new WebClient();
        //        var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "PettyCash/" + length + "/" + start + "/" + "/" + sortColumn + "/" + sortColumnDir + "/" + draw + "/" + search));
        //        return res["ObjectResult"].ToObject<Base_Model>();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}