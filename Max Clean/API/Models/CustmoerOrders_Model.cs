using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class CustmoerOrders_Model
    {
        public long OrderID;
        public DateTime? deliverydate;

        public DateTime? ordereddate;
        public orderCycle_Model status;
        public double? total;
        public List<CustmoerOrderDetails_Model> orderList;
        public string DeliveryAddress;
        public DateTime? expectedDeliveryDate;
        public int store_id;
        public DateTime? expected_delivery_date;
        public DateTime? pickup_date;
        public TimeSpan? pickup_time;
        public string pickup_address;
        public TimeSpan? delivery_time;
        public string pickedup_by;
        public double? paid_amount;
        public int balance_amount;
        public string payment_Status;
        public string payment_type;
        public string order_status;
        public bool? isActive;
        public double? balanceAmount;
        public double? CustomerRewardPoints { get; set; }

    }

    public class CustmoerOrderDetails_Model
    {
        public string product;
        public string service;
        public int? quantity;
        public int price;
        public double? subtotal;
        public double? extraPrice;

    }
}