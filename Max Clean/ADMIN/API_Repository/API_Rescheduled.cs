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
    public class API_Rescheduled
    {

        public dynamic Rescheduled(string Obj)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Reschedule", "PUT", Obj));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic ChangeStore(string obj)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Reschedule", "POST", obj));
                return res;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}