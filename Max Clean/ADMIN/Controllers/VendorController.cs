using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADMIN.API_Repository;
using Newtonsoft.Json;

namespace ADMIN.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult Index()

        {
            return View();
        }

        // GET: Vendor
        public dynamic GetVendorList()
        {
            try
            {
                API_Vendor Objvendor = new API_Vendor();
                return JsonConvert.SerializeObject(Objvendor.GetVendorList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddVendor(string ObjVendor)
        {
            try
            {
                API_Vendor Objvendor = new API_Vendor();
                return JsonConvert.SerializeObject(Objvendor.AddVendor(ObjVendor));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateVendor(string ObjVendor)
        {
            try
            { 
                API_Vendor Objvendor = new API_Vendor();
                return JsonConvert.SerializeObject(Objvendor.UpdateVendor(ObjVendor));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetVendor(int VendorID)
        {
            try
            {
                API_Vendor Objvendor = new API_Vendor();
                return JsonConvert.SerializeObject(Objvendor.GetVendor(VendorID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteVendor(int VendorID)
        {
            try
            {
                API_Vendor Objvendor = new API_Vendor();
                return JsonConvert.SerializeObject(Objvendor.DeleteVendor(VendorID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}