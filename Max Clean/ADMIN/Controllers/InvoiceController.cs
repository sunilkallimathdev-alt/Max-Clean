using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API;
using ADMIN.API_Repository;
using Newtonsoft.Json;

namespace ADMIN.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index(string ID)
        {
            try
            {
             API_PurchaseOrder obj = new API_PurchaseOrder();
                //ViewBag.OrderDetails = obj.GetOrderByID(Convert.ToInt64(ID));
                long id = Convert.ToInt64(ID);
               var res= obj.GetOrderByID(id);
                ViewBag.OrderDetails= res["ObjectResult"].ToObject<API.Models.PurchaseOrder_Model>();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return View();
        }
        public ActionResult Invoice(string ID)
        {
            try
            {
                API_PurchaseOrder obj = new API_PurchaseOrder();
                //ViewBag.OrderDetails = obj.GetOrderByID(Convert.ToInt64(ID));
                long id = Convert.ToInt64(ID);
                var res = obj.GetOrderByID(id);
                ViewBag.OrderDetails = res["ObjectResult"].ToObject<API.Models.PurchaseOrder_Model>();

                API_Store objstore = new API_Store();
                ViewBag.StoreAddress = objstore.GetStoreList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

    }
}