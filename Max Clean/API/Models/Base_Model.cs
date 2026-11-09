using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Base_Model
    {
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdateBy { get; set; }
        public string Admin_name { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string draw { get; set; }
        public int recordsTotal { get; set; }
        public dynamic data { get; set; }
        public string search { get; set; }
        public int? ID { get; set; }

    }
}