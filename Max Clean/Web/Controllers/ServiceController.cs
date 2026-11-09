using API.Models;
using MaxCleanWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaxCleanWeb.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
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
    }
}