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
    public class CategoryController : Controller
    {
         
        // GET: Category
        public ActionResult Index()
        {
   
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            API_Service Objservice = new API_Service();
            ViewBag.Service = Objservice.GetActiveService();
            return View();
        }
        public dynamic GetCategoryList()
        {
            try
            {
                API_Category Objcategory = new API_Category();
                return JsonConvert.SerializeObject(Objcategory.GetCategoryList());
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddCategory(string ObjCategory)
        {
            try
            {
                API_Category Objcategory = new API_Category();
                return JsonConvert.SerializeObject(Objcategory.AddCategory(ObjCategory));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateCategory(string ObjCategory)
        {
            try
            {
                API_Category Objcategory = new API_Category();
                return JsonConvert.SerializeObject(Objcategory.UpdateCategory(ObjCategory));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetCategory(int CategoryId)
        {
            try
            {
                API_Category Objcategory = new API_Category();
                return JsonConvert.SerializeObject(Objcategory.GetCategory(CategoryId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteCategory(int CategoryId)
        {
            try
            {
                API_Category Objcategory = new API_Category();
                return JsonConvert.SerializeObject(Objcategory.DeleteCategory(CategoryId));
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



                API_Category objcat = new API_Category();
                var result = objcat.Getsortedlist(length, start, sortColumn, sortColumnDir, draw, search);

               

                return Json(new { draw = result.draw, recordsFiltered = result.recordsTotal, recordsTotal = result.recordsTotal, data = result.data.ToObject<List<Category_Model>>() }, JsonRequestBehavior.AllowGet);

                

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private dynamic getlist(string length,string start,string sortColumn,string sortColumnDir,string draw)
        {
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            List<Category_Model> ObjCategories = new List<Category_Model>();
            using (API.DataAccess.maxcleanEntities Context = new API.DataAccess.maxcleanEntities())
            {
                // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
                Context.Configuration.LazyLoadingEnabled = false;
                
                 ObjCategories = (from a in Context.t_category
                         select new Category_Model
                         {
                             CategoryId = a.category_id,
                             //ServiceID = a.service_id,
                             //ServiceName = a.t_service.service_name,
                             CategoryName = a.category_name,
                             CategoryBanner = a.category_banner,
                             CategoryIcon = a.category_icon,
                             DisplayOrder = a.display_order,
                             Description = a.description,
                             IsActive = a.is_active,
                             CreatedBy = a.created_by,
                             CreatedDate = a.created_date,
                             UpdateBy = a.updated_by,
                             UpdateDate = a.updated_date
                         }).ToList();
                         

                ////SORT
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    ObjCategories = ObjCategories.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                }

                recordsTotal = ObjCategories.Count();
                var data = ObjCategories.Skip(skip).Take(pageSize).ToList();
                var result = Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
                return result;
                //return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
            }
        }
    }


    

        
}