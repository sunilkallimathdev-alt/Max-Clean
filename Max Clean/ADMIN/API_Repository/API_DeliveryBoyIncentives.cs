using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ADMIN.API_Repository
{
    public class API_DeliveryBoyIncentives
    {
        public dynamic AddDeliveryBoyIncentives(string ObjDeliveryBoyIncentives)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "DeliveryBoyIncentives", "POST", ObjDeliveryBoyIncentives));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetAllDeliveryBoyIncentivesByDeliveryBoyID(int DeliveryBoyID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "DeliveryBoyIncentives/" + DeliveryBoyID));
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
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "DeliveryBoyIncentives", "POST", strpay));
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
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "DeliveryBoyIncentives/" + RequesOrderID + "/0"));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}