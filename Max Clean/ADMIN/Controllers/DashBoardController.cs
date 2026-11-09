using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                API_Store Objstore = new API_Store();
                ViewBag.StoreList = Objstore.GetStoreList();
                ViewBag.Storeid = (int)id;
                return View();
            }
            else
            {
                API_Store Objstore1 = new API_Store();
                ViewBag.Storeid = 0;
                ViewBag.StoreList = Objstore1.GetStoreList();
                return View();
            }
        }

        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Orders()
        {
            API_Store Objstore = new API_Store();
            ViewBag.StoreList = Objstore.GetStoreList();

            return View();
        }

        public ActionResult ThisMonthsOrders()
        {
            API_Store Objstore = new API_Store();
            ViewBag.StoreList = Objstore.GetStoreList();
            return View();
        }

        public ActionResult Customers()
        { 
            return View();
        }

        public ActionResult ThisMonthsCustomers()
        {
            return View();
        }

        public ActionResult TotalBilling()
        {
            return View();
        }

        public ActionResult CurrentMonthsBilling()
        {
            return View();
        }

        public ActionResult PickUpOrders()
        {
            return View();
        }

        public ActionResult DropOrders()
        {
            return View();
        }

        public ActionResult DeliveryOrders()
        {
            return View();
        }

        //public dynamic GetDashoboardData()
        //{
        //    dynamic res;
        //    try
        //    {
        //     res   
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return res;
        //}
        
    }
}