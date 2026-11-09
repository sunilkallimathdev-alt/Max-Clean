using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Coupon_Model : Base_Model
    {
        public int CouponID { get; set; }
        public string CouponCode { get; set; }
        public string CouponDescription { get; set; }
        public double? CouponMinPrice { get; set; }
        public DateTime CouponValidFrom { get; set; }
        public DateTime CouponValidUpto { get; set; }
        public bool? IsActive { get; set; }
    }
}