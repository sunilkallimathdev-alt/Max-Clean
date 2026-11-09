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
    public class GetOrdersbyDateController : ApiController
    {
        API.Repository.PurchaseOrder_Repository ObjAllOrders = new API.Repository.PurchaseOrder_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();
        //// GET: api/GetOrdersbyDate
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/GetOrdersbyDate/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/GetOrdersbyDate
        [HttpPost]
        public dynamic Post([FromBody]PurchaseOrder_Model value)
        {
            try
            {
                var Res = ObjAllOrders.GetAllOrders(value);
                
                ObjSuccessResponce_Model.Result = Res;
                //ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
                //ObjSuccessResponce_Model.ObjectResult = ObjAllOrders.GetAllOrders(Res);
               // ObjSuccessResponce_Model.ReceivedParameters = value;

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
        //// PUT: api/GetOrdersbyDate/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/GetOrdersbyDate/5
        //public void Delete(int id)
        //{
        //}
    }
}
