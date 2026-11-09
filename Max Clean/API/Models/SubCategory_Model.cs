using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class SubCategory_Model:Base_Model
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryIcon { get; set; }
        public string SubCategoryBanner { get; set; }
        public int? SubCategoryDisplayOrder { get; set; }
        

    }
}