using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class PettyCash_Model : Base_Model
    {
        public int? Store_Id { get; set; }
        public string Store_Name { get; set; }
        public int? Petty_Cash_Id { get; set; }
        public decimal? Amount { get; set; }
        public string Reason { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Pdate { get; set; }
        public decimal? BalanceAmount { get; set; }

      

    }

    public class Expenses_Model : Base_Model
    {
        public int? Store_Id { get; set; }
        public int? User_Id { get; set; }
        public string Store_Name { get; set; }
        public string User_Name { get; set; }
        public int? Expenses_Id { get; set; }
        public decimal? Amount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public string Reason { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string date { get; set; }
    }

    public class PCashUtilzation_Model : Base_Model
    {
        public string Store_Name { get; set; }
        public int PCashUtil_id { get; set; }
        public int? Store_Id { get; set; }
        public string Reason { get; set; }
        public decimal? CreditedAmount { get; set; }
        public decimal? DebitedAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Pdate { get; set; }
       
    }
}