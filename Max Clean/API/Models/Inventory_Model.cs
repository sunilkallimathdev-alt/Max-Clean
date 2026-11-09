using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Inventory_Model
    {

        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public int? DeliveredQuantity { get; set; }
        public string Unit { get; set; }
        public double? UnitPrice { get; set; }
    }
}