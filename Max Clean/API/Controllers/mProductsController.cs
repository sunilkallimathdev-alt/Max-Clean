using API.CommonClasses;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace API.Controllers
{
    public class mProductsController : ApiController
    {
        API.IRepository.Product_IRepository Objproduct = new API.Repository.Product_Repository();
        API.IRepository.Category_IRepository Objcategory = new API.Repository.Category_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();

        [HttpGet]
        public dynamic Get(int id)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = Objcategory.GetCategoriesByServiceID(id);

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
               
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                return ObjErrorResponce;
            }
        }

        [HttpGet]
        public dynamic Get(string serviceid, string categoryid)
        {
            try
            {
                int serviceID = Convert.ToInt32(serviceid);
                int categoryID = Convert.ToInt32(categoryid);
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = Objproduct.GetProductByCategoryServiceID(serviceID,categoryID);

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {

                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                return ObjErrorResponce;
            }
        }
    }
}
