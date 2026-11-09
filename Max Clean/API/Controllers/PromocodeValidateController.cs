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
    public class PromocodeValidateController : ApiController
    {
        API.IRepository.Customer_IRepository ObjCustomers = new API.Repository.Customer_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();

        [HttpPost]
        public dynamic Post([FromBody]Promocode_Model value)
        {
            try
            {
                var Res = ObjCustomers.Promocode(value);
                if (Res != null)
                {
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                    ObjSuccessResponce_Model.Message = "Valid Promocode";
                }
                else
                {
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                    ObjSuccessResponce_Model.Message = "Invalid Promocode";
                }
                ObjSuccessResponce_Model.ObjectResult = Res;

                ObjSuccessResponce_Model.ReceivedParameters = value;

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                //Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                ObjErrorResponce.ReceivedParameters = value;
                return ObjErrorResponce;
            }
        }
    }
}
