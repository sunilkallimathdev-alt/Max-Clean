using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class PurchaseOrderPaymentController : Controller
    {
        // GET: VendorProductOrderPayment
        public ActionResult Index()
        {
            API_Vendor Objve = new API_Vendor();
            ViewBag.VendorList = Objve.GetVendorList();
            return View();
        }
        public dynamic GetRequestOrderList(long VendorID)
        {
            try
            {
                API_PurchasePayment Objcategory = new API_PurchasePayment();
                return JsonConvert.SerializeObject(Objcategory.GetRequestOrdersByVendorID(VendorID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic MakePayment(string strpay)
        {
            try
            {
                API_PurchasePayment Objcategory = new API_PurchasePayment();
                return JsonConvert.SerializeObject(Objcategory.MakePayment(strpay));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic SendInvoice(long RequesOrderID)
        {
            try
            {
                API_PurchasePayment Objcategory = new API_PurchasePayment();
                return JsonConvert.SerializeObject(Objcategory.SendInvoiceMail(RequesOrderID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
