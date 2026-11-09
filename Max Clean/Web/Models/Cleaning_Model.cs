using API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MaxCleanWeb.Models
{
    public class Cleaning_Model
    {

        public dynamic GetActiveService()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Service/0/0"));
                return res["ObjectResult"].ToObject<List<Service_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetCategories(int ServID)
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Category/"+ServID+"/0/0"));
                return res["ObjectResult"].ToObject<List<Shop_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}