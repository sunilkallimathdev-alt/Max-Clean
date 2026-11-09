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
    public class GetOrderByCustomerIDController : ApiController
    {

        API.IRepository.IOrder_CycleStage_Repository ObjCycle = new API.Repository.Order_Cycle_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();

        [HttpGet]
        public dynamic Post(int id)


        {
            try
            {
                if (id > 0)
                {
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                    ObjSuccessResponce_Model.ObjectResult = ObjCycle.GetOrderByCustomerID(id);

                    return ObjSuccessResponce_Model;
                }
                else
                {
                   
                    ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                    ObjErrorResponce.Message = "Failure : Customer ID is zero or negative.";
                    return ObjErrorResponce;

                }
               
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                return ObjErrorResponce;
            }
        }

    }
}
