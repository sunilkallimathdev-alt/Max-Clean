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
    public class InventoryTaggingController : Controller
    {
        // GET: InventoryTagging
        public ActionResult Index()
        {
            return View();
        }



        public dynamic AddTag(string objTag)
        {
            try
            {
                API_Tagging obj = new API_Tagging();
                return JsonConvert.SerializeObject(obj.AddTag(objTag));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public dynamic GetTags()
        {
            try
            {
                API_Tagging obj = new API_Tagging();
                return JsonConvert.SerializeObject(obj.GetTagsList());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetNonTagged()
        {
            try
            {

                API_Tagging obj = new API_Tagging();
                return JsonConvert.SerializeObject(obj.GetNonTaggedList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}