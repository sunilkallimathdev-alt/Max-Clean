using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class PurchaseOrder_Model : Base_Model
    {
        public long OrderID { get; set; }
        public int? CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public string PickupAddress { get; set; }
        public int? StoreID { get; set; }
        public string StoreName { get; set; }
        public string Store_code { get; set; }
        public string StoreMobileNo { get; set; }
        public string StoreEmail { get; set; }
        public string StoreAddress { get; set; }
        public string StoreCity { get; set; }
        public double? PaidAmount { get; set; }
        public double? Advance_Amount { get; set; }
        public double? RemainingAmount { get; set; }
        public double? GrandTotal { get; set; }
        public DateTime? ExpectingDeliveryDate { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ActualDeliveryDate { get; set; }
        public string Remark { get; set; }
        public int? orderstatusID { get; set; }
        public string OrderStatus { get; set; }
        public bool? IsOutletOrder { get; set; }
        public List<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedUser { get; set; }
        public int? PickedbyID { get; set; }
        public string Pickedby { get; set; }
        public int? DeliveredbyID { get; set; }
        public string DeliveryboyName { get; set; }
        public TimeSpan PickupTime { get; set; }
        public TimeSpan DeliveryTime { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Action { get; set; }
        public int DeliveryBoyID { get; set; }
        public bool? OrderChanged { get; set; }
        //public string DeliveryBoyName { get; set; }
        public string Admin_Sign_At_pickup { get; set; }
        public string Admin_Sign_At_Delivery { get; set; }
        public string Custm_Sign_At_pickup { get; set; }
        public string Custm_Sign_At_Delivery { get; set; }
        public string ReceivedOrder_AtStore_by { get; set; }
        public string DispatchedOrder_Atstore_by { get; set; }
        public string PaymentStatus { get; set; }
        public int Reports { get; set; }
        public bool? IsReward { get; set; }
        public String TagNumber { get; set; }
        public string RescheduledNote { get; set; }
        public string Invoice_No { get; set; }
        public double OldBalanceApplied { get; set; }
        public double ExpressChargeApplied { get; set; }
        public double? RewardPointsApplied { get; set; }
        public string PaymentType { get; set; }
        public bool? IsExpressDelivery { get; set; }

        public bool IsPromocodeApplied { get; set; }
        public string Promocode { get; set; }
        public double Promocode_Amount { get; set; }
        public double Promocode_Discount { get; set; }
        public double Delivery_charge { get; set; }
        public int? Pincode { get; set; }
        public long? OrderDetailsID { get; set; }
        public int? TotalQuantity { get; set; }
        // public long? OrderID { get; set; }
        public bool Is_MobileOrder { get; set; }
        public int? TimeslabId { get; set; }
        public int? ServiceID { get; set; }
        public string ServiceName { get; set; }
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public int? DeliveredQuantity { get; set; }
        public string Unit { get; set; }
        public double? UnitPrice { get; set; }
        public decimal? ExtraPercent { get; set; }
        public double? ExtraPrice { get; set; }

        public int? pickup_address_type { get; set; }
        public int? delivery_address_type { get; set; }
        public string City { get; set; }

        public int? rating { get; set; }
        public string comments { get; set; }

        //For Order Accept And Reject
        public int accept { get; set; }
        public int reject { get; set; }
        public Nullable<sbyte> is_cancel { get; set; }
        public long order_id { get; set; }
        public string reason_order_cancel { get; set; }


    }

    public class PurchaseOrderDetail
    {

        public int? ServiceID { get; set; }
        public string ServiceName { get; set; }
        public long? OrderDetailsID { get; set; }
        public long? OrderID { get; set; }
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public double? UnitPrice { get; set; }
        public decimal? ExtraPercent { get; set; }
        public double? ExtraPrice { get; set; }
        public int? Quantity { get; set; }

        public string Unit { get; set; }
        public int? DeliveredQuantity { get; set; }
        public int? RemainingQuantity { get; set; }
    }

    public class ActiveOrders_Model
    {
        public long OrderID { get; set; }
        public double? PaidAmount { get; set; }
        public double? GrandTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public int? orderstatusID { get; set; }
        public string OrderStatus { get; set; }

    }
}