using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADMIN;
using ADMIN.API_Repository;
using API.Models;
using API.DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace ADMIN.Controllers
{
    public class ChangeStoreController : Controller
    {
        // GET: ChangeStore
        public ActionResult Index()
        {
            API_Store Objstore = new API_Store();
            ViewBag.StoreList = Objstore.GetStoreList();
            return View();
        }

        public dynamic UpdateorderStore(string Obj)
        {
            try
            {
                API_Rescheduled obj = new API_Rescheduled();
                return JsonConvert.SerializeObject(obj.ChangeStore(Obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}