using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Role_Repository : Role_IRepository
    {
        public int Add(Role_Model ObjRole)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_role.FirstOrDefault(x => x.role_name == ObjRole.RoleName );
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_role ObjCoun = new t_role();
                        ObjCoun.role_name = ObjRole.RoleName;
                        ObjCoun.is_active = ObjRole.IsActive;
                        ObjCoun.created_by = ObjRole.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_role.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.role_id;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Role_Model ObjRole)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_role.FirstOrDefault(x => x.role_name == ObjRole.RoleName && x.role_id != ObjRole.RoleID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_role.SingleOrDefault(x => x.role_id == ObjRole.RoleID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjRole.RoleName))
                                IDExist.role_name = ObjRole.RoleName;
                            if (ObjRole.IsActive != null)
                                IDExist.is_active = ObjRole.IsActive;
                            IDExist.updated_by = ObjRole.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjRole.RoleID;
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
        public int Delete(int RoleID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_role role = Context.t_role.SingleOrDefault(x => x.role_id == RoleID);
                    Context.t_role.Remove(role);
                    Context.SaveChanges();
                }
                return iRes = RoleID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Role_Model> GetActiveRole()
        {
            List<Role_Model> ObjRole = new List<Role_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjRole = (from a in Context.t_role
                                            where a.is_active == true
                                            select new Role_Model
                                            {
                                                RoleID = a.role_id,
                                                RoleName = a.role_name,
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

        public List<Role_Model> GetAllRole()
        {
            List<Role_Model> ObjRole = new List<Role_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjRole = (from a in Context.t_role
                                            select new Role_Model
                                            {
                                                RoleID = a.role_id,
                                                RoleName = a.role_name,
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

        public Role_Model GetRoleByID(int RoleID)
        {
            Role_Model ObjRole = new Role_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjRole = (from a in Context.t_role
                                            where a.role_id == RoleID
                                            select new Role_Model
                                            {
                                                RoleID = a.role_id,
                                                RoleName = a.role_name,
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