using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            return View();
        }
        public dynamic GetServiceList()
        {
            try
            {
                API_Service Objservice = new API_Service();
                return JsonConvert.SerializeObject(Objservice.GetServiceList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddService(string ObjService)
        {
            try
            {
                API_Service Objservice = new API_Service();
                return JsonConvert.SerializeObject(Objservice.AddService(ObjService));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateService(string ObjService)
        {
            try
            {
                API_Service Objservice = new API_Service();
                return JsonConvert.SerializeObject(Objservice.UpdateService(ObjService));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetService(int ServiceID)
        {
            try
            {
                API_Service Objservice = new API_Service();
                return JsonConvert.SerializeObject(Objservice.GetService(ServiceID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteService(int ServiceID)
        {
            try
            {
                API_Service Objservice = new API_Service();
                return JsonConvert.SerializeObject(Objservice.DeleteService(ServiceID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}