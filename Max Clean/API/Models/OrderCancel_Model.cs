using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class OrderCancel_Model
    {
        public int? store_id { get; set; }
        public long order_id { get; set; }
        public DateTime? order_date { get; set; }
        public string CancelDate { get; set; }
        public Nullable<sbyte> is_cancel { get; set; }
        public string store_name { get; set; }
        public string store_code { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Nullable<double> grandtotal_price { get; set; }
    }
}