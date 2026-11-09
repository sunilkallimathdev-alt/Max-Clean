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
    public class API_PurchaseOrder
    {

        public dynamic Getsortedlist(string length, string start, string sortColumn, string sortColumnDir, string draw, string search,long ID,string FromDate,string ToDate)
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "PurchaseOrder/" + length + "/" + start + "/" + "/" + sortColumn + "/" + sortColumnDir + "/" + draw + "/" + search+"/"+ID+"/"+ FromDate + "/"+ ToDate));
                return res["ObjectResult"].ToObject<Base_Model>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddPurchaseOrder(string ObjPurchaseOrder)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "PurchaseOrder", "POST", ObjPurchaseOrder));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public dynamic PayBalance(string ObjPurchaseOrder)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "PayBalance", "POST", ObjPurchaseOrder));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public dynamic UpdatePurchaseOrder(string ObjPurchaseOrder)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "PurchaseOrder", "PUT", ObjPurchaseOrder));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public dynamic UpdateStatus(string ObjPurchaseOrder)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "PayBalance", "PUT", ObjPurchaseOrder));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public dynamic UpdateOrder(string ObjPurchaseOrder)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "PickUpOrders", "PUT", ObjPurchaseOrder));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeletePurchaseOrder(string ObjPurchaseOrder)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "PurchaseOrder", "DELETE", ObjPurchaseOrder));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetOrderByID(long OrderByID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "PurchaseOrder/" + OrderByID));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetAllOnlineOrder()
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "PurchaseOrder"));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetAllOnlinestoreOrder(bool? IsOutletorder)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "PurchaseOrder/"+ IsOutletorder + "/0"));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetAllOrderList()
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "PurchaseOrder/0/0/0"));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetAllOrderByStoreID(long ID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "PurchaseOrder/"+ ID + "/0/0/0"));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetAllOrders(string Obj)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "GetOrdersbyDate", "POST", Obj));

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}