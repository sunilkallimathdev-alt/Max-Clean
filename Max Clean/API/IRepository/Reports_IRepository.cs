using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Reports_IRepository
    {
       dynamic GetReports(PurchaseOrder_Model Obj);

        List<Inventory_Model> GetInventory(int Id);

        List<Inventory_Model> GetInventoryManagement(int id1, int id2);
    }
}
