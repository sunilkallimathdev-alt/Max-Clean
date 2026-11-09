using ADMIN.API_Repository;
using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ADMIN.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CustomerList()
        {
            return View();
        }


        public ActionResult CashFlow()
        {
            API_Store obj = new API_Store();
            ViewBag.StoreList = obj.GetStoreList();
            return View();
        }

        public ActionResult PickupOrders()
        {
            return View();
        }

        public ActionResult GetNonPerformingCustomer()
        {
            return View();
        }

        public ActionResult OrderList()
        {
            API_Store obj = new API_Store();
            ViewBag.StoreList = obj.GetStoreList();
            return View();
        }

        public ActionResult GetInventory()
        {
            API_Store obj = new API_Store();
            ViewBag.StoreList = obj.GetStoreList();
            API_Category Objcategory = new API_Category();
            ViewBag.Category = Objcategory.GetCategoryList();
            return View();
        }

        public ActionResult GetCustomerOrders()
        {
            return View();
        }
        public ActionResult GetInventoryManagement()
        {
            API_Store obj = new API_Store();
            ViewBag.StoreList = obj.GetStoreList();
            return View();
        }

        public dynamic GetCustomerOrdersByID(int CustomerID)
        {
            try
            {
                API_Customer obj = new API_Customer();
                return JsonConvert.SerializeObject(obj.GetCustomerOrderList(CustomerID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public dynamic GetCustomerList()
        {
            try
            {
                API_Customer Objcustomer = new API_Customer();
                return JsonConvert.SerializeObject(Objcustomer.GetCustomerList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public dynamic GetOrders(string obj)
        {
            try
            {
                API_Reports Objreports = new API_Reports();
                return JsonConvert.SerializeObject(Objreports.GetOrders(obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public dynamic GetNonPerformingcustomer1()
        {
            try
            {
                API_Customer Objcustomer = new API_Customer();
                return JsonConvert.SerializeObject(Objcustomer.GetNonPerformingCustomer());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetInventory1(int StoreID)
        {
            try
            {
                API_Reports Objreports = new API_Reports();
                return JsonConvert.SerializeObject(Objreports.GetInventory(StoreID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetInventoryManagement1(int StoreID, int orderstatusID)
        {
            try
            {
                API_Reports Objreports = new API_Reports();
                return JsonConvert.SerializeObject(Objreports.GetInventoryManagement(StoreID, orderstatusID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        //public dynamic GetReportsList()
        //{
        //    try
        //    {
        //        API_Reports Objreports = new API_Reports();
        //        return JsonConvert.SerializeObject(Objreports.GetReportsList());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public dynamic GetReports(int CustomerId)
        //{
        //    try
        //    {
        //        API_Customer Objreports = new API_Customer();
        //        return JsonConvert.SerializeObject(Objreports.GetReports(CustomerId));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}