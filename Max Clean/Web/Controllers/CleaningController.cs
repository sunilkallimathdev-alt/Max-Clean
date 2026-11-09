using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using API.Models;
using Newtonsoft.Json;
using MaxCleanWeb.Models;
using Newtonsoft.Json.Linq;
using System.IO;

namespace MaxCleanWeb.Controllers
{
    public class CleaningController : Controller
    {
        // GET: Cleaning
        public ActionResult Index()
        {
            ViewBag.IsOtherResp = true;
            //Menu Module start -- 
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
            //Menu Module End --
            
            Cleaning_Model proc = new Cleaning_Model();
            ViewBag.Service = proc.GetActiveService();
           
            return View();
        }


        public dynamic GetCategories(int id)
        {
            Cleaning_Model proc = new Cleaning_Model();
            return Json(proc.GetCategories(id));
        }
    }
}