using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Tag_IRepository
    {
        long UpdateTag(Tag_Model obj);
        Tag_Model Get_Tag_ById(long id);
        Base_Model GetAllTaggedOrders(string length, string start, string sortColumn, string sortColumnDir, string draw, string search);
        long ChangeOrders_Status(OrderList_Model obj);
        List<PurchaseOrder_Model> GetOrders_buStatusID(int id);
    }
}
