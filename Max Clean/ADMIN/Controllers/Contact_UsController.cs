using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class Contact_UsController : Controller
    {
        // GET: Contact_Us
        public ActionResult Index()
        {
            return View();
        }
        public dynamic GetContactUsList()
        {
            try
            {
                API_Contact_Us Objcontactus = new API_Contact_Us();
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
                API_Contact_Us Objcontactus = new API_Contact_Us();
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
                API_Contact_Us Objcontactus = new API_Contact_Us();
                return JsonConvert.SerializeObject(Objcontactus.UpdateContactUs(ObjContactUs));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetContactUs(int ContactUsId)
        {
            try
            {
                API_Contact_Us Objcontactus = new API_Contact_Us();
                return JsonConvert.SerializeObject(Objcontactus.GetContactUs(ContactUsId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteContactUs(int ContactUsId)
        {
            try
            {
                API_Contact_Us Objcontactus = new API_Contact_Us();
                return JsonConvert.SerializeObject(Objcontactus.DeleteContactUs(ContactUsId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}