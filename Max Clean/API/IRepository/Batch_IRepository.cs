using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Batch_IRepository
    {
        long Add(Batch_Model ObjBatch);
        long Update(Batch_Model ObjBatch);
        Batch_Model GetBatchByID(long BatchID);
        List<Batch_Model> GetAllBatch();
        List<BatchVendor_Model> GetAllInActiveBatch();
    }
}
