using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Ticket_Repository : Ticket_IRepository
    {
        public int Add(Ticket_Model ObjTicket)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {

                    t_ticket ObjCoun = new t_ticket();
                    ObjCoun.ticketsubject_id = ObjTicket.TicketSubjectID;
                    ObjCoun.order_id = ObjTicket.OrderID;
                    ObjCoun.query = ObjTicket.Query;
                    ObjCoun.is_active = ObjTicket.IsActive;
                    ObjCoun.created_by = ObjTicket.CreatedBy;
                    ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                    Context.t_ticket.Add(ObjCoun);
                    Context.SaveChanges();
                    iRes = ObjCoun.ticket_id;
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(Ticket_Model ObjTicket)
        {

            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var IDExist = Context.t_ticket.SingleOrDefault(x => x.ticket_id == ObjTicket.TicketID);
                    if (IDExist == null)
                    {
                        iRes = -4;
                    }
                    else
                    {
                        if (ObjTicket.TicketSubjectID != null)
                            IDExist.ticketsubject_id = ObjTicket.TicketSubjectID;
                        if (ObjTicket.OrderID != null)
                            IDExist.order_id = ObjTicket.OrderID;
                        if (!string.IsNullOrEmpty(ObjTicket.Query))
                            IDExist.query = ObjTicket.Query;
                        if (ObjTicket.IsActive != null)
                            IDExist.is_active = ObjTicket.IsActive;
                        IDExist.updated_by = ObjTicket.UpdateBy;
                        IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.SaveChanges();
                        iRes = ObjTicket.TicketID;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(int TicketID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_ticket ticket = Context.t_ticket.SingleOrDefault(x => x.ticket_id == TicketID);
                    Context.t_ticket.Remove(ticket);
                    Context.SaveChanges();
                }
                return iRes = TicketID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Ticket_Model> GetActiveTicket()
        {
            List<Ticket_Model> ObjTicket = new List<Ticket_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjTicket = (from a in Context.t_ticket
                                        where a.is_active == true
                                        select new Ticket_Model
                                        {
                                            TicketID = a.ticket_id,
                                            TicketSubjectID = a.ticketsubject_id,
                                            SubjectName = a.t_tickets_subject.subject,
                                            OrderID = a.order_id,
                                            Query = a.query,
                                            IsActive = a.is_active,
                                            CreatedBy = a.created_by,
                                            CreatedDate = a.created_date,
                                            UpdateBy = a.updated_by,
                                            UpdateDate = a.updated_date
                                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Ticket_Model> GetAllTicket()
        {
            List<Ticket_Model> ObjTicket = new List<Ticket_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjTicket = (from a in Context.t_ticket
                                        select new Ticket_Model
                                        {
                                            TicketID = a.ticket_id,
                                            TicketSubjectID = a.ticketsubject_id,
                                            SubjectName = a.t_tickets_subject.subject,
                                            OrderID = a.order_id,
                                            Query = a.query,
                                            IsActive = a.is_active,
                                            CreatedBy = a.created_by,
                                            CreatedDate = a.created_date,
                                            UpdateBy = a.updated_by,
                                            UpdateDate = a.updated_date
                                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Ticket_Model GetTicketByID(int TicketID)
        {
            Ticket_Model ObjTikect = new Ticket_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjTikect = (from a in Context.t_ticket
                                               where a.ticket_id == TicketID
                                               select new Ticket_Model
                                               {
                                                   TicketID = a.ticket_id,
                                                   TicketSubjectID = a.ticketsubject_id,
                                                   OrderID=a.order_id,
                                                   Query=a.query,
                                                   IsActive = a.is_active,
                                                   CreatedBy = a.created_by,
                                                   CreatedDate = a.created_date,
                                                   UpdateBy = a.updated_by,
                                                   UpdateDate = a.updated_date
                                               }).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}