using API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static MaxCleanWebV1.Models.StaticData;

namespace MaxCleanWebV1.Controllers
{
    public class MaxCleanController : Controller
    {
        // GET: MaxClean


        public ActionResult Home()
        {
            WebClient client = new WebClient();
            var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Banner"));
            ViewBag.Banners = res["ObjectResult"].ToObject<List<Banner_Model>>();

            if (Session["Users"] == null)
            {
                Session["login"] = "False";
                ViewBag.Header = 1;
            }

            else
                ViewBag.Header = 2;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
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

        public JsonResult RegisterContactUsDetails(string name, string mobile, string email, string msg)
        {
            try
            {
                ContactUs_Model oContactUs_Model = new ContactUs_Model();
                oContactUs_Model.ContactName = name;
                oContactUs_Model.MobileNumber = mobile;
                oContactUs_Model.EmailID = email;
                oContactUs_Model.Query = msg;
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";


                //var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "ContactUs", "POST", Objcontactus));
                //return res;

                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "ContactUs", "POST", JsonConvert.SerializeObject(oContactUs_Model)));

                //var result = JsonConvert.SerializeObject(res);

                int result = Convert.ToInt32(res["StatusCode"]);

                if (result == 200)
                    return Json(new { result = "Success" });
                else
                    return Json(new { result = "NotFound" });
            }
            catch (Exception)
            {

                return null;
            }
        }
        
        public dynamic LoginDetails(string userName, string password)
        {
            try
            {
                Customer_Model oCustomer_Model = new Customer_Model();
                oCustomer_Model.MobileNumber = userName;
                oCustomer_Model.Password = password;
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var result = client.UploadString(Static_Data.BaseURL + "CustomerLogin", "POST", JsonConvert.SerializeObject(oCustomer_Model));
                var res = (JObject)JsonConvert.DeserializeObject(result);
                int ststuscode = Convert.ToInt32(res["StatusCode"]);
                if (ststuscode == 200)
                {
                    //DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(BlogSite));
                    //BlogSite bsObj2 = (BlogSite)deserializer.ReadObject(ms);
                    // Customer_Model loginCustomer_Model = JsonConvert.DeserializeObject<Customer_Model>(result);
                    Customer_Model loginCustomer_Model = res["ObjectResult"].ToObject<Customer_Model>();
                    Session.Clear();
                    Session.RemoveAll();
                    Session["Users"] = loginCustomer_Model;
                    Session["login"] = "True";
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic RegisterCustomer(string ObjCustomer)
        {

            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Customer", "POST", ObjCustomer));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult Logout()
        {
            Session["Users"] = "";
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Home");
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
                var result = (JObject)res;
                Otp obj = new Otp();
                if (result["StatusCode"].ToString() == "200")
                {
                    obj.StatusCode =Convert.ToInt32(result["StatusCode"]);
                    obj.ObjectResult = 2;
                }
                else
                {
                    obj.StatusCode = Convert.ToInt32(result["StatusCode"]);
                    obj.ObjectResult = -1;
                }
                Session["OTP"] = result["ObjectResult"] + "_" + Mobile;
                return JsonConvert.SerializeObject(obj);
            }
            else
            {
                return null;
            }


        }
        public class Otp
        {
            public int StatusCode { get; set; }
            public int ObjectResult { get; set; }
        }
        public dynamic Verify_OTP(string otp, string mobile)
        {
            try
            {
                Otp obj = new Otp();
              
                string otp_mobile = Session["OTP"].ToString();
                string myString = otp_mobile;
                string[] splitString = myString.Split('_');
                if (splitString[0] == otp && splitString[1] == mobile)
                {
                    obj.StatusCode = 200;
                    return JsonConvert.SerializeObject(obj);
                }
                else
                {
                    obj.StatusCode = 404;
                    return JsonConvert.SerializeObject(obj);
                }
                
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public dynamic verifyPincode()
        {



            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Pincode/0/0"));
            return JsonConvert.SerializeObject(res);



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