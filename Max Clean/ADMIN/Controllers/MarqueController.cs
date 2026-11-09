using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class MarqueController : Controller
    {
        // GET: Marque
        public ActionResult Index()
        {
            if (Session["logindetails"] == null)
                Response.Redirect("/AdminLogin/Index");
            return View();
        }

        public dynamic GetMarqueList()
        {
            try
            {
                API_Marque ObjMarque = new API_Marque();
                return JsonConvert.SerializeObject(ObjMarque.GetMarqueList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddMarque(string ObjMarque)
        {
            try
            {
                API_Marque Obj = new API_Marque();
                return JsonConvert.SerializeObject(Obj.AddMarque(ObjMarque));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic UpdateMarque(string ObjMarque)
        {
            try
            {
                API_Marque Obj = new API_Marque();
                return JsonConvert.SerializeObject(Obj.UpdateMarque(ObjMarque));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetMarque(int MarqueID)
        {
            try
            {
                API_Marque ObjMarque = new API_Marque();
                return JsonConvert.SerializeObject(ObjMarque.GetMarque(MarqueID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteMarque(int MarqueID)
        {
            try
            {
                API_Marque ObjMarque = new API_Marque();
                return JsonConvert.SerializeObject(ObjMarque.DeleteMarque(MarqueID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}