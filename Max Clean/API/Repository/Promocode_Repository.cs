using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace API.Repository
{
    public class Promocode_Repository : Promocode_IRepository
    {
        public int Add(Promocode_Model ObjPromocode)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_promocode.FirstOrDefault(x => x.promocode_name == ObjPromocode.Promocode_Name);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_promocode ObjCoun = new t_promocode();
                        ObjCoun.promocode_name = ObjPromocode.Promocode_Name;
                        ObjCoun.discount = ObjPromocode.Discount;
                        ObjCoun.is_active = ObjPromocode.IsActive;
                        ObjCoun.created_by = ObjPromocode.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        ObjCoun.updated_date = ObjCoun.created_date;
                        ObjCoun.title = ObjPromocode.Title;
                        ObjCoun.description = ObjPromocode.Description;
                        Context.t_promocode.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.promocode_id;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int PromocodeId)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_promocode category = Context.t_promocode.SingleOrDefault(x => x.promocode_id == PromocodeId);
                    Context.t_promocode.Remove(category);
                    Context.SaveChanges();
                }
                return iRes = PromocodeId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Promocode_Model> GetActivePromocode()
        {
            List<Promocode_Model> ObjCategories = new List<Promocode_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCategories = (from a in Context.t_promocode
                                            where a.is_active == true
                                            select new Promocode_Model
                                            {
                                                Promocode_ID = a.promocode_id,
                                                Promocode_Name = a.promocode_name,
                                                Discount = a.discount,
                                                Title = a.title,
                                                Description = a.description,
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

        public Promocode_Model GetPromocodeByID(int PromocodeId)
        {
            Promocode_Model ObjCategories = new Promocode_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCategories = (from a in Context.t_promocode
                                            where a.promocode_id == PromocodeId
                                            select new Promocode_Model
                                            {
                                                Promocode_ID = a.promocode_id,
                                                Promocode_Name = a.promocode_name,
                                                Discount = a.discount,
                                                Title = a.title,
                                                Description = a.description,
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

        public Base_Model GetPromocodeList(string length, string start, string sortColumn, string sortColumnDir, string draw, string search)
        {
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            List<Promocode_Model> ObjCategories = new List<Promocode_Model>();
            using (API.DataAccess.maxcleanEntities Context = new API.DataAccess.maxcleanEntities())
            {
                // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
                Context.Configuration.LazyLoadingEnabled = false;

                ObjCategories = (from a in Context.t_promocode
                                 select new Promocode_Model
                                 {
                                     Promocode_ID = a.promocode_id,
                                     Promocode_Name = a.promocode_name,
                                     Discount = a.discount,
                                     Title = a.title,
                                     Description = a.description,
                                     IsActive = a.is_active,
                                     CreatedBy = a.created_by,
                                     CreatedDate = a.created_date,
                                     UpdateBy = a.updated_by,
                                     UpdateDate = a.updated_date
                                 }).ToList();

                // Verification.   
                if (!string.IsNullOrEmpty(search) &&
                    !string.IsNullOrWhiteSpace(search) && (search != "$0$"))
                {
                    // Apply search   
                    ObjCategories = ObjCategories.Where(p => p.Promocode_Name.ToString().ToLower().Contains(search.ToLower()) ||
                        p.Discount.ToString().Contains(search.ToLower())
                     ).ToList();
                }


                ////SORT
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    ObjCategories = ObjCategories.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                }

                recordsTotal = ObjCategories.Count();
                var data = ObjCategories.Skip(skip).Take(pageSize).ToList();
                Base_Model obj = new Base_Model();
                obj.draw = draw;
                obj.recordsTotal = recordsTotal;
                obj.data = data;

                return obj;
            }
        }

        public int Update(Promocode_Model ObjPromocode)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_promocode.FirstOrDefault(x => x.promocode_name == ObjPromocode.Promocode_Name && x.promocode_id != ObjPromocode.Promocode_ID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_promocode.SingleOrDefault(x => x.promocode_id == ObjPromocode.Promocode_ID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjPromocode.Promocode_Name))
                                IDExist.promocode_name = ObjPromocode.Promocode_Name;
                            if (!string.IsNullOrEmpty(ObjPromocode.Title))
                                IDExist.title = ObjPromocode.Title;
                            if (!string.IsNullOrEmpty(ObjPromocode.Description))
                                IDExist.description = ObjPromocode.Description;
                            if (ObjPromocode.Discount != 0)
                                IDExist.discount = ObjPromocode.Discount;
                            if (ObjPromocode.IsActive != null)
                                IDExist.is_active = ObjPromocode.IsActive;
                            IDExist.updated_by = ObjPromocode.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjPromocode.Promocode_ID;
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