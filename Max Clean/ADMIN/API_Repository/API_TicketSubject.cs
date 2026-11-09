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
    public class API_TicketSubject
    {


        public dynamic  GetTicketSubjectList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "TicketSubject"));
                return res["ObjectResult"].ToObject<List<TicketSubject_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic AddTicketSubject(string Objticketsubject)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "TicketSubject", "POST", Objticketsubject));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic UpdateTicketSubject(string Objticketsubject)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "TicketSubject", "PUT", Objticketsubject));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteTicketSubject(int TicketSubjectID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "TicketSubject/" + TicketSubjectID, "DELETE", ""));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetTicketSubject(int TicketSubjectID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "TicketSubject/" + TicketSubjectID));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}