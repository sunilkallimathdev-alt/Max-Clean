using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ServicePrice_Model: Base_Model
    {
        public int ServicePriceID { get; set; }
        public int? ServiceID { get; set; }
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? ProductId { get; set; }
        public string ServiceName { get; set; }
        public string ProductName { get; set; }
        public int? Unit { get; set; }
        public double? Price { get; set; }

       
    }
}