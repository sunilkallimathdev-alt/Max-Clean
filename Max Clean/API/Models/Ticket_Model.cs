using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Ticket_Model:Base_Model
    {

        public int TicketID { get; set; }
        public int? TicketSubjectID { get; set; }
        public string SubjectName { get; set; }
        public int? OrderID { get; set; }
        public string Query { get; set; }

    }
}