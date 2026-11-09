using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ADMIN.API_Repository
{
    public class API_BatchVendor
    {
        public dynamic GetStoreStock(long BatchID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "BatchVendor/" + BatchID));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddBatchStock(string ObjBatch)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "BatchVendor", "POST", ObjBatch));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}