using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Unit_Repository : Unit_IRepository
    {
        public int Add(Unit_Model ObjUnit)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_unit.FirstOrDefault(x => x.unit_name == ObjUnit.UnitName);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_unit ObjCoun = new t_unit();
                        ObjCoun.unit_name = ObjUnit.UnitName;
                        ObjCoun.unit_scale = ObjUnit.UnitScale;
                        ObjCoun.is_active = ObjUnit.IsActive;
                        ObjCoun.created_by = ObjUnit.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_unit.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.unit_id;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(Unit_Model ObjUnit)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_unit.FirstOrDefault(x => x.unit_name == ObjUnit.UnitName && x.unit_id != ObjUnit.UnitID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_unit.SingleOrDefault(x => x.unit_id == ObjUnit.UnitID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjUnit.UnitName))
                                IDExist.unit_name = ObjUnit.UnitName;
                            if (ObjUnit.UnitScale != null)
                                IDExist.unit_scale = ObjUnit.UnitScale;
                            if (ObjUnit.IsActive != null)
                                IDExist.is_active = ObjUnit.IsActive;
                            IDExist.updated_by = ObjUnit.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjUnit.UnitID;
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

        public int Delete(int UnitID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_unit unit = Context.t_unit.SingleOrDefault(x => x.unit_id == UnitID);
                    Context.t_unit.Remove(unit);
                    Context.SaveChanges();
                }
                return iRes = UnitID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Unit_Model> GetActiveUnit()
        {
            List<Unit_Model> ObjStores = new List<Unit_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjStores = (from a in Context.t_unit
                                        where a.is_active == true
                                        select new Unit_Model
                                        {
                                            UnitID = a.unit_id,
                                            UnitName = a.unit_name,
                                            UnitScale = a.unit_scale,
                                            IsActive=a.is_active,
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
        public List<Unit_Model> GetAllUnit()
        {

            List<Unit_Model> ObjUnit = new List<Unit_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjUnit = (from a in Context.t_unit
                                            select new Unit_Model
                                            {
                                                UnitID = a.unit_id,
                                                UnitName = a.unit_name,
                                                UnitScale = a.unit_scale,
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

        public Unit_Model GetUnitByID(int UnitID)
        {
            Unit_Model ObjUnit = new Unit_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjUnit = (from a in Context.t_unit
                                            where a.unit_id ==UnitID
                                            select new Unit_Model
                                            {
                                                UnitID = a.unit_id,
                                                UnitName = a.unit_name,
                                                UnitScale = a.unit_scale,
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