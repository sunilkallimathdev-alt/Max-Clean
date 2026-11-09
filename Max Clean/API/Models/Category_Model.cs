using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Category_Model : Base_Model
    {
        public int? ServiceID { get; set; }
        public string ServiceName { get; set; }

        public int? CategoryID { get; set; }

        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public int CategoryId { get; set; }
        //public int? ServiceID { get; set; }
        //public string ServiceName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
        public string CategoryBanner { get; set; }
        public int? DisplayOrder { get; set; }
        public string Description { get; set; }
        
    }
    public class mCategory_Model
    {
        
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
        public string CategoryBanner { get; set; }

    }
}