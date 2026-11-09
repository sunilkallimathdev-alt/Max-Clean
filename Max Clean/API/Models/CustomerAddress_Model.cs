using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class CustomerAddress_Model : Base_Model
    {
        public int CUstomerAddressID { get; set; }
        public int? CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string AddressType { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string Latitude { get; set; }
        public string Longitute { get; set; }
    }
}