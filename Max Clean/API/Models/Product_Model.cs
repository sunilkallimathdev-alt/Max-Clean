using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Product_Model : Base_Model
    {
        public int ProductId { get; set; }
        public int? ServiceProductID { get; set; }
        public int? ServiceID { get; set; }
        public string ServiceName { get; set; }
        public double? ServicePrice { get; set; }
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
        public string CategoryBanner { get; set; }
        public int? SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public string ProductCode { get; set; }
        public string ProductImage { get; set; }
        public string ProductVideo { get; set; }
        public bool? Is_EndUserProduct { get; set; }
        public string TagLine { get; set; }
        public string ProductDescription { get; set; }
        public int? ProductDisplayOrder { get; set; }
        public double? Price { get; set; }
        public double? OfferPrice { get; set; }
    }
    public class mProduct_Model
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public double? Price { get; set; }
    }
}