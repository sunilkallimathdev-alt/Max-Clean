using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Customer_Model: Base_Model
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string EmailID { get; set; }
        public string MobileNumber { get; set; }
        public string AltMobileNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PickUPAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public double? OldBalance { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? Pincode { get; set; }
        public int Total_Cust_Orders { get; set; }
        public int Closed_Cust_Orders { get; set; }
        public int Open_Cust_Orders { get; set; }

        public Boolean? isMobileVerified { get; set; }
        public int RewardID { get; set; }
        public double? CustomerRewardPoints { get; set; }
        public double? CustomerUsedRewardPoints { get; set; }

        public int? pickup_address_type { get; set; }
        public int? delivery_address_type { get; set; }
        public string city { get; set; }
        //public string zipcode { get; set; }

    }
    public class CustomerRating_Model : Base_Model
    {
        public int CustomerRatingID { get; set; }
        public int CustomerID { get; set; }
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string EmailID { get; set; }
        public string MobileNumber { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }

    }