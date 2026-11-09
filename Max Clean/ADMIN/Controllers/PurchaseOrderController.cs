using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADMIN.API_Repository;
using Newtonsoft.Json;

namespace ADMIN.Controllers
{
    public class PurchaseOrderController : Controller
    {
        // GET: PurchaseOrder
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            API_Customer Objve = new API_Customer();
            ViewBag.CustomerList = Objve.GetCustomerList();
            API_Store Objstore = new API_Store();
            ViewBag.StoreList = Objstore.GetStoreList();
            API_Category Objcat = new API_Category();
            ViewBag.CategoryList = Objcat.GetCategoryList();
            return View();
        }

        public dynamic GetIntProductOnCategoryID(int CategoryID)
        {
            API_Product Objpro = new API_Product();
            return Objpro.GetInvProduct(CategoryID);
        }

        public dynamic AddPurchaseOrder(string ObjPO)
        {
            API_PurchaseOrder Objpro = new API_PurchaseOrder();
            return Objpro.AddPurchaseOrder(ObjPO);
        }
        public dynamic UpdatePurchaseOrder(string ObjOrder)
        {
            try
            {
                API_PurchaseOrder Objpro = new API_PurchaseOrder();
                return JsonConvert.SerializeObject(Objpro.UpdatePurchaseOrder(ObjOrder));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic PayBalance(string Obj)
        {
            try
            {
                API_PurchaseOrder Objpro = new API_PurchaseOrder();
                return JsonConvert.SerializeObject(Objpro.PayBalance(Obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
