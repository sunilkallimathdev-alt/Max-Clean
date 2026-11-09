using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API.Models;

namespace ADMIN.Controllers
{
    public class TaggingController : Controller
    {
        // GET: Tagging
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tag(string ID)
        {
            try
            {
                API_Tagging obj = new API_Tagging();
                //ViewBag.OrderDetails = obj.GetOrderByID(Convert.ToInt64(ID));
                long id = Convert.ToInt64(ID);
                ViewBag.TagDetails = obj.GetTagbyid(id); 

                API.Models.Tag_Model Tag = (API.Models.Tag_Model)ViewBag.TagDetails;

                API_PurchaseOrder objorder = new API_PurchaseOrder();
                long ordID = Tag.Order_ID??0;
                var Result = objorder.GetOrderByID(ordID);
                ViewBag.OrderDetails = Result["ObjectResult"].ToObject<API.Models.PurchaseOrder_Model>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        public dynamic UpdateTag(string objTag)
        {
            try {
                API_Tagging obj = new API_Tagging();
                return JsonConvert.SerializeObject(obj.Update(objTag));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpPost]
        public dynamic GetTags()
        {
            try
            {
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


                API_Tagging obj = new API_Tagging();
                //return JsonConvert.SerializeObject(obj.GetTagsList());

                var result = obj.GetTagsList(length, start, sortColumn, sortColumnDir, draw, search);

                return Json(new { draw = result.draw, recordsFiltered = result.recordsTotal, recordsTotal = result.recordsTotal, data = result.data.ToObject<List<Tag_Model>>() }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic Getby_ID(long id)
        {
            try {

                API_Tagging obj = new API_Tagging();
                return JsonConvert.SerializeObject(obj.GetTagbyid(id));
            } catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}