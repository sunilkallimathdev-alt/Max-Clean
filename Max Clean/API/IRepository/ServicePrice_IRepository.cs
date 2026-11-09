using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface ServicePrice_IRepository
    {
        int Add(ServicePrice_Model ObjServicePrice);
        int Update(ServicePrice_Model ObjServicePrice);
        int Delete(int ServicePriceID);
        ServicePrice_Model GetServicePriceByID(int ServicePriceID);
        //List<Category_Model> GetActiveCategory();
        List<ServicePrice_Model> GetAllServicePrice();
    }
}
