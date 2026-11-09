using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class TicketSubjectController : Controller
    {
        // GET: TicketSubject
        public ActionResult Index()
        {
            return View();
        }
        public dynamic GetTicketSubjectList()
        {
            try
            {
                API_TicketSubject Objrole = new API_TicketSubject();
                return JsonConvert.SerializeObject(Objrole.GetTicketSubjectList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddTicketSubject(string ObjTicketSubject)
        {
            try
            {
                API_TicketSubject Objrole = new API_TicketSubject();
                return JsonConvert.SerializeObject(Objrole.AddTicketSubject(ObjTicketSubject));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateTicketSubject(string ObjTicketSubject)
        {
            try
            {
                API_TicketSubject Objrole = new API_TicketSubject();
                return JsonConvert.SerializeObject(Objrole.UpdateTicketSubject(ObjTicketSubject));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetTicketSubject(int TicketSubjectID)
        {
            try
            {
                API_TicketSubject Objrole = new API_TicketSubject();
                return JsonConvert.SerializeObject(Objrole.GetTicketSubject(TicketSubjectID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteTicketSubject(int TicketSubjectID)
        {
            try
            {
                API_TicketSubject Objrole = new API_TicketSubject();
                return JsonConvert.SerializeObject(Objrole.DeleteTicketSubject(TicketSubjectID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}