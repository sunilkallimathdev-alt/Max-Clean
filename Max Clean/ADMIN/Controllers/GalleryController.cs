using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            return View();
        }
        public dynamic GetGalleryList()
        {
            try
            {
                API_Gallery Objgallery = new API_Gallery();
                return JsonConvert.SerializeObject(Objgallery.GetGalleryList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddGallery(string ObjGallery)
        {
            try
            {
                API_Gallery Objgallery = new API_Gallery();
                return JsonConvert.SerializeObject(Objgallery.AddGallery(ObjGallery));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic UpdateGallery(string ObjGallery)
        {
            try
            {
                API_Gallery Objgallery = new API_Gallery();
                return JsonConvert.SerializeObject(Objgallery.UpdateGallery(ObjGallery));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetGallery(int GalleryID)
        {
            try
            {
                API_Gallery Objgallery = new API_Gallery();
                return JsonConvert.SerializeObject(Objgallery.GetGallery(GalleryID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteGallery(int GalleryID)
        {
            try
            {
                API_Gallery Objgallery = new API_Gallery();
                return JsonConvert.SerializeObject(Objgallery.DeleteGallery(GalleryID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}