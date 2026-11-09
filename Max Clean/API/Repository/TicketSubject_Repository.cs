using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class TicketSubject_Repository : TicketSubject_IRepository
    {
        public int Add(TicketSubject_Model ObjTicketSubject)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_tickets_subject.FirstOrDefault(x => x.subject == ObjTicketSubject.SubjectName);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_tickets_subject ObjCoun = new t_tickets_subject();
                        ObjCoun.subject = ObjTicketSubject.SubjectName;
                        ObjCoun.display_order = ObjTicketSubject.DisplayOrder;
                        ObjCoun.is_active = ObjTicketSubject.IsActive;
                        ObjCoun.created_by = ObjTicketSubject.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_tickets_subject.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.ticketsubject_id;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(TicketSubject_Model ObjTicketSubject)
        {

            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_tickets_subject.FirstOrDefault(x => x.subject == ObjTicketSubject.SubjectName && x.ticketsubject_id != ObjTicketSubject.TicketSubjectID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_tickets_subject.SingleOrDefault(x => x.ticketsubject_id == ObjTicketSubject.TicketSubjectID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjTicketSubject.SubjectName))
                                IDExist.subject = ObjTicketSubject.SubjectName;
                            if (ObjTicketSubject.DisplayOrder != null)
                                IDExist.display_order = ObjTicketSubject.DisplayOrder;

                            if (ObjTicketSubject.IsActive != null)
                                IDExist.is_active = ObjTicketSubject.IsActive;
                            IDExist.updated_by = ObjTicketSubject.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjTicketSubject.TicketSubjectID;
                        }
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int TicketSubjectID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_tickets_subject ticketsubject = Context.t_tickets_subject.SingleOrDefault(x => x.ticketsubject_id ==TicketSubjectID);
                    Context.t_tickets_subject.Remove(ticketsubject);
                    Context.SaveChanges();
                }
                return iRes = TicketSubjectID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TicketSubject_Model> GetActiveTicketSubject()
        {
            List<TicketSubject_Model> ObjTicketSubject = new List<TicketSubject_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjTicketSubject = (from a in Context.t_tickets_subject
                                            where a.is_active == true
                                            select new TicketSubject_Model
                                            {
                                                TicketSubjectID = a.ticketsubject_id,
                                                SubjectName = a.subject,
                                                DisplayOrder=a.display_order,
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

        public List<TicketSubject_Model> GetAllTicketSubject()
        {
            List<TicketSubject_Model> ObjTicketSubject = new List<TicketSubject_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjTicketSubject = (from a in Context.t_tickets_subject
                                      select new TicketSubject_Model
                                      {
                                          TicketSubjectID = a.ticketsubject_id,
                                          SubjectName = a.subject,
                                          DisplayOrder=a.display_order,
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

        public TicketSubject_Model GetTicketSubjectByID(int TicketSubjectID)
        {
            TicketSubject_Model ObjTikectSubject = new TicketSubject_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjTikectSubject = (from a in Context.t_tickets_subject
                                      where a.ticketsubject_id == TicketSubjectID
                                      select new TicketSubject_Model
                                      {
                                          TicketSubjectID = a.ticketsubject_id,
                                          SubjectName = a.subject,
                                          DisplayOrder=a.display_order,
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
