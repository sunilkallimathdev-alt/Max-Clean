using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class DeliveryBoyDevice_Model: Base_Model
    {
        public int ID { get; set; }
        public int? DeliveryBoyID { get; set; }
        public string DeliveryBoyName { get; set; }
        public string DeviceID { get; set; }


    }
}