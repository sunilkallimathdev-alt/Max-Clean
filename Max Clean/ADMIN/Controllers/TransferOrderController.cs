using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADMIN.API_Repository;
namespace ADMIN.Controllers
{
    public class TransferOrderController : Controller
    {
        // GET: TransferOrder
        public ActionResult Index()
        {
            API_Store Objstore1 = new API_Store();
            ViewBag.StoreList = Objstore1.GetStoreList();
            return View();
        }
    }
}