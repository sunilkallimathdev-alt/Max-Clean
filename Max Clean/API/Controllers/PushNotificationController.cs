using API.CommonClasses;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;

namespace API.Controllers
{
    public class PushNotificationController : ApiController
    {
        
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();
        
        // POST: api/PushNotification
        [HttpPost]
        public async void  Post([FromBody]PushNotification_Model obj)
        {

            try
            {
                List<string> lstDeviceIds = new List<string>();
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    lstDeviceIds = Context.t_deliveryboy_deviceid.Select(x => x.deviceId).ToList();
                }
                
                foreach (string strDI in lstDeviceIds)
                {
                    SendPushNotification(obj.Title, obj.Body, strDI);
                    //NotifyAsync(obj.Title, obj.Body, strDI);
                }
                ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(8);

                ObjSuccessResponce_Model.ReceivedParameters = obj;

                //return ObjSuccessResponce_Model;
            }   
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                ObjErrorResponce.ReceivedParameters = obj;
               // return ObjErrorResponce;
            }

        }


        //public async Task<bool> NotifyAsync(string title, string body, string deviceID)
        public bool SendPushNotification(string title, string body, string deviceID)
        {
            try
            {
                //var applicationID = "AIzaSyBHXYfdbN1m3SAqDDzIUqHBH1LiXmShYI0";
               // var senderId = "551447307117";
                //var applicationID = "AIzaSyAXuLCJ3iK4ro9n07tCH_WOtv9bhYwb2qw";
                //    var senderId = "362496901052";

                var applicationID = "AAAAgNPalmQ:APA91bEMGUUZtHegz03j2bboG9qzsH6LRbYZ_YScOHE6jLHvg2Ou3mWRXoXy2SBMxHgnuKqD3QsLKROZgmKtrzxHkovr76UEhOuyf5yfGW2lreUiZ2ZmKL3X_Qvde5zgMD-hVNo2HgSCMubZeb_O6QAzdD9fj68Qvw";
                var senderId = "553310131812";
                // string deviceId = "/topics/all"; //"device_id_of_reciever";
                //string deviceId = "d8W9BS3dNvc:APA91bE6k-lkMg1H5qNRAiFGgDUeDg3XI7z8z0jZtUmRNlniBgJaNXbJI8QDyQE3kG2IEmKLwe-E_mpg7ygB_XSi3gwqhbO__fY0hcI4xquhRN1vh__Cugqw_oFTqDZs5QOP8r-fg14lS0Euv3WreGVkl8SH3jUSpA";
                //string deviceId = "ccglVRyvpOk:APA91bFmhSQvi_OhCe_wviEvw7LgXIOgXz5lBvwsU79JIklKLLIttTyzYbj6MBafsAcf_QAOeRKWCcRCNmE7sj3SMCcCQj04UlpaVwpIT59P64JyRq4k6u9_VXStajLoul7b6N40AGixNXqNLz-p_HKd3lHDkK-vwA"; 
                string deviceId = deviceID;

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "POST";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = body,
                        title = title,
                        icon = "myicon"
                    },
                    priority = 10
                };

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String sResponseFromServer = tReader.ReadToEnd();
                            //Response.Write(sResponseFromServer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
                //Response.Write(ex.Message);
            }
            
            return true;
        }

    }
}
