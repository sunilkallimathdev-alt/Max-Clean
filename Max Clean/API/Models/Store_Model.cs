using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Store_Model : Base_Model
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public string ManagerName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Store_code { get; set; }
        public decimal? Radius { get; set; }
    }
}