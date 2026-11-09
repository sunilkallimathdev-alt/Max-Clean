using API.CommonClasses;
using API.DataAccess;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class RescheduleController : ApiController
    {

        API.IRepository.Reschedule_IRepository Obj = new API.Repository.Reschedule_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();

        [HttpPost]
        public dynamic Post([FromBody]PurchaseOrder_Model value)
        {
            try
            {
                long res = 0;
                PurchaseOrder_Model ObjModel = value;
                if (ObjModel != null && ObjModel.OrderID > 0 && ObjModel.StoreID > 0)
                {
                    using (maxcleanEntities context = new maxcleanEntities())
                    {
                        var isexist = context.t_orders.Where(x => x.order_id == ObjModel.OrderID).FirstOrDefault();
                        if (isexist != null)
                        {
                            isexist.store_id = ObjModel.StoreID;
                        }
                        context.SaveChanges();
                        res = isexist.order_id;
                    }
                    if (res > 0)
                    {
                        ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                    }
                    ObjSuccessResponce_Model.Result = res;
                    ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(res);
                    ObjSuccessResponce_Model.ReceivedParameters = value;
                    return ObjSuccessResponce_Model;
                }
                return res;
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


        [HttpPut]
        public dynamic Put([FromBody]PurchaseOrder_Model value)
        {
            try
            {
                var Res = Obj.Update(value);
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
