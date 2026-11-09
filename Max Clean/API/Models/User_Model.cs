using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class User_Model
    {
        public int UserID { get; set; }
        public int? RoleID { get; set; }
        public string RoleName { get; set; }
        public int? StoreID { get; set; }
        public string StoreName { get; set; }
        public string UserName { get; set; }
        public string EmailID { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int PinCode { get; set; }
    }
}