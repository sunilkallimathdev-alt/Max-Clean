using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Promocode_Model : Base_Model
    {

        public int Promocode_ID { get; set; }
        public string Promocode_Name { get; set; }
        public double? Discount { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}