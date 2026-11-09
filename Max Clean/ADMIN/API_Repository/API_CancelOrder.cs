using API.CommonClasses;
using API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System; 
using System.Net;
using System.Web;
 

namespace ADMIN.API_Repository
{
    public class API_CancelOrder  
    {
        
        // GET: API_CancelOrder
        public dynamic UpdateOrder(string Order)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json"; 

                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "CancelOrder", "PUT", Order));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic Getsortedlist(string length, string start, string sortColumn, string sortColumnDir, string draw, string search, long ID, string FromDate, string ToDate)
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "CancelOrder/" + length + "/" + start + "/" + "/" + sortColumn + "/" + sortColumnDir + "/" + draw + "/" + search + "/" + ID + "/" + FromDate + "/" + ToDate));
                return res["ObjectResult"].ToObject<Base_Model>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}