using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class BatchVendorController : Controller
    {
        // GET: BatchVendor
        public ActionResult Index()
        {
            return View();
        }

        public dynamic GetYettostartBatchList()
        {
            API_Repository.API_Batch Objbatch = new API_Repository.API_Batch();
            return Objbatch.GetYettostartBatchesList();
        }

        public dynamic GetVendorStockList()
        {
            API_Repository.API_Batch Objbatch = new API_Repository.API_Batch();
            return Objbatch.GetYettostartBatchesList();
        }

        public dynamic GetStoreStock(long BatchID)
        {
            API_Repository.API_BatchVendor Objbatch = new API_Repository.API_BatchVendor();
            return Objbatch.GetStoreStock(BatchID);
        }

        public dynamic AddBatchStock(string ObjBatch)
        {
            API_BatchVendor Objcategory = new API_BatchVendor();
            return JsonConvert.SerializeObject(Objcategory.AddBatchStock(ObjBatch));
        }
    }
}
