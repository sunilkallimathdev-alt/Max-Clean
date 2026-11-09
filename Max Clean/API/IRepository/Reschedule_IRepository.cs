using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Reschedule_IRepository
    {
        long Update(PurchaseOrder_Model Obj);
    }
}