using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class BatchVendorStock_Model : Batch_Model
    {
        public long? BatchVendorStockID { get; set; }
        public long? RequestedOrderID { get; set; }
        public double? Quantities { get; set; }
    }
}