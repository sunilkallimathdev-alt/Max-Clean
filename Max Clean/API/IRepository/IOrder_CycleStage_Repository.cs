using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface IOrder_CycleStage_Repository
    {
        
        int Add(orderCycle_Model ObjOrderCycle);
        int Update(orderCycle_Model ObjOrderCycle);
        int Delete(int OrderCycleID);
        orderCycle_Model GetOrderCycleByID(int OrderCycleID);
        List<orderCycle_Model> GetActiveOrderCycle();
        List<orderCycle_Model> GetAllOrderCycle();
        dynamic GetOrderByCustomerID(int CusId);

    }
}
