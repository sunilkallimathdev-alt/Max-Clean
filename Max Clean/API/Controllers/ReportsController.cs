using API.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    public class ReportsController : ApiController
    {
        API.IRepository.Reports_IRepository ObjReports = new API.Repository.Reports_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();

        
        // GET: api/Reports/5
        public dynamic Get(int id)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjReports.GetInventory(id);
                //ObjSuccessResponce_Model.ReceivedParameters = id;

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
               // ObjErrorResponce.ReceivedParameters = id;

                return ObjErrorResponce;
            }
        }

        public dynamic Get(int id1, int id2)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjReports.GetInventoryManagement(id1, id2);
                //ObjSuccessResponce_Model.ReceivedParameters = id;

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                // ObjErrorResponce.ReceivedParameters = id;

                return ObjErrorResponce;
            }
        }

        // POST: api/Reports
        public dynamic Post([FromBody]PurchaseOrder_Model value)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjReports.GetReports(value);

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

        // PUT: api/Reports/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Reports/5
        public void Delete(int id)
        {
        }
    }
}
