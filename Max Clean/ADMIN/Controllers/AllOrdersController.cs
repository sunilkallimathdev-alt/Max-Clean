using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class AllOrdersController : Controller
    {
        // GET: PurchaseOrder2
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            return View();
        }
        public ActionResult paymentdetails()
        {
            return View();
        }
        public dynamic GetAllOrders(string Obj)
        {
            try
            {
                API_PurchaseOrder Objallorders = new API_PurchaseOrder();
                return JsonConvert.SerializeObject(Objallorders.GetAllOrders(Obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}