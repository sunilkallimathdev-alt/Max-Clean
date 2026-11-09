using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            return View();
        }
        public dynamic GetCustomerList()
        {
            try
            {
                API_Customer Objcustomer = new API_Customer();
                return JsonConvert.SerializeObject(Objcustomer.GetCustomerList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetCustomerList_BYdates(string obj)
        {
            try
            {
                API_Customer Objcustomer = new API_Customer();
                return JsonConvert.SerializeObject(Objcustomer.GetCustomerByDate(obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic AddCustomer(string ObjCustomer)
        {
            try
            {
                API_Customer Objcustomer = new API_Customer();
                return JsonConvert.SerializeObject(Objcustomer.AddCustomer(ObjCustomer));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateCustomer(string ObjCustomer)
        {
            try
            {
                API_Customer Objcustomer = new API_Customer();
                return JsonConvert.SerializeObject(Objcustomer.UpdateCustomer(ObjCustomer));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetCustomer(int CustomerId)
        {
            try
            {
                API_Customer Objcustomer = new API_Customer();
                return JsonConvert.SerializeObject(Objcustomer.GetCustomer(CustomerId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteCustomer(int CustomerId)
        {
            try
            {
                API_Customer Objcustomer = new API_Customer();
                return JsonConvert.SerializeObject(Objcustomer.DeleteCustomer(CustomerId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}