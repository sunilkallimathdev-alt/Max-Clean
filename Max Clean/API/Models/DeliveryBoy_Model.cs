using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class DeliveryBoy_Model:Base_Model
    {
        public int DeliveryBoyID { get; set; }
        public string DeliveryBoyName { get; set; }
        public string DeliveryBoyEmailID { get; set; }
        public string DeliveryBoy_DeviceID { get; set; }
        public string DeliveryBoyMobileNumber { get; set; }
        public string DeliveryBoyAltMobileNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Vehicle_No { get; set; }
        public string Image { get; set; }
        public string Licence_No { get; set; }
        public string Languages { get; set; }
        public int is_login { get; set; }
        public double? Incentive { get; set; }

        public int Total_Pickup { get; set; }
        public int Open_Pickup { get; set; }
        public int Total_Delivery { get; set; }
        public int Open_Delivery { get; set; }
    }
}