using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        public ActionResult Index()
        {
            if (Session["logindetails"] != null)
                Session.RemoveAll();
            return View();
        }
        public dynamic GetForgotPassword(string EmailID)
        {
            try
            {
                API_Login ObjProduct = new API_Login();
                return JsonConvert.SerializeObject(ObjProduct.GetForgotPassword(EmailID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic AddLogin(string ObjAdmin)
        {
            try
            {
                API_Login Objpro = new API_Login();
                var Res = Objpro.AddLogin(ObjAdmin);
                if (Res["StatusCode"] == 200)
                {
                    
                    var abc = Res["ObjectResult"];
                    var dc = abc["objadmin"];
                    Session["logindetails"] = dc;
                    

                }
                return JsonConvert.SerializeObject(Res);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}