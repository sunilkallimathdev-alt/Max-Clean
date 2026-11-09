using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            return View();
        }
        public dynamic GetRoleList()
        {
            try
            {
                API_Role Objrole = new API_Role();
                return JsonConvert.SerializeObject(Objrole.GetRoleList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddRole(string ObjRole)
        {
            try
            {
                API_Role Objrole = new API_Role();
                return JsonConvert.SerializeObject(Objrole.AddRole(ObjRole));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateRole(string ObjRole)
        {
            try
            {
                API_Role Objrole = new API_Role();
                return JsonConvert.SerializeObject(Objrole.UpdateRole(ObjRole));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetRole(int RoleID)
        {
            try
            {
                API_Role Objrole = new API_Role();
                return JsonConvert.SerializeObject(Objrole.GetTicketSubject(RoleID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteRole(int RoleID)
        {
            try
            {
                API_Role Objrole = new API_Role();
                return JsonConvert.SerializeObject(Objrole.DeleteRole(RoleID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}