using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class RescheduleDateController : Controller
    {
        // GET: RescheduleDate
        public ActionResult Index()
        {
            return View();
        }

        public dynamic Updateorder(string Obj) { 
            try
            {
                API_Rescheduled obj = new API_Rescheduled();
                return JsonConvert.SerializeObject(obj.Rescheduled(Obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}