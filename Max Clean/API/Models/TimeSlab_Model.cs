using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class TimeSlab_Model
    {
        public int timeslab_id { get; set; }
        public string timeslab { get; set; }
        public TimeSpan? timevalue { get; set; }
        public bool? isActive { get; set; }
        public DateTime Date { get; set; }
    }
}