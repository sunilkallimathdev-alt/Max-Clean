using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models;

namespace API.Models
{
    //public class VendorProductOrderPayment_ModelResult : Base_Model
    //{
    //    public long VendorPaymentOrderID { get; set; }
    //    public int? VendorID { get; set; }
    //    public string VendorName { get; set; }
    //    public string VendorMobileNO { get; set; }
    //    public int? StoreID { get; set; }
    //    public string StoreName { get; set; }
    //    public string StoreMobileNo { get; set; }
    //    public string VendorOrderID { get; set; }
    //    public double? PaidAmount { get; set; }
    //    public double? RemainingAmount { get; set; }
    //    public double? GrandTotal { get; set; }
    //    public double? PricePerKG { get; set; }
    //    public double? RequestedQuantity { get; set; }
    //    public double? ReceivedQuantity { get; set; }
    //    public DateTime? ExpectingDeliveryDate { get; set; }
    //    public DateTime? ActualDeliveryDate { get; set; }
    //    public string Remark { get; set; }

    //    public List<PurchaseOrderPayment_Model> TransactionList { get; set; }
    //}

    //public class PurchaseOrderPayment_Model : Base_Model
    //{
    //    public long VendorPaymentOrderPaymentID { get; set; }
    //    public long? VendorPaymentOrderID { get; set; }
    //    public double? PaidAmountTran { get; set; }
    //    public string PaymentType { get; set; }
    //    public string Note { get; set; }
    //    public DateTime TransactionDate { get; set; }
    //}

    public class VendorProductOrderPayment_ModelResult :pu
    {
        public long VendorPaymentOrderID { get; set; }
        public List<PurchaseOrderPayment_Model> TransactionList { get; set; }
    }

    public class PurchaseOrderPayment_Model : Base_Model
    {
        public long VendorPaymentOrderPaymentID { get; set; }
        public long? VendorPaymentOrderID { get; set; }
        public double PaidAmountTran { get; set; }
        public string PaymentType { get; set; }
        public string Note { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}