using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class ContactUs_Repository : ContactUs_IRepository
    {
        public int Add(ContactUs_Model ObjContactUs)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                  
                        t_enquiries ObjCoun = new t_enquiries();
                        ObjCoun.contact_name = ObjContactUs.ContactName;
                        ObjCoun.email_id = ObjContactUs.EmailID;
                        ObjCoun.mobile_number = ObjContactUs.MobileNumber;
                    
                        ObjCoun.message = ObjContactUs.Query;

                        ObjCoun.is_active = ObjContactUs.IsActive;
                        ObjCoun.created_by = ObjContactUs.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_enquiries.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.contact_us_id;

                   
                    UpdateMail(iRes);



                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool UpdateMail(int ID)
        {
            bool request = false;
            ContactUs_Model ObjUser = new ContactUs_Model();
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    var Res = Context.t_enquiries.FirstOrDefault(c => c.contact_us_id == ID);
                    if (Res != null)
                    {
                        if (Res.email_id != null)
                        {

                            ObjUser.ContactName = Res.contact_name;
                            ObjUser.EmailID = Res.email_id;
                            var emailFrom = API.CommonClasses.EmailSMSCredentials.FromEmail;
                            var password = API.CommonClasses.EmailSMSCredentials.Password;


                            AdminLogin_Repository fr = new AdminLogin_Repository();
                            if (ObjUser.EmailID != null)
                            {
                                fr.SendMail(
                                    ObjUser.EmailID,
                                    "Company Enquiry",
                                    emailFrom,
                                    password,
                                    "Dear " + ObjUser.ContactName + ","
                                      + Environment.NewLine + ""
                                    + Environment.NewLine + "Welcome to Company."
                                    + Environment.NewLine + "Thanks for Contacting Us "
                                    + Environment.NewLine + "Will Contact You Soon"
                                    + Environment.NewLine + "- Team Company.");
                            }
                            request = true;
                        }
                    }
                    else
                    {
                        request = false;
                    }
                }
            }
            catch (Exception ex)
            {
                return true;
            }
            return request;
        }

            public int Delete(int ContactUsID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_enquiries contactus = Context.t_enquiries.SingleOrDefault(x => x.contact_us_id == ContactUsID);
                    Context.t_enquiries.Remove(contactus);
                    Context.SaveChanges();
                }
                return iRes = ContactUsID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(ContactUs_Model ObjContactUs)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_enquiries.FirstOrDefault(x => x.contact_name == ObjContactUs.ContactName && x.contact_us_id != ObjContactUs.ContactUsID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_enquiries.SingleOrDefault(x => x.contact_us_id == ObjContactUs.ContactUsID);
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
                            if (!string.IsNullOrEmpty(ObjContactUs.MobileNumber))
                                IDExist.mobile_number = ObjContactUs.MobileNumber;
                            if (!string.IsNullOrEmpty(ObjContactUs.Query))
                                IDExist.message = ObjContactUs.Query;

                            if (ObjContactUs.IsActive != null)
                                IDExist.is_active = ObjContactUs.IsActive;
                            IDExist.update_by = ObjContactUs.UpdateBy;
                            IDExist.update_date = CommonClasses.StaticData.GetCurrentDataTime;
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

        public List<ContactUs_Model> GetActiveContactUs()
        {
            List<ContactUs_Model> ObjContactUs = new List<ContactUs_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjContactUs = (from a in Context.t_enquiries
                                           where a.is_active == true
                                           select new ContactUs_Model
                                           {
                                               ContactUsID = a.contact_us_id,
                                               ContactName = a.contact_name,
                                               EmailID = a.email_id,
                                               MobileNumber = a.mobile_number,
                                               Query = a.message,
                                               IsActive = a.is_active,
                                               CreatedBy = a.created_by,
                                               CreatedDate = a.created_date,
                                               UpdateBy = a.update_by,
                                               UpdateDate = a.update_date
                                           }).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ContactUs_Model> GetAllContactUs()
        {
            List<ContactUs_Model> ObjContactUs = new List<ContactUs_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjContactUs = (from a in Context.t_enquiries
                                           select new ContactUs_Model
                                           {
                                               ContactUsID = a.contact_us_id,
                                               ContactName = a.contact_name,
                                               EmailID = a.email_id,
                                               MobileNumber = a.mobile_number,
                                               Query = a.message,
                                               IsActive = a.is_active,
                                               CreatedBy = a.created_by,
                                               CreatedDate = a.created_date,
                                               UpdateBy = a.update_by,
                                               UpdateDate = a.update_date
                                           }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ContactUs_Model GetContactUsByID(int ContactUsID)
        {
            ContactUs_Model ObjContactUs = new ContactUs_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjContactUs = (from a in Context.t_enquiries
                                            where a.contact_us_id == ContactUsID
                                            select new ContactUs_Model
                                            {
                                                ContactUsID = a.contact_us_id,
                                                ContactName = a.contact_name,
                                                EmailID = a.email_id,
                                                MobileNumber = a.mobile_number,
                                       
                                                Query = a.message,

                                                IsActive = a.is_active,
                                                CreatedBy = a.created_by,
                                                CreatedDate = a.created_date,
                                                UpdateBy = a.update_by,
                                                UpdateDate = a.update_date
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