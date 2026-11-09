using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class GeoLocation_Model
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Distance { get; set; }
    }
}