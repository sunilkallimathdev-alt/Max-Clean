using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class orderCycle_Model:Base_Model
    {
        public int OrderCycleID { get; set; }
        public string OrderCycleStage { get; set; }
        public int? DisplayOrder { get; set; }
    }
}