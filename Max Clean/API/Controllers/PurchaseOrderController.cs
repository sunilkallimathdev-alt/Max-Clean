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
    public class PurchaseOrderController : ApiController
    {
        API.IRepository.PurchaseOrder_IRepository Objorder = new API.Repository.PurchaseOrder_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();

    
        [HttpGet]
        public dynamic Get(bool? id1, int id2)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = Objorder.GetAllOnlineOutletOrders(id1);

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

        public dynamic Get(string length, string start, string sortColumn, string sortColumnDir, string draw, string search,long id,string FromDate, string ToDate)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = Objorder.GetOrdersList_BystoreID(length,start,sortColumn,sortColumnDir,draw,search,id,FromDate,ToDate);
                ObjSuccessResponce_Model.ReceivedParameters = length + "," + start + "," + sortColumn + "," + start + "," + sortColumn + "," + search+","+id+","+ FromDate+","+ ToDate;

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                ObjErrorResponce.ReceivedParameters = length + "," + start + "," + sortColumn + "," + start + "," + sortColumn + "," + search + "," + id;

                return ObjErrorResponce;
            }
        }

        // GET: api/Category/5
        public dynamic Get(int id)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = Objorder.GetOrderByID(id);
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
     

       // POST: api/PurchaseOrder
       [HttpPost]
        public dynamic Post([FromBody]PurchaseOrder_Model value)
        {
            try
            {
                var Res = Objorder.Add(value);
                if (Res > 0)
                {
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                }
                else
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                ObjSuccessResponce_Model.Result = Res;
                ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
                //ObjSuccessResponce_Model.ObjectResult = ObjStores.GetOrderByID(Res);
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



        [HttpGet]
        public dynamic Get(int id1, int id2, int id3)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = Objorder.GetAllOrdersList();
                ObjSuccessResponce_Model.ReceivedParameters = id1 + "," + id2 + "," + id3;


                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                ObjSuccessResponce_Model.ReceivedParameters = id1 + "," + id2 + "," + id3;
                return ObjErrorResponce;
            }
        }

        [HttpGet]
        public dynamic Get(int id1, int id2, int id3,int id4)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = Objorder.GetOrdersByStoreId(id1);
                ObjSuccessResponce_Model.ReceivedParameters = id1 + "," + id2 + "," + id3+","+id4;


                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                ObjSuccessResponce_Model.ReceivedParameters = id1 + "," + id2 + "," + id3 + "," + id4;
                return ObjErrorResponce;
            }
        }

        // PUT: api/PurchaseOrder
        [HttpPut]
        public dynamic Put([FromBody]PurchaseOrder_Model value)
        {
            try
            {
                var Res = Objorder.UpdateDetails(value);
                if (Res > 0)
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                else
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                ObjSuccessResponce_Model.Result = Res;
                ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
               // ObjSuccessResponce_Model.ObjectResult = ObjStores.get(Res);
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

        //// DELETE: api/PurchaseOrder
        //[HttpDelete]
        //public dynamic Delete([FromBody]PurchaseOrder_Model value)
        //{
        //    try
        //    {
        //        var Res = ObjStores.Delete(value);
        //        if (Res > 0)
        //            ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
        //        else
        //            ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
        //        ObjSuccessResponce_Model.Result = Res;
        //        ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
        //        ObjSuccessResponce_Model.ReceivedParameters = value;
        //        return ObjSuccessResponce_Model;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!ex.Message.ToString().Contains("An error occurred while updating the entries. See the inner exception for details"))
        //            Send_Exception_Mail.SendExceptionMail(ex);
        //        ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
        //        ObjErrorResponce.Message = "Failure : " + ex.Message.ToString().Replace("An error occurred while updating the entries. See the inner exception for details", "Cant Delete Contact Admin");
        //        ObjErrorResponce.ReceivedParameters = value;
        //        return ObjErrorResponce;
        //    }
        //}
    }
}
