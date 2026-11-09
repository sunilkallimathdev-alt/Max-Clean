using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface ICustomerOrderPlacement_Repository
    {
        long Add(PurchaseOrder_Model ObjPO);
    }
}
