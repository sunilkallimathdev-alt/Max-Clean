using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class TicketSubject_Model:Base_Model
    {
        public int TicketSubjectID { get; set; }
        public String SubjectName { get; set; }
        public int? DisplayOrder { get; set; }
    }
}