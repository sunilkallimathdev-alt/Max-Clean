using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models;

namespace API.Models
{
    public class Pincode_Model:Base_Model
    {

        public int PincodeID { get; set; }
        public int? Pincode { get; set; }
       // public string Banner_Image { get; set; }

    }
}