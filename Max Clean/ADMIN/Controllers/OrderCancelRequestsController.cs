using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADMIN.API_Repository;
using Newtonsoft.Json;

namespace ADMIN.Controllers
{
    public class OrderCancelRequestsController : Controller
    {
        // GET: OrderCancelRequests
        public ActionResult Index()
        {
            API_Store Objstore1 = new API_Store();
            ViewBag.StoreList = Objstore1.GetStoreList();
            return View();
        }
        public dynamic GetOrderCancelRequestList(string store)
        {
            API_GetOrderRequests ObjOrdCanReq = new API_GetOrderRequests();
            return JsonConvert.SerializeObject(ObjOrdCanReq.GetOrderCancelRequestList(store));
        }        
    }
}