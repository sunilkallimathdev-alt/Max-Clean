using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class PaymentStatus_Model : Base_Model
    {
        public long paymentStatus_id { get; set; }
        public long order_id { get; set; }
        public string transaction_id { get; set; }
        public string payment_method { get; set; }
        public string payment_status { get; set; }
        public string bank_name { get; set; }
        public DateTime txn_date { get; set; }
        public string mode_of_payment { get; set; }

    }


}