using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface IDeliveryBoy_Repository
    {

        int Add(DeliveryBoy_Model ObjDeliveryBoy);
        int Register(DeliveryBoy_Model ObjDeliveryBoy);
        int Forgot_Password(DeliveryBoy_Model ObjDeliveryBoy);
        int Update(DeliveryBoy_Model ObjDeliveryBoy);
        int Delete(int DeliveryBoyID);
        DeliveryBoy_Model GetDeliveryBoyID(int UserID);
        List<DeliveryBoy_Model> GetActiveDeliveryBoys();
        List<DeliveryBoy_Model> GetAllDeliveryBoys();
        int DeliveryBoyLogin(DeliveryBoy_Model objDBoy);
        long AddDeviceID(DeliveryBoy_Model obj);
    }
}
