using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface BatchVendor_IRepository
    {
        long AddBatchVendor(BatchVendor_Model ObjBV);

        List<VendorProductOrder_Model> GetRequestedOrderIDStock(long BatchID);
    }
}
