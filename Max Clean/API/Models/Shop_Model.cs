using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Shop_Model
    {
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
        public string CategoryBanner { get; set; }
        public List<Shop_Product> ProductList { get; set; }
    }
    public class Shop_Product
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public double? Price { get; set; }
        
    }

    
}