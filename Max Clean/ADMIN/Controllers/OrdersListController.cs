using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADMIN;
using ADMIN.API_Repository;
using API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ADMIN.Controllers
{
    public class OrdersListController : Controller
    {
        // GET: OrdersList
        public ActionResult Index()
        {
            //if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            //{
            //    if (Session["logindetails"] == null)
            //        Response.Redirect("/AdminLogin/Index");
            //}
            API_Customer Objve = new API_Customer();
            ViewBag.CustomerList = Objve.GetCustomerList();
            API_Store Objstore = new API_Store();
            ViewBag.StoreList = Objstore.GetStoreList();
            API_Category Objcat = new API_Category();
            ViewBag.CategoryList = Objcat.GetCategoryList();
            API_Repository.API_OrderCycleStage obj = new API_Repository.API_OrderCycleStage();
            ViewBag.OrderStatusList = obj.GetActiveOrderCycleList();
            API_Repository.API_AdminUser objuser = new API_Repository.API_AdminUser();
            ViewBag.Adminuserlist = objuser.GetActiveAdminUser();
            API_Repository.API_DeliveryBoy objdelboy = new API_Repository.API_DeliveryBoy();
            ViewBag.DeliveryBoys = objdelboy.GetActiveDeliveryBoyList();

            API_Service objsrc = new API_Service();
            ViewBag.ServiceList = objsrc.GetServiceList();


            return View();
        }

        public ActionResult placeOrder()
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
            API_Repository.API_OrderCycleStage obj = new API_Repository.API_OrderCycleStage();
            ViewBag.OrderStatusList = obj.GetActiveOrderCycleList();
            API_Repository.API_AdminUser objuser = new API_Repository.API_AdminUser();
            ViewBag.Adminuserlist = objuser.GetActiveAdminUser();
            API_Repository.API_DeliveryBoy objdelboy = new API_Repository.API_DeliveryBoy();
            ViewBag.DeliveryBoys = objdelboy.GetActiveDeliveryBoyList();

            API_Service objsrc = new API_Service();
            ViewBag.ServiceList = objsrc.GetServiceList();
            return View();
        }

        // public dynamic GetOrdersList(long ID)

        [HttpPost]
        public ActionResult LoadData(string obj)
        {
            try
            {
               
                PurchaseOrder_Model ObjModel = JsonConvert.DeserializeObject<PurchaseOrder_Model>(obj);

                //API_Repository.API_PurchaseOrder obj = new API_Repository.API_PurchaseOrder();
                //if(IsOutlet!=null)
                //return JsonConvert.SerializeObject(obj.GetAllOnlinestoreOrder(IsOutlet));
                //else
                //    return JsonConvert.SerializeObject(obj.GetAllOrderList());

                ///////****************orderby_store_id******************////////
                //API_Repository.API_PurchaseOrder obj = new API_Repository.API_PurchaseOrder();
                //return JsonConvert.SerializeObject(obj.GetAllOrderByStoreID(ID));

                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var search = Request.Form.GetValues("search[value]")[0];
                if (search == "")
                    search = "$0$";
                search = search.Replace(" ", "");

                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                //Find Order Column
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

                API_PurchaseOrder objcat = new API_PurchaseOrder();
                long ID =Convert.ToInt64(ObjModel.StoreID);
             
                string FromDate = "";
               string ToDate = "";

                if (ObjModel.FromDate != null)
                {
                    DateTime res = Convert.ToDateTime(ObjModel.FromDate);
                    FromDate += res.Month +"-"+ res.Day+"-" + res.Year;
                }
                else
                {
                    FromDate = "abc";
                }
                if (ObjModel.ToDate != null)
                {
                    DateTime res = Convert.ToDateTime(ObjModel.ToDate);
                    ToDate += res.Month+"-" + res.Day+"-" + res.Year;
                }
                else
                {
                    ToDate = "abc";
                }
                var result = objcat.Getsortedlist(length, start, sortColumn, sortColumnDir, draw, search, ID, FromDate, ToDate);

                return Json(new { draw = result.draw, recordsFiltered = result.recordsTotal, recordsTotal = result.recordsTotal, data = result.data.ToObject<List<PurchaseOrder_Model>>() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetCustomerID(string objcst)
        {
            try
            {

                API_Repository.API_Customer obj = new API_Repository.API_Customer();

                return JsonConvert.SerializeObject(obj.GetCustomerId(objcst));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetIntProductOnCategoryID(int CategoryID)
        {
            try
            {
                API_Product obj = new API_Product();
                return JsonConvert.SerializeObject(obj.GetInvProduct(CategoryID));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public dynamic AddPurchaseOrder(string ObjPO)
        {
            API_PurchaseOrder Objpro = new API_PurchaseOrder();
            return Objpro.AddPurchaseOrder(ObjPO);
        }

        public dynamic GetCategoryProductsByServiceID(int ServiceID)
        {
            API_Category obj = new API_Category();
            return JsonConvert.SerializeObject(obj.GetCategoryProductsByServiceID(ServiceID));
        }

        public dynamic GetService()
        {
            API_Service Objpro = new API_Service();
            return JsonConvert.SerializeObject(Objpro.GetActiveService());
        }

        public dynamic UpdateStatus(string obj)
        {
            API_PurchaseOrder objpo = new API_PurchaseOrder();
            return objpo.UpdateStatus(obj);
        }


        ///////////////********* POP-UP Methods*********//////////////
        public dynamic GetOrderBYstatus(int StatusID)
        {
            try
            {

                API_Repository.API_Tagging objtag = new API_Repository.API_Tagging();

                return JsonConvert.SerializeObject(objtag.GetOrderList_byStatusID(StatusID));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic ChangeStatus(string objlist)
        {
            try
            {
                API_Tagging Objpro = new API_Tagging();
                return Objpro.UpdateOrderStatus(objlist);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}