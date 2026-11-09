using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Batch_Model : Base_Model
    {
        public long BatchID { get; set; }
        public string BatchName { get; set; }
        public int? StoreID { get; set; }
        public string StoreName { get; set; }
        public string StoreMobile { get; set; }
        public DateTime? BatchStarted { get; set; }
        public DateTime? BatchEnded { get; set; }
        public DateTime? Exp_Date { get; set; }
        public double? AveragePrice { get; set; }
    }
}