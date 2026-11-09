using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Service_IRepository
    {
        int Add(Service_Model ObjService);
        int Update(Service_Model ObjService);
        int Delete(int ServiceID);
        Service_Model GetServiceByID(int ServiceID);
        List<Service_Model> GetActiveService();
        List<Service_Model> GetAllService();
    }
}
