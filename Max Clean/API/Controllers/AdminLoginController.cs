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
    public class AdminLoginController : ApiController
    {
        API.IRepository.AdminLogin_IRepository ObjStores = new API.Repository.AdminLogin_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();


        [HttpPut]
        public dynamic Put(AdminUser_Model objAdmin)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjStores.ForgotPassword(objAdmin);
                ObjSuccessResponce_Model.ReceivedParameters = objAdmin;

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                ObjErrorResponce.ReceivedParameters = objAdmin;

                return ObjErrorResponce;
            }
        }


        // POST: api/AdminLogi
        [HttpPost]
        public dynamic Post([FromBody]AdminUser_Model value)
        {
            try
            {
                var Res = ObjStores.login(value);
                if (Res != null)
                {
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                    ObjSuccessResponce_Model.Message = "Login Success";
                }
                else
                {
                    ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                    ObjSuccessResponce_Model.Message = "Failed to login";
                }
                ObjSuccessResponce_Model.ObjectResult = Res;

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
