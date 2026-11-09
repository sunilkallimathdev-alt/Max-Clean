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
    public class API_GetOrderRequests
    {
        public dynamic GetOrderCancelRequestList(string store)
        {
             
            var Store = JsonConvert.DeserializeObject<OrderCancel_Model>(store);
            try
            {
                WebClient client = new WebClient();
 
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "GetOrderRequestList/" + Store.store_id ));
                return res["ObjectResult"].ToObject<List<OrderCancel_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}