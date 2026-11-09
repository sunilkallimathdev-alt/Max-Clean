using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Store_IRepository
    {
        int Add(Store_Model ObjStore);
        int Update(Store_Model ObjStore);
        int Delete(int StoreID);
        Store_Model GetStoreByID(int StoreID);
        List<Store_Model> GetActiveStores();
        List<Store_Model> GetAllStores();
        List<Store_Model> GetStoresByLocation(GeoLocation_Model data);
    }
}
