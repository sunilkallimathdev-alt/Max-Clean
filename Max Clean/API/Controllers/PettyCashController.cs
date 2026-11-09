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
    public class PettyCashController : ApiController
    {
        API.IRepository.PettyCash_IRepository ObjPCash = new Repository.PettyCash_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();
        // GET: PettyCash
        public dynamic Get( int id)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjPCash.GetPettyCashList(id);
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

        [HttpPost]

        public dynamic Post([FromBody] PettyCash_Model value)
        {
            try
            {
                var Res = ObjPCash.Add(value);
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

        //public dynamic Get(string length, string start, string sortColumn, string sortColumnDir, string draw, string search)
        //{
        //    try
        //    {
        //        ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
        //        ObjSuccessResponce_Model.ObjectResult = ObjPCash.GetPettyCashList(length, start, sortColumn, sortColumnDir, draw, search);
        //        ObjSuccessResponce_Model.ReceivedParameters = length + "," + start + "," + sortColumn + "," + start + "," + sortColumn + "," + search;

        //        return ObjSuccessResponce_Model;
        //    }
        //    catch (Exception ex)
        //    {
        //        Send_Exception_Mail.SendExceptionMail(ex);
        //        ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
        //        ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
        //        ObjErrorResponce.ReceivedParameters = length + "," + start + "," + sortColumn + "," + start + "," + sortColumn + "," + search;
        //        return ObjErrorResponce;
        //    }
        //}
    }
}