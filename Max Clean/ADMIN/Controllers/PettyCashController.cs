using ADMIN.API_Repository;
using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 
 
namespace ADMIN.Controllers
{
    public class PettyCashController : Controller
    {
        // GET: PettyCash
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            API_Store Objstore = new API_Store();
            ViewBag.StoreList = Objstore.GetStoreList();
            return View();
        }
        public dynamic AddPettyCash(string ObjPCash)
        {
            try
            {
                API_PettyCash ObjPcash = new API_PettyCash();
                return JsonConvert.SerializeObject(ObjPcash.AddPettyCash(ObjPCash));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetPettyCashList(string Store)
        {
            try
            {
                API_PettyCash Objpcash = new API_PettyCash();
                return JsonConvert.SerializeObject(Objpcash.GetPettyCashList(Store));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[HttpPost]
        //public ActionResult LoadData()
        //{
        //    try
        //    {
        //        var draw = Request.Form.GetValues("draw").FirstOrDefault();
        //        var search = Request.Form.GetValues("search[value]")[0];
        //        if (search == "")
        //            search = "$0$";
        //        var start = Request.Form.GetValues("start").FirstOrDefault();
        //        var length = Request.Form.GetValues("length").FirstOrDefault();
        //        //Find Order Column
        //        var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
        //        var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
        //        //return getlist(length, start, sortColumn, sortColumnDir, draw);



        //        API_PettyCash ObjPcash = new API_PettyCash();
        //        var result = ObjPcash.Getsortedlist(length, start, sortColumn, sortColumnDir, draw, search);



        //        return Json(new { draw = result.draw, recordsFiltered = result.recordsTotal, recordsTotal = result.recordsTotal, data = result.data.ToObject<List<PettyCash_Model>>() }, JsonRequestBehavior.AllowGet);



        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private dynamic getlist(string length, string start, string sortColumn, string sortColumnDir, string draw)
        //{
        //    int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //    int skip = start != null ? Convert.ToInt32(start) : 0;
        //    int recordsTotal = 0;
        //    List<PettyCash_Model> pettycash = new List<PettyCash_Model>();
        //    using (API.DataAccess.maxcleanEntities Context = new API.DataAccess.maxcleanEntities())
        //    {
        //        // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
        //        Context.Configuration.LazyLoadingEnabled = false;

        //        pettycash = (from a in Context.t_petty_cash
        //                     select new PettyCash_Model
        //                     {
        //                         Store_Name = a.t_store.store_name,
        //                         Amount = a.Amount,
        //                         Reason = a.Reason,
        //                         Date = a.Date,

        //                     }).ToList();


        //        ////SORT
        //        //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
        //        //{
        //        //    pettycash = pettycash.OrderBy(sortColumn + " " + sortColumnDir).ToList();
        //        //}

        //        recordsTotal = pettycash.Count();
        //        var data = pettycash.Skip(skip).Take(pageSize).ToList();
        //        var result = Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        //        return result;
        //        //return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        //    }
        //}

    }
}