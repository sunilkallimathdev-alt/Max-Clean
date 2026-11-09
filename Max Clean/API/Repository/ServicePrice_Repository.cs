using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class ServicePrice_Repository : ServicePrice_IRepository
    {
        public int Add(ServicePrice_Model ObjServicePrice)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_service_price.FirstOrDefault(x => x.service_id == ObjServicePrice.ServiceID && x.product_id == ObjServicePrice.ProductId);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_service_price ObjCoun = new t_service_price();
                        ObjCoun.service_id = ObjServicePrice.ServiceID;
                        ObjCoun.product_id = ObjServicePrice.ProductId;

                        ObjCoun.unit = ObjServicePrice.Unit;
                        ObjCoun.price = ObjServicePrice.Price;
                        ObjCoun.is_active = ObjServicePrice.IsActive;
                        ObjCoun.created_by = ObjServicePrice.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_service_price.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.service_price_id;
                       
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

        public int Update(ServicePrice_Model ObjServicePrice)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_service_price.FirstOrDefault(x => x.service_id == ObjServicePrice.ServiceID && x.product_id ==ObjServicePrice.ProductId && x.service_price_id != ObjServicePrice.ServicePriceID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_service_price.SingleOrDefault(x => x.service_price_id == ObjServicePrice.ServicePriceID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (ObjServicePrice.ServiceID != null)
                                IDExist.service_id = ObjServicePrice.ServiceID;
                            if (ObjServicePrice.ProductId != null)
                                IDExist.product_id = ObjServicePrice.ProductId;

                            if (ObjServicePrice.Unit != null)
                                IDExist.unit = ObjServicePrice.Unit;
                            if (ObjServicePrice.Price != null)
                                IDExist.price = ObjServicePrice.Price;
                            if (ObjServicePrice.IsActive != null)
                                IDExist.is_active = ObjServicePrice.IsActive;

                            IDExist.updated_by = ObjServicePrice.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjServicePrice.ServicePriceID;
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

        public int Delete(int ServicePriceID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_service_price serviceprice = Context.t_service_price.SingleOrDefault(x => x.service_price_id == ServicePriceID);
                    Context.t_service_price.Remove(serviceprice);
                    Context.SaveChanges();
                }
                return iRes = ServicePriceID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ServicePrice_Model> GetAllServicePrice()
        {
            List<ServicePrice_Model> ObjServicePrice = new List<ServicePrice_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjServicePrice = (from a in Context.t_service_price
                                            select new ServicePrice_Model
                                            {
                                                ServicePriceID = a.service_price_id,
                                                ServiceID = a.service_id,
                                                ServiceName = a.t_service.service_name,
                                                CategoryID = a.t_product.category_id,
                                                CategoryName=a.t_product.t_category.category_name,
                                                ProductId = a.product_id,
                                                ProductName = a.t_product.product_name,
                                                Unit = a.unit,
                                                Price = a.price,
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

        public ServicePrice_Model GetServicePriceByID(int ServicePriceID)
        {
            ServicePrice_Model ObjServicePrice = new ServicePrice_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjServicePrice = (from a in Context.t_service_price
                                            where a.service_price_id == ServicePriceID
                                            select new ServicePrice_Model
                                            {
                                                ServicePriceID = a.service_price_id,
                                                ServiceID = a.service_id,
                                                ServiceName = a.t_service.service_name,
                                                CategoryID=a.t_product.category_id,
                                                CategoryName=a.t_product.t_category.category_name,
                                                ProductId = a.product_id,
                                                ProductName = a.t_product.product_name,
                                                Unit = a.unit,
                                                Price = a.price,
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