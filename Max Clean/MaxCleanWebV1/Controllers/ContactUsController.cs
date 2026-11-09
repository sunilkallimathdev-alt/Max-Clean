using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static MaxCleanWebV1.Models.StaticData;

namespace MaxCleanWebV1.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: ContactUs
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult registerdetails(string name, string mobile, string email, string msg)
        {
            try
            {
                ContactUs_Model oContactUs_Model = new ContactUs_Model();
                oContactUs_Model.ContactName = name;
                oContactUs_Model.MobileNumber = mobile;
                oContactUs_Model.EmailID = email;
                oContactUs_Model.Subject = msg;
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                
                var res = JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "ContactUs", "POST", JsonConvert.SerializeObject(oContactUs_Model)));

                var result= JsonConvert.SerializeObject(res);


              
                if (result != null)   
                    return Json(new { result = "Success" });
                else
                    return Json(new { result = "NotFound" });
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}