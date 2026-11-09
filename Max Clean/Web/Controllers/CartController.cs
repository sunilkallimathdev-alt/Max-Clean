using API.Models;
using MaxCleanWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaxCleanWeb.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Cart()
        {
            ViewBag.IsOtherResp = true;
            if (Session["CurrentUser"] is Customer_Model)
            {
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


                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
    }
}