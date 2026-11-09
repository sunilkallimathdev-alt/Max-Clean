using API.Models;
using MaxCleanWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MaxCleanWeb.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            #region Common Start Module that intitiates and sets up the user controls.
            var currentUser = (Customer_Model)Session["CurrentUser"];

            if (Session["RegistrationDone"] == "yes" && currentUser == null)
            {
                ViewBag.RegTtl = "Registration Failed. please try again.";
            }
            else
            {
                ViewBag.RegTtl = "Registration";
            }

            if (currentUser != null)
            {
                ViewBag.UserName = currentUser.CustomerName;
                Session["LoginAttempt"] = "";
            }
            else
            {
                if (Session["LoginAttempt"] == ">0")
                {
                    ViewBag.UserName = "Login Failed. Please try again.";
                }
                else
                {
                    ViewBag.UserName = "Login";
                }
            }
            ViewBag.URL = Static_Data.BaseURL;
            #endregion
            return View();
        }

        public dynamic Enquiry(ContactUs_Model value)
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var res = JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "ContactUs", "POST", JsonConvert.SerializeObject(value)));

            return JsonConvert.SerializeObject(res);
          
        }
    }
}