using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class PCashUtilizationController : Controller
    {
        // GET: PCashUtilization
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            API_Store Objstore = new API_Store();
            ViewBag.StoreList = Objstore.GetStoreList();
            return View();
        }

        public dynamic GetPCashUtilizationList(string Store_Id)
        {
            try
            {
                API_PCashUtilization Objpcash = new API_PCashUtilization();
                return JsonConvert.SerializeObject(Objpcash.GetPCashUtilizationList(Store_Id));


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
    }
}