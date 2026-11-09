using API.CommonClasses;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.DataAccess;

namespace API.Controllers
{
    public class DeliveryBoyDeviceController : ApiController
    {
        API.IRepository.DeliveryBoyDevice_IRepository ObjDeliveryBoyDevice = new API.Repository.DeliveryBoyDevice_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();
        [HttpGet]
        public dynamic Get()
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjDeliveryBoyDevice.GetAllDeliveryBoyDevice();

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
        //// GET: api/DeliveryBoyDevice
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //GET: api/DeliveryBoyDevice/5
        public dynamic Get(int id)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjDeliveryBoyDevice.GetDeliveryBoyDeviceByDeliveryBoyID(id);
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

        // POST: api/DeliveryBoyDevice
        [HttpPost]
        public dynamic Post([FromBody]DeliveryBoyDevice_Model value)
        {
            try
            {
                var Res = ObjDeliveryBoyDevice.Add(value);
                if (Res > 0)
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                else
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                ObjSuccessResponce_Model.Result = Res;
                ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
                ObjSuccessResponce_Model.ObjectResult = ObjDeliveryBoyDevice.GetDeliveryBoyDeviceByDeliveryBoyID(Res);
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

        //// PUT: api/DeliveryBoyDevice/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/DeliveryBoyDevice/5
        //public void Delete(int id)
        //{
        //}
    }
}
