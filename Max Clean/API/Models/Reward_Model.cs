using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Reward_Model: Base_Model
    {
        public int RewardID { get; set; }
        public int? CustomerID { get; set; }
        public string CustomerName { get; set; }
        public Double? CustomerRewardPoints { get; set; }
        public Double? CustomerUsedRewardPoints { get; set; }
        public bool IsActive { get; set; }
    }
}