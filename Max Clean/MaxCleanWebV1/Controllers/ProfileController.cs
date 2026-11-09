using API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static MaxCleanWebV1.Models.StaticData;

namespace MaxCleanWebV1.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            if (Session["Users"] == null)
            {
                RedirectToAction("");
            }

            return View("MyProfile");
        }

        public dynamic updateuserprofile(string ObjCustomer)
        {

            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var result = client.UploadString(Static_Data.BaseURL + "Customer", "PUT", ObjCustomer);
                var res = (JObject)JsonConvert.DeserializeObject(result);
                int ststuscode = Convert.ToInt32(res["StatusCode"]);
                if (ststuscode == 200)
                {
                    Customer_Model loginCustomer_Model = res["ObjectResult"].ToObject<Customer_Model>();
                    Session["Users"] = loginCustomer_Model;
                }


                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult Myrewards()
        {
            if (Session["Users"] == null)
            {
                RedirectToAction("");
            }


            return View("MyRewards");
        }
    }
}