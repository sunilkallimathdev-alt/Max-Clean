using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Marque_Repository : Marque_IRepository
    {
        public int Add(Marque_Model ObjMarque)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_marque.FirstOrDefault(x => x.title == ObjMarque.Marque_Name);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_marque ObjCoun = new t_marque();
                        ObjCoun.title = ObjMarque.Marque_Name;
                        ObjCoun.is_active = ObjMarque.IsActive;
                        ObjCoun.created_by = ObjMarque.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_marque.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.marque_id;
                        Context.SaveChanges();
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int MarqueID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_marque gallery = Context.t_marque.SingleOrDefault(x => x.marque_id == MarqueID);
                    Context.t_marque.Remove(gallery);
                    Context.SaveChanges();
                }
                return iRes = MarqueID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Marque_Model> GetActiveMarque()
        {
            List<Marque_Model> ObjMarque = new List<Marque_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjMarque = (from a in Context.t_marque
                                        where a.is_active == true
                                        select new Marque_Model
                                        {
                                            MarqueID = a.marque_id,
                                            Marque_Name = a.title,
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

        public List<Marque_Model> GetAllMarque()
        {
            List<Marque_Model> ObjMarque = new List<Marque_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjMarque = (from a in Context.t_marque
                                        select new Marque_Model
                                        {
                                            MarqueID = a.marque_id,
                                            Marque_Name = a.title,
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

        public Marque_Model GetMarqueByID(int MarqueID)
        {
            Marque_Model ObjMarque = new Marque_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjMarque = (from a in Context.t_marque
                                        where a.marque_id == MarqueID
                                        select new Marque_Model
                                        {
                                            MarqueID = a.marque_id,
                                            Marque_Name = a.title,
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

        public int Update(Marque_Model ObjMarque)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_marque.FirstOrDefault(x => x.title == ObjMarque.Marque_Name && x.marque_id != ObjMarque.MarqueID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_marque.SingleOrDefault(x => x.marque_id == ObjMarque.MarqueID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjMarque.Marque_Name))
                                IDExist.title = ObjMarque.Marque_Name;
                            if (ObjMarque.IsActive != null)
                                IDExist.is_active = ObjMarque.IsActive;
                            IDExist.updated_by = ObjMarque.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjMarque.MarqueID;
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
    }
}