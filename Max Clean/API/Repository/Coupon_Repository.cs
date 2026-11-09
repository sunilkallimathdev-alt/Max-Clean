using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Coupon_Repository : Coupon_IRepository
    {
        public int Add(Coupon_Model ObjCoupon)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.coffeebrewEntities Context = new DataAccess.coffeebrewEntities())
                {
                    var Exist = Context.t_coupon.FirstOrDefault(x => x.coupon_id == ObjCoupon.CouponID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_coupon ObjCoun = new t_coupon();
                        ObjCoun.coupon_code = ObjCoupon.CouponCode;
                        ObjCoun.coupon_description = ObjCoupon.CouponDescription;
                        ObjCoun.coupon_minprice = ObjCoupon.CouponMinPrice;
                        ObjCoun.coupon_validfrom = ObjCoupon.CouponValidFrom;
                        ObjCoun.coupon_validupto = ObjCoupon.CouponValidUpto;
                        ObjCoun.is_active = ObjCoupon.IsActive;
                       
                        Context.t_coupon.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.coupon_id;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Coupon_Model ObjCoupon)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.coffeebrewEntities Context = new DataAccess.coffeebrewEntities())
                {
                    var Exist = Context.t_coupon.FirstOrDefault(x => x.coupon_code == ObjCoupon.CouponCode && x.coupon_id != ObjCoupon.CouponID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_coupon.SingleOrDefault(x => x.coupon_id == ObjCoupon.CouponID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjCoupon.CouponCode))
                                IDExist.coupon_code = ObjCoupon.CouponCode;
                            if (!string.IsNullOrEmpty(ObjCoupon.CouponDescription))
                                IDExist.coupon_description = ObjCoupon.CouponDescription;
                            if (ObjCoupon.CouponMinPrice  != null)
                                IDExist.coupon_minprice = ObjCoupon.CouponMinPrice;
                            if (ObjCoupon.CouponValidFrom != null)
                                IDExist.coupon_validfrom = ObjCoupon.CouponValidFrom;
                            if (ObjCoupon.CouponValidUpto != null)
                                IDExist.coupon_validupto = ObjCoupon.CouponValidUpto;


                            if (ObjCoupon.IsActive != null)
                                IDExist.is_active = ObjCoupon.IsActive;
                           
                            Context.SaveChanges();
                            iRes = ObjCoupon.CouponID;
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

        public List<Coupon_Model> GetActiveCoupons()
        {
            List<Coupon_Model> ObjCoupon = new List<Coupon_Model>();
            try
            {
                using (DataAccess.coffeebrewEntities Context = new DataAccess.coffeebrewEntities())
                {
                    return ObjCoupon = (from a in Context.t_coupon
                                           where a.is_active == true
                                           select new Coupon_Model
                                           {
                                               CouponID = a.coupon_id,
                                               CouponCode = a.coupon_code,
                                               CouponDescription = a.coupon_description,
                                               CouponMinPrice = a.coupon_minprice,
                                               CouponValidFrom = a.coupon_validfrom,
                                               CouponValidUpto = a.coupon_validupto,
                                               IsActive = a.is_active
                                              
                                           }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Coupon_Model> GetInActiveCoupons()
        {
            List<Coupon_Model> ObjCoupon = new List<Coupon_Model>();
            try
            {
                using (DataAccess.coffeebrewEntities Context = new DataAccess.coffeebrewEntities())
                {
                    return ObjCoupon = (from a in Context.t_coupon
                                           where a.is_active == false   
                                           select new Coupon_Model
                                           {
                                               CouponID = a.coupon_id,
                                               CouponCode = a.coupon_code,
                                               CouponDescription = a.coupon_description,
                                               CouponMinPrice = a.coupon_minprice,
                                               CouponValidFrom = a.coupon_validfrom,
                                               CouponValidUpto = a.coupon_validupto,
                                               IsActive = a.is_active

                                           }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Coupon_Model> GetAllCoupons()
        {
            List<Coupon_Model> ObjCoupon = new List<Coupon_Model>();
            try
            {
                using (DataAccess.coffeebrewEntities Context = new DataAccess.coffeebrewEntities())
                {
                    return ObjCoupon = (from a in Context.t_coupon
                                           select new Coupon_Model
                                           {
                                               CouponID = a.coupon_id,
                                               CouponCode = a.coupon_code,
                                               CouponDescription = a.coupon_description,
                                               CouponMinPrice = a.coupon_minprice,
                                               CouponValidFrom = a.coupon_validfrom,
                                               CouponValidUpto = a.coupon_validupto,
                                               IsActive = a.is_active


                                           }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Coupon_Model GetCouponByID(int CouponID)
        {
            Coupon_Model ObjCoupon = new Coupon_Model();
            try
            {
                using (DataAccess.coffeebrewEntities Context = new DataAccess.coffeebrewEntities())
                {
                    return ObjCoupon = (from a in Context.t_coupon
                                           where a.coupon_id == CouponID
                                           select new Coupon_Model
                                           {
                                               CouponID = a.coupon_id,
                                               CouponCode = a.coupon_code,
                                               CouponDescription = a.coupon_description,
                                               CouponMinPrice = a.coupon_minprice,
                                               CouponValidFrom = a.coupon_validfrom,
                                               CouponValidUpto = a.coupon_validupto,
                                               IsActive = a.is_active

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