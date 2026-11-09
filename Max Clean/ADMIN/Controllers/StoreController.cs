using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            return View();
        }
        public dynamic GetStoreList()
        {
            try
            {
                API_Store Objstore = new API_Store();
                return JsonConvert.SerializeObject(Objstore.GetStoreList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddStore(string ObjStore)
        {
            try
            {
                API_Store Objstore = new API_Store();
                return JsonConvert.SerializeObject(Objstore.AddStore(ObjStore));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateStore(string ObjStore)
        {
            try
            {
                API_Store Objstore = new API_Store();
                return JsonConvert.SerializeObject(Objstore.UpdateStore(ObjStore));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetStore(int StoreID)
        {
            try
            {
                API_Store Objstore = new API_Store();
                return JsonConvert.SerializeObject(Objstore.GetStore(StoreID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteStore(int StoreID)
        {
            try
            {
                API_Store Objstore = new API_Store();
                return JsonConvert.SerializeObject(Objstore.DeleteStore(StoreID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}