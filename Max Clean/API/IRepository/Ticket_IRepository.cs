using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Ticket_IRepository
    {
        int Add(Ticket_Model ObjTicket);
        int Update(Ticket_Model ObjTicket);
        int Delete(int TicketID);
        Ticket_Model GetTicketByID(int TicketID);
        List<Ticket_Model> GetActiveTicket();
        List<Ticket_Model> GetAllTicket();
    }
}
