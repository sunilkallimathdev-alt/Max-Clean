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
    public class Order_Model
    {
      public dynamic GetCustomerOrder(int id)
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "GetOrderByCustomerID/" + id));
                return res["ObjectResult"].ToObject<List<CustmoerOrders_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}