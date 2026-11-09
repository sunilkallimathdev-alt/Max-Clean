using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ADMIN.API_Repository
{
    public class API_Vendor
    {
        public dynamic GetVendorList()
        {
            try
            {
                using (var client = new WebClient())
                {
                    var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Vendor"));
                    return res["ObjectResult"].ToObject<List<Vendor_Model>>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddVendor(string Objvendor)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.Accept] = "application/json";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Vendor", "POST", Objvendor));
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic UpdateVendor(string Objvendor)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.Accept] = "application/json";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Vendor", "PUT", Objvendor));
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteVendor(int VendorID)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.Accept] = "application/json";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Vendor/" + VendorID, "DELETE",""));
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetVendor(int VendorID)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.Accept] = "application/json";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Vendor/" + VendorID));
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}