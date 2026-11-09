using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Pincode_IRepository
    {
        int Add(Pincode_Model ObjPincode);
        int Update(Pincode_Model ObjPincode);
        int Delete(int PincodeID);
        Pincode_Model GetPincodeByID(int PincodeID);
        List<Pincode_Model> GetActivePincode();
        List<Pincode_Model> GetAllPincode();
        int GetStoreByPincode(int Pincode);
    }
}
