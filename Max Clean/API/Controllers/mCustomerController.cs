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
    public class mCustomerController : ApiController
    {
        API.IRepository.Customer_IRepository ObjCustomers = new API.Repository.Customer_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();

        // POST: api/Customer
        [HttpPost]
        public dynamic Post([FromBody]Customer_Model value)
        {
            try
            {
                var Res = ObjCustomers.Add(value);
                if (Res > 0)
                {
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                    ObjSuccessResponce_Model.ObjectResult = ObjCustomers.GetCustomerByID(Res);
                }
                else if (Res == -1)
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.Conflict;
                else
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                ObjSuccessResponce_Model.Result = Res;
                ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);

                ObjSuccessResponce_Model.ReceivedParameters = value;

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                ObjErrorResponce.ReceivedParameters = value;
                return ObjErrorResponce;
            }
        }

        [HttpPut]
        public dynamic Put([FromBody]Customer_Model value)
        {
            try
            {
                var Res = ObjCustomers.Update(value);
                if (Res > 0)
                {
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                    ObjSuccessResponce_Model.ObjectResult = ObjCustomers.GetCustomerByID(Res);
                }
                else
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
                // ObjSuccessResponce_Model.ObjectResult = ObjCustomers.GetCustomerByID(Res);
                ObjSuccessResponce_Model.ReceivedParameters = value;

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                ObjErrorResponce.ReceivedParameters = value;
                return ObjErrorResponce;
            }
        }

       
        public dynamic Get(int id)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjCustomers.GetCustomerByID(id);
                ObjSuccessResponce_Model.ReceivedParameters = id;

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                ObjErrorResponce.ReceivedParameters = id;

                return ObjErrorResponce;
            }
        }
        [HttpGet]
        public dynamic Get(string CustomerID)
        {
            try
            {
                int customerID = Convert.ToInt32(CustomerID);
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjCustomers.GetOrders_of_Customer_mobile(customerID);

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
