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
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult ProfilePage()
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


            Models.User_Model proc = new Models.User_Model();
            if (Session["CurrentUser"] == null)
            {

                return RedirectToAction("Index", "Home");

            }

            return View();
        }

        public dynamic ProfileUpdate(FormCollection userData)
        {


            Customer_Model user = new Customer_Model();
            Models.User_Model proc = new Models.User_Model();

            user.Address = userData["ProAddr"];
            user.AltMobileNumber = userData["ProAltNum"];
            user.CustomerID = ((Customer_Model)Session["CurrentUser"]).CustomerID;
            user.CustomerName = userData["ProName"];
            user.EmailID = userData["ProEmail"];
            user.MobileNumber = userData["ProMobile"];
            user.Password = userData["ProPwd"];
            var res = proc.UpdateCustomerData(user);

            if (res == 200)
            {
                TempData["UpdateResponse"] = "Successfully update your details";
            }
            else
            {
                TempData["UpdateResponse"] = "Couldnt update your information. Please try again later.";
            }

            return RedirectToAction("ProfilePage");


        }

        public dynamic ForgotPassword(string EmailID)
        {
            Customer_Model User = new Customer_Model();
            User.EmailID = EmailID;

            try
            {
                if (EmailID != "" && EmailID != null)
                {
                    var Packedmail = JsonConvert.SerializeObject(User);

                    WebClient client = new WebClient();
                    client.Headers[HttpRequestHeader.Accept] = "application/json";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var res = JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "CustomerLogin", "PUT", Packedmail));
                    return JsonConvert.SerializeObject(res);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}