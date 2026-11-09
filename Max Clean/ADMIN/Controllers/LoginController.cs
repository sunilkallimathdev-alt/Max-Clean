using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public dynamic Login(API_Role.Admin objlogin)
        {
            try
            {
                API_Role Objcustomer = new API_Role();
                return JsonConvert.SerializeObject(Objcustomer.login(objlogin));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}