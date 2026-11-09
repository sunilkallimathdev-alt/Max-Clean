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
    public class AdminUserController : Controller
    {
        // GET: AdminUser
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            API_Role Objcategory = new API_Role();
            ViewBag.RoleList = Objcategory.GetActiveRoleList();

            API_Store objstore = new API_Store();
            ViewBag.StoreList = objstore.GetActiveStore();
            return View();
        }
        public dynamic GetAdminUserList()
        {
            try
            {
                API_AdminUser Objstore = new API_AdminUser();
                return JsonConvert.SerializeObject(Objstore.GetAdminUserList());
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddAdminUser(string ObjAdminUser)
        {
            try
            {
                API_AdminUser Objadminuser = new API_AdminUser();
                return JsonConvert.SerializeObject(Objadminuser.AddAdminUser(ObjAdminUser));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateAdminUser(string ObjAdminUser)
        {
            try
            {
                API_AdminUser Objadminuser = new API_AdminUser();
                return JsonConvert.SerializeObject(Objadminuser.UpdateAdminUser(ObjAdminUser));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetAdminUser(int UserID)
        {
            try
            {
                API_AdminUser Objstore = new API_AdminUser();
                return JsonConvert.SerializeObject(Objstore.GetAdminUser(UserID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteAdminUser(int UserID)
        {
            try
            {
                API_AdminUser Objadminuser = new API_AdminUser();
                return JsonConvert.SerializeObject(Objadminuser.DeleteAdminUser(UserID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}