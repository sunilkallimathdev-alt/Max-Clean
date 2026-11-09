using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class OrderList_Model
    {

        public string ReceivedOrder_AtStore_by { get; set; }
        public string DispatchedOrder_Atstore_by { get; set; }
        public string Admin_Sign_At_pickup { get; set; }
        public string Admin_Sign_At_Delivery { get; set; }
        public string TagNumber { get; set; }
        public int? PickedbyID { get; set; }
        public int? DeliveredbyID { get; set; }
        public int? StoreID { get; set; }
        public List<order> Orders { get; set; }
        public DateTime? TaggedDate { get; set; }
        public long? TagID { get; set; }
        public int? Order_Status { get; set; }

    }

    public class order
    {
        public long OrderID { get; set; }
        public long? OrdID { get; set; }
    } 

    public class Tag_Model
    {
        public long Tag_ID { get; set; }
        public string Tag_Number { get; set; }
        public long? Order_ID { get; set; }
        public DateTime? TaggedDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? Sent_to_factory { get; set; }
        public DateTime? Received_from_factory { get; set; }
        public string Tag_Note { get; set; }
        public string Order_Status { get; set; }
    }
}