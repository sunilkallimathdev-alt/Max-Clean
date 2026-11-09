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
    public class OTPController : ApiController
    {
        API.IRepository.Customer_IRepository ObjStores = new API.Repository.Customer_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();

        public dynamic Post([FromBody]string value)
        {
            try
            {
                var Res = ObjStores.SendOTP(value);
                if (Res > 0)
                {
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                    ObjSuccessResponce_Model.Message = "OTP SENT";
                }
                else
                {
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                    ObjSuccessResponce_Model.Message = "Failed to send the OTP";
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

        public dynamic Put(Customer_Model objAdmin)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjStores.OTPVerify(objAdmin.CustomerID);
                ObjSuccessResponce_Model.ReceivedParameters = objAdmin;

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                ObjErrorResponce.ReceivedParameters = objAdmin;

                return ObjErrorResponce;
            }
        }

    }
}
