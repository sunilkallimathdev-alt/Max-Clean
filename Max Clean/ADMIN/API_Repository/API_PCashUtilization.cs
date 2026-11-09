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
    public class API_PCashUtilization
    {
        public dynamic GetPCashUtilizationList(string Store_Id)
        {
            try
            {
               

                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "PCashUtilization" ,"PUT", Store_Id));
                return res["ObjectResult"].ToObject<List<PCashUtilzation_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //PCashUtilzation_Model
        public dynamic GetActiveStoreList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Store/0/0"));
                return res["ObjectResult"].ToObject<List<PCashUtilzation_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}