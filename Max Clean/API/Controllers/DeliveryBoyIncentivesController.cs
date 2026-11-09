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
    public class DeliveryBoyIncentivesController : ApiController
    {
        API.IRepository.DeliveryBoyIncentives_IRepository ObjDeliveryBoyIncentives = new API.Repository.DeliveryBoyIncentives_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();

        public dynamic Get(int id)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjDeliveryBoyIncentives.GetAllTransactionByDeliveryBoyID(id);
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

        //public dynamic Get(int id1,int id2)
        //{
        //    try
        //    {
        //        ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
        //        ObjSuccessResponce_Model.ObjectResult = ObjDeliveryBoyIncentives(id2);
        //        ObjSuccessResponce_Model.ReceivedParameters = id1+","+ id2;

        //        return ObjSuccessResponce_Model;
        //    }
        //    catch (Exception ex)
        //    {
        //        Send_Exception_Mail.SendExceptionMail(ex);
        //        ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
        //        ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
        //        ObjErrorResponce.ReceivedParameters = id1 + "," + id2;

        //        return ObjErrorResponce;
        //    }
        //}
        //// GET: api/DeliveryBoyIncentives
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/DeliveryBoyIncentives/5
        //public string Get(int id)
        //{
        //    return "value";


        // POST: api/DeliveryBoyIncentives
        [HttpPost]
        public dynamic Post([FromBody]DeliveryBoyInsentives_Model value)
        {
            try
            {
                var Res = ObjDeliveryBoyIncentives.AddDeliveryBoyIncentives(value);
                if (Res > 0)
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                else
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                ObjSuccessResponce_Model.Result = Res;
                ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
                //ObjSuccessResponce_Model.ObjectResult = ObjDeliveryBoyIncentives.AddDeliveryBoyIncentives(Res);
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


        // PUT: api/DeliveryBoyIncentives/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DeliveryBoyIncentives/5
        public void Delete(int id)
        {
        }
    }
}
