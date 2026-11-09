using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult Index()
        {   
            API_TicketSubject Objticket = new API_TicketSubject();
            ViewBag.Ticket = Objticket.GetTicketSubjectList();
            return View();
        }
        public dynamic GetTicketList()
        {
            try
            {
                API_Ticket Objrole = new API_Ticket();
                return JsonConvert.SerializeObject(Objrole.GetTicketList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddTicket(string ObjTicket)
        {
            try
            {
                API_Ticket Objrole = new API_Ticket();
                return JsonConvert.SerializeObject(Objrole.AddTicket(ObjTicket));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateTicket(string ObjTicket)
        {
            try
            {
                API_Ticket Objrole = new API_Ticket();
                return JsonConvert.SerializeObject(Objrole.UpdateTicket(ObjTicket));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetTicket(int TicketID)
        {
            try
            {
                API_Ticket Objrole = new API_Ticket();
                return JsonConvert.SerializeObject(Objrole.GetTicket(TicketID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteTicket(int TicketID)
        {
            try
            {
                API_Ticket Objrole = new API_Ticket();
                return JsonConvert.SerializeObject(Objrole.DeleteTicket(TicketID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}