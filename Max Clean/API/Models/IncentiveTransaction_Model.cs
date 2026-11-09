using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class IncentiveTransaction_Model : Base_Model
    {
        public long InsentiveTransactionID { get; set; }
        public int? DeliveryBoyIncentiveID { get; set; }
        public string DeliveryBoyName { get; set; }
        public double? PaidAmountTrans { get; set; }
        public string PaymentType { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Note { get; set; }

        
    }
}