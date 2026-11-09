using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class DeliveryBoyIncentivesController : Controller
    {
        // GET: DeliveryBoyIncentives
        public ActionResult DeliveryBoyIncentives()
        {
            API_DeliveryBoy Objdeliveryboy= new API_DeliveryBoy();
            ViewBag.DeliveryBoy = Objdeliveryboy.GetDeliveryBoyList();
            return View();
        }

        public dynamic GetAllDeliveryBoyIncentivesByDeliveryBoyID(int DeliveryBoyID)
        {
            try
            {
                API_DeliveryBoyIncentives Objdeliveryboyincentives = new API_DeliveryBoyIncentives();
                return JsonConvert.SerializeObject(Objdeliveryboyincentives.GetAllDeliveryBoyIncentivesByDeliveryBoyID(DeliveryBoyID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic AddDeliveryBoyIncentives(string ObjDeliveryBoyIncentives)
        {
            try
            {
                API_DeliveryBoyIncentives Objdeliveryboyincentives = new API_DeliveryBoyIncentives();
                return JsonConvert.SerializeObject(Objdeliveryboyincentives.AddDeliveryBoyIncentives(ObjDeliveryBoyIncentives));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic MakePayment(string strpay)
        {
            try
            {
                API_DeliveryBoyIncentives Objdeliveryboyincentives = new API_DeliveryBoyIncentives();
                return JsonConvert.SerializeObject(Objdeliveryboyincentives.MakePayment(strpay));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic SendInvoice(long RequesOrderID)
        {
            try
            {
                API_DeliveryBoyIncentives Objdeliveryboyincentives = new API_DeliveryBoyIncentives();
                return JsonConvert.SerializeObject(Objdeliveryboyincentives.SendInvoiceMail(RequesOrderID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}