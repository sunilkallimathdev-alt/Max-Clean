using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    //public class DeliveryBoyInsentives_Model: Base_Model
    //{
    //    public long DeliveryBoyIncentiveID { get; set; }
    //    public int? DeliveryBoyID { get; set; }
    //    public string DeliveryBoyName { get; set; }
    //    public double? GrandTotal { get; set; }
    //    public double? PaidAmount { get; set; }
    //    public double? RemainingAmount { get; set; }
       
    //    public List<IncentiveTransaction_Model>TransactionList { get; set; }
    //}

    public class DeliveryBoyInsentives_Model 
    {
        public long? DeliveryBoyIncentiveID { get; set; }
        public int? DeliveryBoyID { get; set; }
        public string DeliveryBoyName { get; set; }
        public double? GrandTotal { get; set; }
        public double? PaidAmount { get; set; }
        public string PaymentType { get; set; }
        public double? RemainingAmount { get; set; }
        public string Note { get; set; }
        public List<PurchaseOrderPayment_Model> TransactionList { get; set; }
    }

    public class PurchaseOrderPayment_Model : Base_Model
    {
        public long IncentiveTransactionID { get; set; }
        public long? DeliveryBoyIncentiveID { get; set; }
        public double? PaidAmountTrs { get; set; }
        public string PaymentTypeTrs { get; set; }
        public string Note { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}