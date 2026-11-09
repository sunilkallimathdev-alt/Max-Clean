using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Unit_Model:Base_Model
    {
        public int UnitID { get; set; }
        public string UnitName { get; set; }
        public double? UnitScale { get; set; }
    }
}