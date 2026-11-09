using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class BannerController : Controller
    {
        // GET: Banner
        public ActionResult Index()
        {
            if (Session["logindetails"] == null)
                Response.Redirect("/AdminLogin/Index");
            return View();
        }

        public dynamic GetBannerList()
        {
            try
            {
                API_Banner ObjBanner = new API_Banner();
                return JsonConvert.SerializeObject(ObjBanner.GetBannerList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddBanner(string ObjBanner)
        {
            try
            {
                API_Banner Obj = new API_Banner();
                return JsonConvert.SerializeObject(Obj.AddBanner(ObjBanner));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic UpdateBanner(string ObjBanner)
        {
            try
            {
                API_Banner Obj = new API_Banner();
                return JsonConvert.SerializeObject(Obj.UpdateBanner(ObjBanner));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetBanner(int BannerID)
        {
            try
            {
                API_Banner ObjBanner = new API_Banner();
                return JsonConvert.SerializeObject(ObjBanner.GetBanner(BannerID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteBanner(int BannerID)
        {
            try
            {
                API_Banner ObjBanner = new API_Banner();
                return JsonConvert.SerializeObject(ObjBanner.DeleteBanner(BannerID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}