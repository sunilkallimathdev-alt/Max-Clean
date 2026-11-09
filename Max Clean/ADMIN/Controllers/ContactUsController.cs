using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: ContactUs
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            return View();
        }
        public dynamic GetContactUsList()
        {
            try
            {
                API_ContactUs Objcontactus = new API_ContactUs();
                return JsonConvert.SerializeObject(Objcontactus.GetContactUsList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddContactUs(string ObjContactUs)
        {
            try
            {
                API_ContactUs Objcontactus = new API_ContactUs();
                return JsonConvert.SerializeObject(Objcontactus.AddContactUs(ObjContactUs));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateContactUs(string ObjContactUs)
        {
            try
            {
                API_ContactUs Objcontactus = new API_ContactUs();
                return JsonConvert.SerializeObject(Objcontactus.UpdateContactUs(ObjContactUs));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetContactUs(int ContactUsID)
        {
            try
            {
                API_ContactUs Objcontactus = new API_ContactUs();
                return JsonConvert.SerializeObject(Objcontactus.GetContactUs(ContactUsID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteContactUs(int ContactUsID)
        {
            try
            {
                API_ContactUs Objcontactus = new API_ContactUs();
                return JsonConvert.SerializeObject(Objcontactus.DeleteContactUs(ContactUsID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}