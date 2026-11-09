using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class SubscriberController : Controller
    {
        // GET: Subscriber
        public ActionResult Index()
        {
            return View();
        }

        public dynamic GetSubcriberList()
        {
            try
            {
                API_Subscriber Objrole = new API_Subscriber();
                return JsonConvert.SerializeObject(Objrole.GetSubcriberList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddSubcriber(string ObjSubscriber)
        {
            try
            {
                API_Subscriber Objrole = new API_Subscriber();
                return JsonConvert.SerializeObject(Objrole.AddSubcriber(ObjSubscriber));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateSubcriber(string ObjSubscriber)
        {
            try
            {
                API_Subscriber Objrole = new API_Subscriber();
                return JsonConvert.SerializeObject(Objrole.UpdateSubcriber(ObjSubscriber));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetSubcriber(int SubscriberID)
        {
            try
            {
                API_Subscriber Objrole = new API_Subscriber();
                return JsonConvert.SerializeObject(Objrole.GetSubcriber(SubscriberID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteSubcriber(int SubscriberID)
        {
            try
            {
                API_Subscriber Objrole = new API_Subscriber();
                return JsonConvert.SerializeObject(Objrole.DeleteSubcriber(SubscriberID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
