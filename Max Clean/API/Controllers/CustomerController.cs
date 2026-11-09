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
    public class CustomerController : ApiController
    {
        API.IRepository.Customer_IRepository ObjCustomers = new API.Repository.Customer_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();
        // GET: api/Customer
        [HttpGet]
        public dynamic Get()
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjCustomers.GetAllCustomers();

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                return ObjErrorResponce;
            }
        }

        // GET: api/Customer/5
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

        public dynamic Get(int id1, int id2)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjCustomers.GetActiveCustomers();
                ObjSuccessResponce_Model.ReceivedParameters = id1 + "," + id2;

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                ObjErrorResponce.ReceivedParameters = id1 + "," + id2;
                return ObjErrorResponce;
            }
        }
        public dynamic Get(int id1, int id2,int id3)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjCustomers.GetNonPerformingCustomer();
                ObjSuccessResponce_Model.ReceivedParameters = id1 + "," + id2 + "," + id3;

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                ObjErrorResponce.ReceivedParameters = id1 + "," + id2;
                return ObjErrorResponce;
            }
        }

        public dynamic Get(int id1, int id2, int id3,int id4)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjCustomers.GetOrders_of_Customer(id1);
                ObjSuccessResponce_Model.ReceivedParameters = id1 + "," + id2 + "," + id3;

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                ObjErrorResponce.ReceivedParameters = id1 + "," + id2;
                return ObjErrorResponce;
            }
        }
        //[HttpPost]
        //public dynamic Post([FromBody]int id,Customer_Model value)
        //{
        //    try
        //    {
        //        var Res = ObjCustomers.GetCustomerID(value);

        //        ObjSuccessResponce_Model.Result = Res;
        //        ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
        //        ObjSuccessResponce_Model.ObjectResult = Res;
        //        ObjSuccessResponce_Model.ReceivedParameters = value;

        //        return ObjSuccessResponce_Model;
        //    }
        //    catch (Exception ex)
        //    {
        //        Send_Exception_Mail.SendExceptionMail(ex);
        //        ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
        //        ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
        //        ObjErrorResponce.ReceivedParameters = value;
        //        return ObjErrorResponce;
        //    }
        //}

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


        // PUT: api/Customer/5
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
        
        // DELETE: api/Customer/5
        [HttpDelete]
        public dynamic Delete(int id)
        {
            try
            {
                var Res = ObjCustomers.Delete(id);
                if (Res > 0)
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                else
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
                ObjSuccessResponce_Model.ReceivedParameters = id;

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                if (!ex.Message.ToString().Contains("An error occurred while updating the entries. See the inner exception for details"))
                    Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString().Replace("An error occurred while updating the entries. See the inner exception for details", "Cant Delete Contact Admin");
                ObjErrorResponce.ReceivedParameters = id;
                return ObjErrorResponce;
            }
        }
    }
}
