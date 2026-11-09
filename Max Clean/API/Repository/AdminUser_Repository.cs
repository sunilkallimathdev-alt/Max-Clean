using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class AdminUser_Repository : AdminUser_IRepository
    {
        public int Add(AdminUser_Model ObjAdminUsers)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    //var Exist = Context.t_user.FirstOrDefault(x => x.mobile_no == ObjAdminUsers.MobileNumber || x.email_id == ObjAdminUsers.EmailID);
                    var Exist = Context.t_user.FirstOrDefault(x => x.user_name == ObjAdminUsers.UserName);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_user ObjCoun = new t_user();
                        ObjCoun.role_id = ObjAdminUsers.RoleID;
                        ObjCoun.user_name = ObjAdminUsers.UserName;
                        ObjCoun.email_id = ObjAdminUsers.EmailID;
                        ObjCoun.mobile_no = ObjAdminUsers.MobileNumber;
                        ObjCoun.password = ObjAdminUsers.Password;
                        ObjCoun.address = ObjAdminUsers.Address;
                        ObjCoun.store_id = ObjAdminUsers.StoreID;
                        ObjCoun.is_active = ObjAdminUsers.IsActive;
                        ObjCoun.created_by = ObjAdminUsers.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_user.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.user_id;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(AdminUser_Model ObjAdminUsers)
        {

            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    //var Exist = Context.t_user.FirstOrDefault(x => (x.mobile_no == ObjAdminUsers.MobileNumber || x.email_id == ObjAdminUsers.EmailID) && x.user_id != ObjAdminUsers.UserID);
                    var Exist = Context.t_user.FirstOrDefault(x => (x.user_name == ObjAdminUsers.UserName) && x.user_id != ObjAdminUsers.UserID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_user.SingleOrDefault(x => x.user_id == ObjAdminUsers.UserID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjAdminUsers.UserName))
                                IDExist.user_name = ObjAdminUsers.UserName;
                            if (!string.IsNullOrEmpty(ObjAdminUsers.EmailID))
                                IDExist.email_id = ObjAdminUsers.EmailID;
                            if (!string.IsNullOrEmpty(ObjAdminUsers.MobileNumber))
                                IDExist.mobile_no = ObjAdminUsers.MobileNumber;
                            if (!string.IsNullOrEmpty(ObjAdminUsers.Password))
                                IDExist.password = ObjAdminUsers.Password;
                            if (!string.IsNullOrEmpty(ObjAdminUsers.Address))
                                IDExist.address = ObjAdminUsers.Address;

                            IDExist.store_id = ObjAdminUsers.StoreID;

                            if (ObjAdminUsers.RoleID != null)
                                IDExist.role_id = ObjAdminUsers.RoleID;

                            if (ObjAdminUsers.IsActive != null)
                                IDExist.is_active = ObjAdminUsers.IsActive;
                            IDExist.updated_by = ObjAdminUsers.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjAdminUsers.UserID;
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

        public int Delete(int UserID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_user adminuser = Context.t_user.SingleOrDefault(x => x.user_id == UserID);
                    Context.t_user.Remove(adminuser);
                    Context.SaveChanges();
                }
                return iRes = UserID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AdminUser_Model> GetActiveAdminUsers()
        {
            List<AdminUser_Model> ObjAdminUsers = new List<AdminUser_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjAdminUsers = (from a in Context.t_user
                                            where a.is_active == true
                                            select new AdminUser_Model
                                            {
                                                UserID = a.user_id,
                                                UserName = a.user_name,
                                                MobileNumber = a.mobile_no,
                                                EmailID = a.email_id,
                                                Address = a.address,
                                                RoleID = a.role_id,
                                                Password = a.password,
                                                RoleName = a.t_role.role_name,
                                                StoreID = a.store_id,
                                                StoreName = a.t_store.store_name,
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

        public List<AdminUser_Model> GetAllAdminUsers()
        {
            List<AdminUser_Model> ObjAdminUsers = new List<AdminUser_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjAdminUsers = (from a in Context.t_user
                                            select new AdminUser_Model
                                            {
                                                UserID = a.user_id,
                                                UserName = a.user_name,
                                                MobileNumber = a.mobile_no,
                                                EmailID = a.email_id,
                                                RoleID = a.role_id,
                                                Address = a.address,
                                                Password = a.password,
                                                RoleName = a.t_role.role_name,
                                                StoreID = a.store_id,
                                                StoreName = a.t_store.store_name,
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

        public AdminUser_Model GetAdminUsersByID(int UserID)
        {
            AdminUser_Model ObjAdminUser = new AdminUser_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjAdminUser = (from a in Context.t_user
                                           where a.user_id == UserID
                                           select new AdminUser_Model
                                           {
                                               UserID = a.user_id,
                                               UserName = a.user_name,
                                               MobileNumber = a.mobile_no,
                                               EmailID = a.email_id,
                                               Address = a.address,
                                               RoleID = a.role_id,
                                               Password = a.password,
                                               RoleName = a.t_role.role_name,
                                               StoreID = a.store_id,
                                               StoreName = a.t_store.store_name,
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