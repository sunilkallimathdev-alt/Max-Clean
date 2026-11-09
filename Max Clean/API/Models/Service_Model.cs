using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Service_Model: Base_Model
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string Image { get; set; }
        public int? DisplayOrder { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public List<Shop_Model> Categorylist { get; set; }
    }
}