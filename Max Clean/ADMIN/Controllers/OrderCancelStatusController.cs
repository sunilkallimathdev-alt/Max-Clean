using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADMIN.API_Repository;
using API.Models;

namespace ADMIN.Controllers
{
    public class OrderCancelStatusController : Controller
    {
        // GET: OrderCancelStatus
        public ActionResult Index()
        {
            API_Store Objstore1 = new API_Store();
            ViewBag.StoreList = Objstore1.GetStoreList();
            return View();
        }
        public dynamic GetOrderCancelList(string store)
        {
            try
            {
                API_GetOrderCancel Objcustomer = new API_GetOrderCancel();
                return JsonConvert.SerializeObject(Objcustomer.GetOrderCancelList(store));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult LoadData(string obj)
        {
            try
            {
                OrderCancel_Model ObjModel = JsonConvert.DeserializeObject<OrderCancel_Model>(obj);

                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var search = Request.Form.GetValues("search[value]")[0];
                if (search == "")
                    search = "$0$";
                search = search.Replace(" ", "");

                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                //Find Order Column
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

                API_CancelOrder objcat = new API_CancelOrder();
                long ID = Convert.ToInt64(ObjModel.store_id);

                string FromDate = "";
                string ToDate = "";

                if (ObjModel.FromDate != null)
                {
                    DateTime res = Convert.ToDateTime(ObjModel.FromDate);
                    FromDate += res.Month + "-" + res.Day + "-" + res.Year;
                }
                else
                {
                    FromDate = "abc";
                }
                if (ObjModel.ToDate != null)
                {
                    DateTime res = Convert.ToDateTime(ObjModel.ToDate);
                    ToDate += res.Month + "-" + res.Day + "-" + res.Year;
                }
                else
                {
                    ToDate = "abc";
                }
                var result = objcat.Getsortedlist(length, start, sortColumn, sortColumnDir, draw, search, ID, FromDate, ToDate);

                return Json(new { draw = result.draw, recordsFiltered = result.recordsTotal, recordsTotal = result.recordsTotal, data = result.data.ToObject<List<PurchaseOrder_Model>>() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}