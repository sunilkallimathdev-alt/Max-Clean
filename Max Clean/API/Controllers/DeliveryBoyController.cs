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
    public class DeliveryBoyController : ApiController
    {
        API.IRepository.IDeliveryBoy_Repository ObjDelivery = new API.Repository.DeliveryBoy_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();

        // GET: api/DeliveryBoy/5
        [HttpGet]
        public dynamic Get(int id)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjDelivery.GetDeliveryBoyID(id);
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
        public dynamic Get()
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjDelivery.GetAllDeliveryBoys();

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

        [HttpPost]
        public dynamic Post([FromBody]DeliveryBoy_Model value)
        {
            try
            {
                if (value.is_login == 1)
                {
                    var Res = ObjDelivery.Add(value);
                    if (Res > 0)
                        ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                    else
                        ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                    ObjSuccessResponce_Model.Result = Res;
                    ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
                    ObjSuccessResponce_Model.ReceivedParameters = value;
                }
                else
                {
                    var Res = ObjDelivery.DeliveryBoyLogin(value);
                    ObjSuccessResponce_Model.Result = Res;
                    ObjSuccessResponce_Model.ObjectResult = ObjDelivery.GetDeliveryBoyID(Res);
                    ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
                    ObjSuccessResponce_Model.ReceivedParameters = value;
                }
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


        
        [HttpGet]
        public dynamic Get(long id1, long id2)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjDelivery.GetActiveDeliveryBoys();

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


        // PUT: api/DeliveryBoy/5
        [HttpPut]
        public dynamic Put([FromBody]DeliveryBoy_Model value)
        {
            try
            {
                var Res = ObjDelivery.Update(value);
                if (Res > 0)
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                else
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                ObjSuccessResponce_Model.Result = Res;
                ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
                ObjSuccessResponce_Model.ObjectResult = ObjDelivery.GetDeliveryBoyID(Res);
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

        // DELETE: api/DeliveryBoy/5
        [HttpDelete]
        public dynamic Delete(int id)
        {
            try
            {
                var Res = ObjDelivery.Delete(id);
                if (Res > 0)
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                else
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                ObjSuccessResponce_Model.Result = Res;
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
