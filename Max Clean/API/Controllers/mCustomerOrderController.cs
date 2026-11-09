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
    public class mCustomerOrderController : ApiController
    {

        API.IRepository.Customer_IRepository ObjCustomers = new API.Repository.Customer_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();

        [HttpGet]
        public dynamic Get(string CustomerID)
        {
            try
            {
                int customerID = Convert.ToInt32(CustomerID);
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjCustomers.GetActiveOrders_of_Customer(customerID);

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
