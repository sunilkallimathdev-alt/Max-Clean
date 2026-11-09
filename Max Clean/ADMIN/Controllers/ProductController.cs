using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [Route("Product")]
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            API_Category Objcategory = new API_Category();
            ViewBag.Category = Objcategory.GetActiveCategory();
            //API_Unit ObjUnit = new API_Unit();
            //ViewBag.UnitList = ObjUnit.GetActiveUnitList();
            return View();
        }
        public dynamic GetProductList()
        {
            try
            {
                API_Product ObjProduct = new API_Product();
                return JsonConvert.SerializeObject(ObjProduct.GetProductList());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddProduct(string ObjProduct)
        {
            try
            {
                API_Product Objpro = new API_Product();
                return JsonConvert.SerializeObject(Objpro.AddProduct(ObjProduct));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateProduct(string ObjProduct)
        {
            try
            {
                API_Product Objpro = new API_Product();
                return JsonConvert.SerializeObject(Objpro.UpdateProduct(ObjProduct));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetProduct(int ProductId)
        {
            try
            {
                API_Product ObjProduct = new API_Product();
                return JsonConvert.SerializeObject(ObjProduct.GetProduct(ProductId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteProduct(int ProductId)
        {
            try
            {
                API_Product Objpro = new API_Product();
                return JsonConvert.SerializeObject(Objpro.DeleteProduct(ProductId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
    }
}