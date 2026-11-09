using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ADMIN.API_Repository
{
    public class API_PurchasePayment
    {
        public dynamic GetRequestOrdersByVendorID(long VendorID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "VendorProductOrderPayment/" + VendorID));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic SendInvoiceMail(long RequesOrderID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "VendorProductOrderPayment/" + RequesOrderID + "/0"));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic MakePayment(string strpay)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "VendorProductOrderPayment", "POST", strpay));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}