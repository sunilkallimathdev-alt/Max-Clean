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
    public class PurchaseOrderPaymentController : ApiController
    {
        API.IRepository.PurchaseOrderPayment_IRepository ObjStores = new API.Repository.PurchaseOrderPayment_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();

        // GET: api/VendorProductOrderPayment/5
        public dynamic Get(long id)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjStores.GetAllTransactionByVendorID(id);
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

        public dynamic Get(long id1, int id2)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjStores.SendPaymentMail(id1);
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

        // POST: api/VendorProductOrderPayment
        public dynamic Post([FromBody]PurchaseOrderPayment_Model value)
        {
            try
            {
                var Res = ObjStores.AddPaymentToVendorProductOrder(value);
                if (Res > 0)
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
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
    }
}
