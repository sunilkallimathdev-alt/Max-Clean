using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Contact_Us_Model : Base_Model
    {
        public int ContactUsID { get; set; }
        public string ContactName { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
        public string Subject { get; set; }
        public string Query { get; set; }
    }
}