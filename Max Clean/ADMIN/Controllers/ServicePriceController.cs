using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class ServicePriceController : Controller
    {
        // GET: ServicePrice
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }

            API_Service Objservice = new API_Service();
            ViewBag.Service = Objservice.GetActiveService();
            API_Category Objcategory = new API_Category();
            ViewBag.Category = Objcategory.GetActiveCategory();
            API_Product Objproduct = new API_Product();
            ViewBag.Product = Objproduct.GetActiveProduct();

            return View();
        }
        public dynamic GetServicePriceList()
        {
            try
            {
                API_ServicePrice Objserviceprice = new API_ServicePrice();
                return JsonConvert.SerializeObject(Objserviceprice.GetServicePriceList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddServicePrice(string ObjServicePrice)
        {
            try
            {
                API_ServicePrice Objserviceprice = new API_ServicePrice();
                return JsonConvert.SerializeObject(Objserviceprice.AddServicePrice(ObjServicePrice));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateServicePrice(string ObjServicePrice)
        {
            try
            {
                API_ServicePrice Objserviceprice = new API_ServicePrice();
                return JsonConvert.SerializeObject(Objserviceprice.UpdateServicePrice(ObjServicePrice));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetServicePrice(int ServicePriceID)
        {
            try
            {
                API_ServicePrice Objserviceprice = new API_ServicePrice();
                return JsonConvert.SerializeObject(Objserviceprice.GetServicePrice(ServicePriceID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteServicePrice(int ServicePriceID)
        {
            try
            {
                API_ServicePrice Objserviceprice = new API_ServicePrice();
                return JsonConvert.SerializeObject(Objserviceprice.DeleteServicePrice(ServicePriceID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}