using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Subscriber_Repository : Subscriber_IRepository
    {
        public int Add(Subscriber_Model ObjSubscriber)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_subscriber
                        .FirstOrDefault(x => x.emailid == ObjSubscriber.EmailID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_subscriber ObjCoun = new t_subscriber();
                        ObjCoun.emailid = ObjSubscriber.EmailID;
                        ObjCoun.is_active = ObjSubscriber.IsActive;
                        ObjCoun.created_by = ObjSubscriber.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_subscriber.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.subscriberid;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(Subscriber_Model ObjSubscriber)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_subscriber.FirstOrDefault(x => x.emailid == ObjSubscriber.EmailID && x.subscriberid != ObjSubscriber.SubscriberID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_subscriber.SingleOrDefault(x => x.subscriberid == ObjSubscriber.SubscriberID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjSubscriber.EmailID))
                                IDExist.emailid = ObjSubscriber.EmailID;
                            if (ObjSubscriber.IsActive != null)
                                IDExist.is_active = ObjSubscriber.IsActive;
                            IDExist.updated_by = ObjSubscriber.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjSubscriber.SubscriberID;
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
        public int Delete(int SubscriberID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_subscriber role = Context.t_subscriber.SingleOrDefault(x => x.subscriberid == SubscriberID);
                    Context.t_subscriber.Remove(role);
                    Context.SaveChanges();
                }
                return iRes = SubscriberID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Subscriber_Model> GetActiveSubscribers()
        {
            List<Subscriber_Model> ObjRole = new List<Subscriber_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjRole = (from a in Context.t_subscriber
                                      where a.is_active == true
                                      select new Subscriber_Model
                                      {
                                          SubscriberID = a.subscriberid,
                                          EmailID = a.emailid,
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

        public List<Subscriber_Model> GetAllSubscribers()
        {
            List<Subscriber_Model> ObjRole = new List<Subscriber_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjRole = (from a in Context.t_subscriber
                                      select new Subscriber_Model
                                      {
                                          SubscriberID = a.subscriberid,
                                          EmailID = a.emailid,
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

        public Subscriber_Model GetSubscriberByID(int SubscriberID)
        {
            Subscriber_Model ObjRole = new Subscriber_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjRole = (from a in Context.t_subscriber
                                      where a.subscriberid == SubscriberID
                                      select new Subscriber_Model
                                      {
                                          SubscriberID = a.subscriberid,
                                          EmailID = a.emailid,
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