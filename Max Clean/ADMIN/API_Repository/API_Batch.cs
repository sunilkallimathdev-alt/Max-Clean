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
    public class API_Batch
    {
        public dynamic GetBatchesList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Batch"));
                return res["ObjectResult"].ToObject<List<Batch_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetYettostartBatchesList()
        {
            try
            {
                WebClient client = new WebClient();
                return (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Batch/0/0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddBatch(string ObjBatch)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Batch", "POST", ObjBatch));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}