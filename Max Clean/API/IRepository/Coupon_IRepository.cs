using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Coupon_IRepository
    {
        int Add(Coupon_Model ObjCoupon);
        int Update(Coupon_Model ObjCoupon);

        Coupon_Model GetCouponByID(int CouponID);
        List<Coupon_Model> GetActiveCoupons();
        List<Coupon_Model> GetInActiveCoupons();
        List<Coupon_Model> GetAllCoupons();
    }
}
