using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class DeliveryBoyController : Controller
    {
        // GET: DeliveryBoy
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            return View();
        }


        public dynamic GetAllDeliveryBoyList()
        {
            try
            {
                API_DeliveryBoy ObjDeliveryBoy = new API_DeliveryBoy();
                return JsonConvert.SerializeObject(ObjDeliveryBoy.GetDeliveryBoyList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetActiveDeliveryBoyList()
        {
            try
            {
                API_DeliveryBoy ObjDeliveryBoy = new API_DeliveryBoy();
                return JsonConvert.SerializeObject(ObjDeliveryBoy.GetActiveDeliveryBoyList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddDeliveryBoy(string ObjDeliveryBoy)
        {
            try
            {
                API_DeliveryBoy DeliveryBoy = new API_DeliveryBoy();
                return JsonConvert.SerializeObject(DeliveryBoy.AddDeliveryBoy(ObjDeliveryBoy));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic UpdateDeliveryBoy(string ObjDeliveryBoy)
        {
            try
            {
                API_DeliveryBoy DeliveryBoy = new API_DeliveryBoy();
                return JsonConvert.SerializeObject(DeliveryBoy.UpdateDeliveryBoy(ObjDeliveryBoy));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetDeliveryBoy(int DeliveryBoyID)
        {
            try
            {
                API_DeliveryBoy DeliveryBoy = new API_DeliveryBoy();
                return JsonConvert.SerializeObject(DeliveryBoy.GetDeliveryBoyID(DeliveryBoyID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteDeliveryBoy(int DeliveryBoyID)
        {
            try
            {
                API_DeliveryBoy DeliveryBoy = new API_DeliveryBoy();
                return JsonConvert.SerializeObject(DeliveryBoy.DeleteDeliveryBoy(DeliveryBoyID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}