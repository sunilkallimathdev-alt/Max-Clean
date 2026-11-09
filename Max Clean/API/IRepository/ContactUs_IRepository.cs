using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface ContactUs_IRepository
    {
        int Add(ContactUs_Model ObjContactUs);
        int Update(ContactUs_Model ObjContactUs);
        int Delete(int ContactUsID);
        ContactUs_Model GetContactUsByID(int ContactUsID);
        List<ContactUs_Model> GetActiveContactUs();
        List<ContactUs_Model> GetAllContactUs();
    }
}
