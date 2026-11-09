using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface TicketSubject_IRepository
    {
        int Add(TicketSubject_Model ObjTicketSubject);
        int Update(TicketSubject_Model ObjTicketSubject);
        int Delete(int TicketSubjectID);
        TicketSubject_Model GetTicketSubjectByID(int TicketSubjectID);
        List<TicketSubject_Model> GetActiveTicketSubject();
        List<TicketSubject_Model> GetAllTicketSubject();
    }
}
