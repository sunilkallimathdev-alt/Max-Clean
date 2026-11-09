using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using API.Models;

namespace ADMIN.API_Repository
{
    public class API_GetOrderCancel
    {
       public dynamic GetOrderCancelList(string store)
        {
            var Store = JsonConvert.DeserializeObject<OrderCancel_Model>(store);
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "CancelOrder/"+Store.store_id));
                return res["ObjectResult"].ToObject<List<OrderCancel_Model>>();
            }
            catch (Exception)
            {

                throw;
            }
          
        }
        //public dynamic Getsortedlist(string length, string start, string sortColumn, string sortColumnDir, string draw, string search, long ID, string FromDate, string ToDate)
        //{
        //    try
        //    {
        //        WebClient client = new WebClient();
        //        var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "PurchaseOrder/" + length + "/" + start + "/" + "/" + sortColumn + "/" + sortColumnDir + "/" + draw + "/" + search + "/" + ID + "/" + FromDate + "/" + ToDate));
        //        return res["ObjectResult"].ToObject<Base_Model>();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}