using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Vendor_IRepository
    {
        int Add(Vendor_Model ObjVendor);
        int Update(Vendor_Model ObjVendor);
        int Delete(int VendorID);
        Vendor_Model GetVendorByID(int VendorID);
        List<Vendor_Model> GetActiveVendors();
        List<Vendor_Model> GetAllVendors();
    }
}
