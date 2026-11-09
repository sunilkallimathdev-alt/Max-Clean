using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class OrderCycleController : Controller
    {
        // GET: OrderCycle
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            return View();
        }

        public dynamic GetAllOrderCycleList()
        {
            try
            {
                API_OrderCycleStage ObjOrderCycle = new API_OrderCycleStage();
                return JsonConvert.SerializeObject(ObjOrderCycle.GetOrderCycleList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetActiveOrderCycleList()
        {
            try
            {
                API_OrderCycleStage ObjOrderCycle = new API_OrderCycleStage();
                return JsonConvert.SerializeObject(ObjOrderCycle.GetActiveOrderCycleList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddOrderCycle(string ObjOrderCycle)
        {
            try
            {
                API_OrderCycleStage OrderCycle = new API_OrderCycleStage();
                return JsonConvert.SerializeObject(OrderCycle.AddOrderCycle(ObjOrderCycle));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic UpdateOrderCycle(string ObjOrderCycle)
        {
            try
            {
                API_OrderCycleStage OrderCycle = new API_OrderCycleStage();
                return JsonConvert.SerializeObject(OrderCycle.UpdateOrderCycle(ObjOrderCycle));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetOrderCycle(int OrderCycleID)
        {
            try
            {
                API_OrderCycleStage OrderCycle = new API_OrderCycleStage();
                return JsonConvert.SerializeObject(OrderCycle.GetOrderCycleID(OrderCycleID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteOrderCycle(int OrderCycleID)
        {
            try
            {
                API_OrderCycleStage OrderCycle = new API_OrderCycleStage();
                return JsonConvert.SerializeObject(OrderCycle.DeleteOrderCycle(OrderCycleID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}