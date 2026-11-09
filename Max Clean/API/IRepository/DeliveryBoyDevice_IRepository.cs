using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface DeliveryBoyDevice_IRepository
    {
        int Add(DeliveryBoyDevice_Model ObjDeliveryBoyDevice);
        DeliveryBoyDevice_Model GetDeliveryBoyDeviceByDeliveryBoyID(int DeliveryBoyID);
        List<DeliveryBoyDevice_Model> GetAllDeliveryBoyDevice();
    }
}
