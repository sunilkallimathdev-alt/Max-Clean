using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static MaxCleanWebV1.Models.StaticData;
using API.Models;
using Newtonsoft.Json;

namespace MaxCleanWebV1.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Service()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult login()
        {
            return PartialView("Login.cshtml");
        }
        [HttpPost]
        public JsonResult login(string userName, string password)
        {
            return null;
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

                var result = JsonConvert.SerializeObject(res);



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