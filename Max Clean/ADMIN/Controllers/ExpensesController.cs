using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADMIN.API_Repository;
using Newtonsoft.Json.Linq;

namespace ADMIN.Controllers
{
    public class ExpensesController : Controller
    {
        // GET: Expenses
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
        public ActionResult ExpensesListView()
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
        public dynamic AddExpenses(string ObjExpen)
        {
            try
            {
                API_Expenses ObjExp = new API_Expenses();
                return JsonConvert.SerializeObject(ObjExp.AddExpenses(ObjExpen));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetExpensesList(string Store)
        {
            try
            {
                API_Expenses ObjExp = new API_Expenses();
                return JsonConvert.SerializeObject(ObjExp.GetExpensesList(Store));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}