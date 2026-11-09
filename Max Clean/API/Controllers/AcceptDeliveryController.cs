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
    public class AcceptDeliveryController : ApiController
    {
        // GET: api/AcceptDelivery
        API.IRepository.PurchaseOrder_IRepository ObjOrder = new API.Repository.PurchaseOrder_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();


        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AcceptDelivery/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AcceptDelivery
        public dynamic Post([FromBody]OrderList_Model value)
        {
            try
            {
                var Res = ObjOrder.SubmitDeliveryAccepted(value);
                if (Res > 0)
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                else
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                ObjSuccessResponce_Model.Result = Res;
                ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
                //ObjSuccessResponce_Model.ObjectResult = ObjOrder.GetOrderByID(Res);
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

        // PUT: api/AcceptDelivery/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AcceptDelivery/5
        public void Delete(int id)
        {
        }
    }
}
