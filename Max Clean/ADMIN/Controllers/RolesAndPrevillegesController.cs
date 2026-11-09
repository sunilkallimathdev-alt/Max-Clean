using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class RolesAndPrevillegesController : Controller
    {
        // GET: RolesAndPrevilleges
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            API_Role Objcategory = new API_Role();
            ViewBag.RoleList = Objcategory.GetActiveRoleList();
            return View();
        }


        public dynamic GetPagesList()
        {
            try
            {
                API_Role Objrole = new API_Role();
                return JsonConvert.SerializeObject(Objrole.Getpages());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetRolePages(int RoleID)
        {
            try
            {
                API_Role Objrole = new API_Role();
                return JsonConvert.SerializeObject(Objrole.GetRolePages(RoleID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveRole(List<API_Role.PagesList> SaveRolesObj)
        {
            try
            {
                API_Role Objrole = new API_Role();
               Objrole.SaveRoleList(SaveRolesObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}