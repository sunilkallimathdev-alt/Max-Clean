using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class HomeBannerController : Controller
    {
        // GET: HomeBanner
        public ActionResult Index()
        {
            return View();
        }
        public dynamic GetHomeBannerList()
        {
            try
            {
                API_HomeBanner Objcustomer = new API_HomeBanner();
                return JsonConvert.SerializeObject(Objcustomer.GetHomeBannerList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddHomeBanner(string ObjHome)
        {
            try
            {
                API_HomeBanner Objcustomer = new API_HomeBanner();
                return JsonConvert.SerializeObject(Objcustomer.AddHomeBanner(ObjHome));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateHomeBanner(string ObjHome)
        {
            try
            {
                API_HomeBanner Objcustomer = new API_HomeBanner();
                return JsonConvert.SerializeObject(Objcustomer.UpdateHomeBanner(ObjHome));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetHomeBanner(int BannerID)
        {
            try
            {
                API_HomeBanner Objcustomer = new API_HomeBanner();
                return JsonConvert.SerializeObject(Objcustomer.GetHomeBanner(BannerID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteHomeBanner(int BannerID)
        {
            try
            {
                API_HomeBanner Objcustomer = new API_HomeBanner();
                return JsonConvert.SerializeObject(Objcustomer.DeleteHomeBanner(BannerID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}