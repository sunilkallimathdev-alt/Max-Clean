using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class CustomerAddressController : Controller
    {
        // GET: CustomerAddress
        public ActionResult Index()
        {
            API_Customer Objcategory = new API_Customer();
            ViewBag.Customer = Objcategory.GetCustomerList();
            return View();
        }
      

        public dynamic GetCustomerAddressList()
        {
            try
            {
                API_CustomerAddress Objcustomeraddress = new API_CustomerAddress();
                return JsonConvert.SerializeObject(Objcustomeraddress.GetCustomerAddressList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddCustomerAddress(string ObjCustomerAddress)
        {
            try
            {
                API_CustomerAddress Objcustomeraddress = new API_CustomerAddress();
                return JsonConvert.SerializeObject(Objcustomeraddress.AddCustomerAddress(ObjCustomerAddress));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateCustomerAddress(string ObjCustomerAddress)
        {
            try
            {
                API_CustomerAddress Objcustomeraddress = new API_CustomerAddress();
                return JsonConvert.SerializeObject(Objcustomeraddress.UpdateCustomerAddress(ObjCustomerAddress));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetCustomerAddress(int CUstomerAddressID)
        {
            try
            {
                API_CustomerAddress Objcustomeraddress = new API_CustomerAddress();
                return JsonConvert.SerializeObject(Objcustomeraddress.GetCustomerAddress(CUstomerAddressID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteCustomerAddress(int CUstomerAddressID)
        {
            try
            {
                API_CustomerAddress Objcustomeraddress = new API_CustomerAddress();
                return JsonConvert.SerializeObject(Objcustomeraddress.DeleteCustomerAddress(CUstomerAddressID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetCustomerAddressByCustomerID(int CustomerID)
        {
            try
            {
                API_CustomerAddress Objcustomeraddress = new API_CustomerAddress();
                return JsonConvert.SerializeObject(Objcustomeraddress.GetCustomerAddressByCustomerID(CustomerID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}