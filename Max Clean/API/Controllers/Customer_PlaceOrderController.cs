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
    public class Customer_PlaceOrderController : ApiController
    {
        API.IRepository.ICustomerOrderPlacement_Repository ObjStores = new API.Repository.CustomerOrderPlacement_Repository();
        API.IRepository.Customer_IRepository Objcust = new API.Repository.Customer_Repository();
        API.IRepository.Pincode_IRepository Objpincode = new API.Repository.Pincode_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();


        //[HttpGet]
        //public dynamic Get(bool? id1, int id2)
        //{
        //    try
        //    {
        //        ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
        //        ObjSuccessResponce_Model.ObjectResult = ObjStores.GetAllOnlineOutletOrders(id1);

        //        return ObjSuccessResponce_Model;
        //    }
        //    catch (Exception ex)
        //    {
        //        Send_Exception_Mail.SendExceptionMail(ex);
        //        ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
        //        ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
        //        return ObjErrorResponce;
        //    }
        //}


        //// GET: api/Category/5
        //public dynamic Get(int id)
        //{
        //    try
        //    {
        //        ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
        //        ObjSuccessResponce_Model.ObjectResult = ObjStores.GetOrderByID(id);
        //        ObjSuccessResponce_Model.ReceivedParameters = id;

        //        return ObjSuccessResponce_Model;
        //    }
        //    catch (Exception ex)
        //    {
        //        Send_Exception_Mail.SendExceptionMail(ex);
        //        ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
        //        ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
        //        ObjErrorResponce.ReceivedParameters = id;

        //        return ObjErrorResponce;
        //    }
        //}


        // POST: api/PurchaseOrder

        [HttpPost]
        public dynamic Post([FromBody]PurchaseOrder_Model value)
        {
            try
            {
                // get pincode api 
                int storeid = Objpincode.GetStoreByPincode(Convert.ToInt32(value.Pincode));
                value.StoreID = (storeid != 0) ? storeid : 1;
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

        //[HttpGet]
        //public dynamic Get(int id1, int id2, int id3)
        //{
        //    try
        //    {
        //        ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
        //        ObjSuccessResponce_Model.ObjectResult = ObjStores.GetAllOrdersList();
        //        ObjSuccessResponce_Model.ReceivedParameters = id1 + "," + id2 + "," + id3;


        //        return ObjSuccessResponce_Model;
        //    }
        //    catch (Exception ex)
        //    {
        //        Send_Exception_Mail.SendExceptionMail(ex);
        //        ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
        //        ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
        //        ObjSuccessResponce_Model.ReceivedParameters = id1 + "," + id2 + "," + id3;
        //        return ObjErrorResponce;
        //    }
        //}

        //[HttpGet]
        //public dynamic Get(int id1, int id2, int id3, int id4)
        //{
        //    try
        //    {
        //        ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
        //        ObjSuccessResponce_Model.ObjectResult = ObjStores.GetOrdersByStoreId(id1);
        //        ObjSuccessResponce_Model.ReceivedParameters = id1 + "," + id2 + "," + id3 + "," + id4;


        //        return ObjSuccessResponce_Model;
        //    }
        //    catch (Exception ex)
        //    {
        //        Send_Exception_Mail.SendExceptionMail(ex);
        //        ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
        //        ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
        //        ObjSuccessResponce_Model.ReceivedParameters = id1 + "," + id2 + "," + id3 + "," + id4;
        //        return ObjErrorResponce;
        //    }
        //}

        //// PUT: api/PurchaseOrder
        [HttpPut]
        public dynamic Put([FromBody]Customer_Model value)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = Objcust.GetCustomers_ByDates(value);
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
