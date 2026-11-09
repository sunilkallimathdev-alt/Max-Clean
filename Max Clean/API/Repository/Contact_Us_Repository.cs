using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Contact_Us_Repository : Contact_Us_IRepository
    {
        public int Add(Contact_Us_Model ObjContactUs)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_contact_us.FirstOrDefault(x => x.contact_name == ObjContactUs.ContactName);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_contact_us ObjCoun = new t_contact_us();
                        ObjCoun.contact_name = ObjContactUs.ContactName;
                        ObjCoun.email_id = ObjContactUs.EmailID;
                        ObjCoun.mobile_no = ObjContactUs.MobileNo;
                        ObjCoun.subject = ObjContactUs.Subject;
                        ObjCoun.query = ObjContactUs.Query;
                        ObjCoun.is_active = ObjContactUs.IsActive;
                        ObjCoun.created_by = ObjContactUs.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_contact_us.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.contact_us_id;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int ContactUs_ID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_contact_us contactus = Context.t_contact_us.SingleOrDefault(x => x.contact_us_id == ContactUs_ID);
                    Context.t_contact_us.Remove(contactus);
                    Context.SaveChanges();
                }
                return iRes = ContactUs_ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Contact_Us_Model ObjContactUs)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_contact_us.FirstOrDefault(x => x.contact_name == ObjContactUs.ContactName && x.contact_us_id != ObjContactUs.ContactUsID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_contact_us.SingleOrDefault(x => x.contact_us_id == ObjContactUs.ContactUsID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjContactUs.ContactName))
                                IDExist.contact_name = ObjContactUs.ContactName;
                            if (!string.IsNullOrEmpty(ObjContactUs.EmailID))
                                IDExist.email_id = ObjContactUs.EmailID;
                            if (!string.IsNullOrEmpty(ObjContactUs.MobileNo))
                                IDExist.mobile_no = ObjContactUs.MobileNo;
                            if (!string.IsNullOrEmpty(ObjContactUs.Subject))
                                IDExist.subject = ObjContactUs.Subject;
                            if (!string.IsNullOrEmpty(ObjContactUs.Query))
                                IDExist.query = ObjContactUs.Query;


                            if (ObjContactUs.IsActive != null)
                                IDExist.is_active = ObjContactUs.IsActive;
                            IDExist.updated_by = ObjContactUs.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjContactUs.ContactUsID;
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

        public List<Contact_Us_Model> GetActiveContactUs()
        {
            List<Contact_Us_Model> ObjContactUss = new List<Contact_Us_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjContactUss = (from a in Context.t_contact_us
                                            where a.is_active == true
                                           select new Contact_Us_Model
                                           {
                                               ContactUsID = a.contact_us_id,
                                               ContactName = a.contact_name,
                                               EmailID = a.email_id,
                                               MobileNo = a.mobile_no,
                                               Subject = a.subject,
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

        public List<Contact_Us_Model> GetAllContactUs()
        {
            List<Contact_Us_Model> ObjContactUss = new List<Contact_Us_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjContactUss = (from a in Context.t_contact_us
                                            select new Contact_Us_Model
                                            {
                                               ContactUsID = a.contact_us_id,
                                               ContactName = a.contact_name,
                                               EmailID = a.email_id,
                                               MobileNo = a.mobile_no,
                                               Subject = a.subject,
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

        public Contact_Us_Model GetContactUsByID(int ContactUs_ID)
        {
            Contact_Us_Model ObjContactUss = new Contact_Us_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjContactUss = (from a in Context.t_contact_us
                                            where a.contact_us_id == ContactUs_ID
                                            select new Contact_Us_Model
                                           {
                                               ContactUsID = a.contact_us_id,
                                               ContactName = a.contact_name,
                                               EmailID = a.email_id,
                                               MobileNo = a.mobile_no,
                                               Subject = a.subject,
                                               Query = a.query,

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