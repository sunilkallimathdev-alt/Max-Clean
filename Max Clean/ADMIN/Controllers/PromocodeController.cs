using ADMIN.API_Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Linq.Dynamic;
using API.Models;
using System.Collections.Generic;

namespace ADMIN.Controllers
{
    public class PromocodeController : Controller
    {
        // GET: Promocode
        public ActionResult Index()
        {
            return View();
        }





        public dynamic AddPromocode(string ObjPromocode)
        {
            try
            {
                API_Promocode Obj = new API_Promocode();
                return JsonConvert.SerializeObject(Obj.AddPromocode(ObjPromocode));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdatePromocode(string ObjPromocode)
        {
            try
            {
                API_Promocode Obj = new API_Promocode();
                return JsonConvert.SerializeObject(Obj.UpdatePromocode(ObjPromocode));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetPromocode(int PromocodeId)
        {
            try
            {
                API_Promocode Obj = new API_Promocode();
                return JsonConvert.SerializeObject(Obj.GetPromocode(PromocodeId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeletePromocode(int PromocodeId)
        {
            try
            {
                API_Promocode Obj= new API_Promocode();
                return JsonConvert.SerializeObject(Obj.DeletePromocode(PromocodeId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public dynamic ValidatePromocode(string ObjPromocode)
        {
            try
            {
                API_Promocode Obj = new API_Promocode();
                return JsonConvert.SerializeObject(Obj.ValidatePromocode(ObjPromocode));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public ActionResult LoadData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var search = Request.Form.GetValues("search[value]")[0];
                if (search == "")
                    search = "$0$";
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                //Find Order Column
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                //return getlist(length, start, sortColumn, sortColumnDir, draw);



                API_Promocode objcat = new API_Promocode();
                var result = objcat.Getsortedlist(length, start, sortColumn, sortColumnDir, draw, search);



                return Json(new { draw = result.draw, recordsFiltered = result.recordsTotal, recordsTotal = result.recordsTotal, data = result.data.ToObject<List<Promocode_Model>>() }, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}