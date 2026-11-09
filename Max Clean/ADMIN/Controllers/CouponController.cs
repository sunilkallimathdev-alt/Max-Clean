using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class CouponController : Controller
    {
        // GET: Coupon
        public ActionResult Index()
        {
            return View();
        }

        public dynamic GetCouponList()
        {
            try
            {
                API_Coupon Objcoupon = new API_Coupon();
                return JsonConvert.SerializeObject(Objcoupon.GetCouponList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddCoupon(string ObjCoupon)
        {
            try
            {
                API_Coupon Objcoupon = new API_Coupon();
                return JsonConvert.SerializeObject(Objcoupon.AddCoupon(ObjCoupon));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateCoupon(string ObjCoupon)
        {
            try
            {
                API_Coupon Objcoupon = new API_Coupon();
                return JsonConvert.SerializeObject(Objcoupon.UpdateCoupon(ObjCoupon));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetCoupon(int CouponID)
        {
            try
            {
                API_Coupon Objcoupon = new API_Coupon();
                return JsonConvert.SerializeObject(Objcoupon.GetCoupon(CouponID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}