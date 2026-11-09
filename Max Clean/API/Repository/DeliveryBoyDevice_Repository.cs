using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class DeliveryBoyDevice_Repository : DeliveryBoyDevice_IRepository
    {
        public int Add(DeliveryBoyDevice_Model ObjDeliveryBoyDevice)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {

                    t_deliveryboy_deviceid Obj = new t_deliveryboy_deviceid();
                    Obj.delivery_boy_id = ObjDeliveryBoyDevice.DeliveryBoyID;
                    Obj.deviceId = ObjDeliveryBoyDevice.DeviceID;
                    Context.t_deliveryboy_deviceid.Add(Obj);
                    Context.SaveChanges();
                    iRes = Obj.id;
                  

                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DeliveryBoyDevice_Model> GetAllDeliveryBoyDevice()
        {
            List<DeliveryBoyDevice_Model> ObjDeliveryBoyDevice = new List<DeliveryBoyDevice_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjDeliveryBoyDevice = (from a in Context.t_deliveryboy_deviceid
                                                   select new DeliveryBoyDevice_Model
                                                   {
                                                       ID = a.id,
                                                       DeliveryBoyID = a.delivery_boy_id,
                                                       DeliveryBoyName = a.t_delivery_boy.name,
                                                       DeviceID = a.deviceId

                                                   }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DeliveryBoyDevice_Model GetDeliveryBoyDeviceByDeliveryBoyID(int DeliveryBoyID)
        {
            DeliveryBoyDevice_Model ObjDeliveryBoyDevice = new DeliveryBoyDevice_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjDeliveryBoyDevice = (from a in Context.t_deliveryboy_deviceid
                                                   where a.id == DeliveryBoyID
                                                   select new DeliveryBoyDevice_Model
                                                   {
                                                       ID = a.id,
                                                       DeliveryBoyID = a.delivery_boy_id,
                                                       DeliveryBoyName = a.t_delivery_boy.name,
                                                       DeviceID = a.deviceId

                                                   }).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}