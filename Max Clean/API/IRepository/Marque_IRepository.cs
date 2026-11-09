using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Marque_IRepository
    {

        int Add(Marque_Model ObjMarque);
        int Update(Marque_Model ObjMarque);
        int Delete(int MarqueID);
        Marque_Model GetMarqueByID(int MarqueID);
        List<Marque_Model> GetActiveMarque();
        List<Marque_Model> GetAllMarque();



    }
}
