using API.CommonClasses;
using API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace MaxCleanWeb.Models
{
    public class User_Model
    {
        public dynamic AddLogin(Customer_Model data)
        {

            string objAdmin = JsonConvert.SerializeObject(data);
            try
            {
             
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "CustomerLogin", "POST", objAdmin));

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic RegisterCusAPICall(Customer_Model data)
        {

            string objAdmin = JsonConvert.SerializeObject(data);
            try
            {

                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Customer", "POST", objAdmin));

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Customer_Model GetCustomerData(int id)
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Customer/" + id));
                return res["ObjectResult"].ToObject<Customer_Model>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateCustomerData(Customer_Model cus)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
          
                string objAdmin = JsonConvert.SerializeObject(cus);
        
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Customer" ,"PUT", objAdmin));
                return res["StatusCode"].ToObject<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
     }

    }
}