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
    public class VendorController : ApiController
    {
        API.IRepository.Vendor_IRepository ObjStores = new API.Repository.Vendor_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();
        [HttpGet]
        public dynamic Get()
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjStores.GetAllVendors();

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
        // GET: api/Vendor/5
        public dynamic Get(int id)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjStores.GetVendorByID(id);
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
        public dynamic Get(int id1, int id2)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjStores.GetActiveVendors();
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

        // POST: api/Vendor
        
        [HttpPost]
        public dynamic Post([FromBody]Vendor_Model value)
        {
            try
            {
                var Res = ObjStores.Add(value);
                if (Res > 0)
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                else
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                ObjSuccessResponce_Model.Result = Res;
                ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
                ObjSuccessResponce_Model.ObjectResult = ObjStores.GetVendorByID(Res);
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

        // PUT: api/Vendor/5
        [HttpPut]
        public dynamic Put([FromBody]Vendor_Model value)
        {
            try
            {
                var Res = ObjStores.Update(value);
                if (Res > 0)
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                else
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                ObjSuccessResponce_Model.Result = Res;
                ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
                ObjSuccessResponce_Model.ObjectResult = ObjStores.GetVendorByID(Res);
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

        // DELETE: api/Vendor/5
        [HttpDelete]
        public dynamic Delete(int id)
        {
            try
            {
                var Res = ObjStores.Delete(id);
                if (Res > 0)
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                else
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                ObjSuccessResponce_Model.Message = GetCustomErrorType.GetCustomError(Res);
                ObjSuccessResponce_Model.ReceivedParameters = id;
                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                if (!ex.Message.ToString().Contains("An error occurred while updating the entries. See the inner exception for details"))
                    Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString().Replace("An error occurred while updating the entries. See the inner exception for details", "Cant Delete Contact Admin");
                ObjErrorResponce.ReceivedParameters = id;
                return ObjErrorResponce;
            }
        }
    }
}
