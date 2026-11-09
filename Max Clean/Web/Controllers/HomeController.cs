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
using System.Net;
using API.CommonClasses;

namespace MaxCleanWeb.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(API.Models.User_Model usr)
        {
            ViewBag.Header = "1"; //Activate First header. (There are two types of headers depending upon the page being visited.)
            #region Common Start Module that intitiates and sets up the user controls.
            var currentUser = (Customer_Model)Session["CurrentUser"];

            if (Session["RegistrationDone"] == "no" && currentUser == null)
            {
                ViewBag.RegTtl = "Registration Failed. Number already registered.";
            }
            else
            {
                ViewBag.RegTtl = "Registration";
            }

            if (currentUser != null)
            {
                ViewBag.UserName = currentUser.CustomerName;
                Session["Mobile"] = ((Customer_Model)Session["CurrentUser"]).MobileNumber;
                Session["Email"] = ((Customer_Model)Session["CurrentUser"]).EmailID;
                Session["IsMobileVerified"] = currentUser.isMobileVerified;
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

        public dynamic LoginUser(FormCollection user)
        {

            try
            {

                Session["LoginAttempt"] = ">0";
                Customer_Model usr = new Customer_Model();
                usr.EmailID = user["EmailID"];
                usr.Password = user["Password"];

                if (user.AllKeys.Contains("ResEmailID"))
                {
                    usr.EmailID = user["ResEmailID"];
                    usr.Password = user["ResPassword"];

                }

                Models.User_Model proc = new Models.User_Model();
                var Res = proc.AddLogin(usr);

                if (Res["StatusCode"] == 200)
                {
                    Customer_Model data = new Customer_Model();
                    data = Res["ObjectResult"].ToObject<Customer_Model>();
                    Session["CurrentUser"] = data;
                }


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //  log(ex.Message+" StackTrace: "+ex.StackTrace);
                throw ex;
            }
        }

        public dynamic RegisterUser(FormCollection User)
        {
            Customer_Model NewCus = new Customer_Model();

            
            if (User["ResEmailID"] == null)
            {
                NewCus.Address = User["RAddr"];
                NewCus.AltMobileNumber = User["RAltMobile"];
                NewCus.CustomerName = User["RName"];
                NewCus.EmailID = User["REmailID"];
                NewCus.Password = User["RPassword"];
                NewCus.MobileNumber = User["RMobile"];
            }
            else
            {
                NewCus.Address = User["ResAddr"];
                NewCus.AltMobileNumber = User["ResAltMobile"];
                NewCus.CustomerName = User["ResName"];
                NewCus.EmailID = User["ResEmailID"];
                NewCus.Password = User["ResPassword1"];
                NewCus.MobileNumber = User["ResMobile"];
            }

            Session["NewCus"] = NewCus;
         


                if (string.IsNullOrEmpty(NewCus.EmailID) || string.IsNullOrEmpty(NewCus.Password) || string.IsNullOrEmpty(NewCus.CustomerName) || string.IsNullOrEmpty(NewCus.AltMobileNumber) || string.IsNullOrEmpty(NewCus.Address))
                {
                return false ;
                }
                Models.User_Model proc = new Models.User_Model();
                var Res = proc.RegisterCusAPICall(NewCus);

                if (Res["StatusCode"] == 200)
                {
                    NewCus.CustomerID = Res["Result"];
                    Session["CurrentUser"] = NewCus;
                    Session["RegistrationDone"] = "yes";
                }
                else
                {
                    Session["RegistrationDone"] = "no";
                }
            SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
            ErrorResponce ObjErrorResponce = new ErrorResponce();

            return JsonConvert.SerializeObject(true); 
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public void log(string logdata)
        {
            using (StreamWriter writer = new StreamWriter(Server.MapPath("/ExepLOGS.txt"), true))
            {
                writer.WriteLine(logdata);
            }
        }

        public dynamic SendOTP(string Mobile)
        {

            if (Mobile != "")
            {
                var PackedMobile = JsonConvert.SerializeObject(Mobile);
             
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "OTP", "POST", PackedMobile));
                return JsonConvert.SerializeObject(res);
            }
            else
            {
                return null;
            }


        }
        
        public dynamic confirmOTP(string Mobile)
        {    
                    Customer_Model data = new Customer_Model();
                    data = (Customer_Model)Session["CurrentUser"];
                    var PackedMobile = JsonConvert.SerializeObject(data);
                    WebClient client = new WebClient();
                    client.Headers[HttpRequestHeader.Accept] = "application/json";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var res = JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "OTP", "PUT", PackedMobile));
                    Session["OTP"] = res;
                    return JsonConvert.SerializeObject(res);             
         
        }

        }
}