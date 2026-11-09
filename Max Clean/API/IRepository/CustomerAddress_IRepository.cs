using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface CustomerAddress_IRepository
    {
        int Add(CustomerAddress_Model ObjCustomerAddress);
        int Update(CustomerAddress_Model ObjCustomerAddress);
        int Delete(int CUstomerAddressID);
        CustomerAddress_Model GetCustomerAddressByID(int CUstomerAddressID);
        List<CustomerAddress_Model> GetCustomerAddressByCustomerID(int CustomerID);
        List<CustomerAddress_Model> GetActiveCustomerAddress();
        List<CustomerAddress_Model> GetAllCustomerAddress();
    }
}
