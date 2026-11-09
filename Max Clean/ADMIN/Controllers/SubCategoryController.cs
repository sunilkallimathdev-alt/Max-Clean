using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class SubCategoryController : Controller
    {
        // GET: SubCategory
        public ActionResult Index()
        {
            API_Category Objcategory = new API_Category();
            ViewBag.Category = Objcategory.GetCategoryList();
            return View();
        }
        public dynamic GetSubCategoryList()
        {
            try
            {
                API_SubCategory Objsubcategory = new API_SubCategory();
                return JsonConvert.SerializeObject(Objsubcategory.GetSubCategoryList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddSubCategory(string ObjSubCategory)
        {
            try
            {
                API_SubCategory Objcategory = new API_SubCategory();
                return JsonConvert.SerializeObject(Objcategory.AddSubCategory(ObjSubCategory));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateSubCategory(string ObjSubCategory)
        {
            try
            {
                API_SubCategory Objcategory = new API_SubCategory();
                return JsonConvert.SerializeObject(Objcategory.UpdateSubCategory(ObjSubCategory));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetSubCategory(int SubCategoryID)
        {
            try
            {
                API_SubCategory Objcategory = new API_SubCategory();
                return JsonConvert.SerializeObject(Objcategory.GetSubCategory(SubCategoryID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteSubCategory(int SubCategoryID)
        {
            try
            {
                API_SubCategory Objstore = new API_SubCategory();
                return JsonConvert.SerializeObject(Objstore.DeleteSubCategory(SubCategoryID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}