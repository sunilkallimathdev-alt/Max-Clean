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
    public class mPaymentStatusController : ApiController
    {
        //API.IRepository.ICustomerOrderPlacement_Repository ObjStores = new API.Repository.CustomerOrderPlacement_Repository();
        //API.IRepository.Customer_IRepository Objcust = new API.Repository.Customer_Repository();
        //API.IRepository.Pincode_IRepository Objpincode = new API.Repository.Pincode_Repository();
        //API.IRepository.PurchaseOrder_IRepository Objorder = new API.Repository.PurchaseOrder_Repository();

        API.IRepository.IPaymentStatus_Repository ObjStores = new API.Repository.PaymentStatus_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();


        [HttpPost]
        public dynamic Post([FromBody]PaymentStatus_Model value)
        {
            try
            {  
                var Res = ObjStores.Add(value);
                if (Res > 0)
                {
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                }
                ObjSuccessResponce_Model.Result = Res;
                ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);                

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
               // ObjErrorResponce.ReceivedParameters = value;
                return ObjErrorResponce;
            }
        }

       
        


    }
}
