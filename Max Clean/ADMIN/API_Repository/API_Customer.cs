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
    public class API_Customer
    {
        public dynamic GetCustomerList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Customer"));
                return res["ObjectResult"].ToObject<List<Customer_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetCustomerOrderList(int CustomerID)
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Customer/"+ CustomerID + "/0/0/0"));
                return res["ObjectResult"].ToObject<List<PurchaseOrder_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic AddCustomer(string Objcustomer)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Customer", "POST", Objcustomer));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateCustomer(string Objcustomer)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Customer", "PUT", Objcustomer));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteCustomer(int CustomerId)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Customer/" + CustomerId, "DELETE", ""));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetCustomer(int CustomerId)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Customer/" + CustomerId));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetNonPerformingCustomer()
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Customer/0/0/0" ));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetCustomerByDate(string Objcustomer)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Customer_PlaceOrder", "PUT", Objcustomer));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetCustomerId(string Customer)
        {
            Customer_Model Id = new Customer_Model();
            try
            {
             var cst=   (JObject)JsonConvert.DeserializeObject(Customer);
              List<Customer_Model> obj =  GetCustomerList();
                foreach(var x in obj)
                {
               
                  
                    if (x.MobileNumber == (string)cst.SelectToken("MobileNumber"))
                    {
                        Id.CustomerID = x.CustomerID;
                        Id.CustomerName = x.CustomerName;
                        Id.MobileNumber = x.MobileNumber;
                        Id.EmailID = x.EmailID;
                        Id.CustomerRewardPoints = x.CustomerRewardPoints;
                        Id.Address = x.Address;
                        Id.OldBalance = x.OldBalance;
                    }
                }
               
                return Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}