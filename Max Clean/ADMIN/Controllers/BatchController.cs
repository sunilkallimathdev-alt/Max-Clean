using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class BatchController : Controller
    {
        // GET: Batch
        public ActionResult Index()
        {
            API_Repository.API_Store Objstore = new API_Repository.API_Store();
            ViewBag.StoreList = Objstore.GetStoreList();
            return View();
        }

        public dynamic GetBatchList()
        {
            try
            {
                API_Batch Objcategory = new API_Batch();
                return JsonConvert.SerializeObject(Objcategory.GetBatchesList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddBatch(string ObjBatch)
        {
            try
            {
                API_Batch Objcategory = new API_Batch();
                return JsonConvert.SerializeObject(Objcategory.AddBatch(ObjBatch));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
