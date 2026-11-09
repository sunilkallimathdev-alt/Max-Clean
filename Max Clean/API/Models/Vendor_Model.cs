
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Vendor_Model:Base_Model
    {
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public string MobileNumber { get; set; }
        public string AltMobileNumber { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set;}
        public string HSN { get; set; }
        public string Bank { get; set; }
        public string BankAccountNo { get; set; }
        public string IFSC { get; set; }
        public string Remark { get; set; }
             
    }
}