using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Subscriber_Model:Base_Model
    {
        public int SubscriberID { get; set; }
        public string EmailID { get; set; }
    }
}