using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Contact_Us_IRepository
    {
        int Add(Contact_Us_Model ObjContactUs);
        int Update(Contact_Us_Model ObjContactUs);
        int Delete(int Contact_UsID);
        Contact_Us_Model GetContactUsByID(int Contact_UsID);
        List<Contact_Us_Model> GetActiveContactUs();
        List<Contact_Us_Model> GetAllContactUs();
    }
}
