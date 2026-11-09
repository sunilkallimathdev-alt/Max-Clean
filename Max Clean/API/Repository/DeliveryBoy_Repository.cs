using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.CommonClasses;
using System.Text;
using System.Net;
using System.IO;

namespace API.Repository
{
    public class DeliveryBoy_Repository : IDeliveryBoy_Repository
    {
        public int Add(DeliveryBoy_Model ObjDeliveryBoy)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_delivery_boy.FirstOrDefault(x => x.mobile == ObjDeliveryBoy.DeliveryBoyMobileNumber || x.email == ObjDeliveryBoy.DeliveryBoyEmailID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_delivery_boy ObjCoun = new t_delivery_boy();
                        ObjCoun.name = ObjDeliveryBoy.DeliveryBoyName;
                        ObjCoun.mobile = ObjDeliveryBoy.DeliveryBoyMobileNumber;
                        ObjCoun.email = ObjDeliveryBoy.DeliveryBoyEmailID;
                        ObjCoun.address = ObjDeliveryBoy.Address;
                        ObjCoun.vehicle_no = ObjDeliveryBoy.Vehicle_No;
                        ObjCoun.licence_no = ObjDeliveryBoy.Licence_No;
                      
                        ObjCoun.languages_known = ObjDeliveryBoy.Languages;
                        ObjCoun.password = ObjDeliveryBoy.Password;
                        ObjCoun.is_active = ObjDeliveryBoy.IsActive;
                        ObjCoun.created_by = ObjDeliveryBoy.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_delivery_boy.Add(ObjCoun);
                        Context.SaveChanges();
                        ObjCoun.image = API.CommonClasses.SaveBase64File.SaveBase64Image(ObjCoun.id, "DeliveryBox", ObjDeliveryBoy.DeliveryBoyName, ObjDeliveryBoy.Image, ".jpg");

                        t_deliveryboy_incentives obj = new t_deliveryboy_incentives();
                        obj.delivery_boy_id = ObjCoun.id;
                        obj.amount_paid = 0;
                        obj.balance = 0;
                        obj.incentives = 0;
                        Context.t_deliveryboy_incentives.Add(obj);
                        Context.SaveChanges();
                        iRes = ObjCoun.id;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int DeliveryBoyID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_delivery_boy deliverybox = Context.t_delivery_boy.SingleOrDefault(x => x.id == DeliveryBoyID);
                    Context.t_delivery_boy.Remove(deliverybox);
                    Context.SaveChanges();
                }
                return iRes = DeliveryBoyID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DeliveryBoy_Model> GetActiveDeliveryBoys()
        {
            List<DeliveryBoy_Model> ObjDeliveryBoy = new List<DeliveryBoy_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjDeliveryBoy = (from a in Context.t_delivery_boy
                                             where a.is_active == true
                                             select new DeliveryBoy_Model
                                             {
                                                 DeliveryBoyID = a.id,
                                                 DeliveryBoyName = a.name,
                                                 DeliveryBoyEmailID = a.email,
                                                 DeliveryBoyMobileNumber = a.mobile,
                                                 Vehicle_No = a.vehicle_no,
                                                 Licence_No = a.licence_no,
                                                 Languages = a.languages_known,
                                                 Image = a.image,
                                                 Address = a.address,
                                                 Password = a.password,
                                                 IsActive = a.is_active,
                                                 CreatedBy = a.created_by,
                                                 CreatedDate = a.created_date,
                                                 UpdateBy = a.updated_by,
                                                 UpdateDate = a.updated_date
                                             }).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DeliveryBoy_Model> GetAllDeliveryBoys()
        {
            List<DeliveryBoy_Model> ObjDeliveryBoy = new List<DeliveryBoy_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjDeliveryBoy = (from a in Context.t_delivery_boy
                                             select new DeliveryBoy_Model
                                             {
                                                 DeliveryBoyID = a.id,
                                                 DeliveryBoyName = a.name,
                                                 DeliveryBoyEmailID = a.email,
                                                 DeliveryBoyMobileNumber = a.mobile,
                                                 Vehicle_No = a.vehicle_no,
                                                 Licence_No = a.licence_no,
                                                 Languages = a.languages_known,
                                                 Image = a.image,
                                                 Address = a.address,
                                                 Password = a.password,
                                                
                                                 Total_Delivery = a.t_orders.Where(c => c.delivered_by == a.id && c.order_status ==11).Select(c => c.order_id).Count(),
                                                 Open_Delivery = a.t_orders.Where(c => c.delivered_by == a.id && c.order_status > 8 && c.order_status < 11).Select(c => c.order_id).Count(),

                                                 Total_Pickup = a.t_orders.Where(c => c.pickedup_by == a.id && c.order_status >3).Select(c => c.order_id).Count(),
                                                 Open_Pickup = a.t_orders.Where(c => c.pickedup_by == a.id &&  c.order_status < 4).Select(c => c.order_id).Count(),
                                                 IsActive = a.is_active,
                                                 CreatedBy = a.created_by,
                                                 CreatedDate = a.created_date,
                                                 
                                                 UpdateBy = a.updated_by,
                                                 UpdateDate = a.updated_date
                                             }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DeliveryBoy_Model GetDeliveryBoyID(int DeliveryBoyID)
        {
            DeliveryBoy_Model ObjDeliveryBoy = new DeliveryBoy_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjDeliveryBoy = (from a in Context.t_delivery_boy
                                             
                                             where a.id == DeliveryBoyID
                                             select new DeliveryBoy_Model
                                             {
                                                 DeliveryBoyID = a.id,
                                                 DeliveryBoyName = a.name,
                                                 DeliveryBoyEmailID = a.email,
                                                 DeliveryBoyMobileNumber = a.mobile,
                                                 Vehicle_No = a.vehicle_no,
                                                 Licence_No = a.licence_no,
                                                 Image = a.image,
                                                 Address = a.address,
                                                 Languages = a.languages_known,
                                                
                                                 Password = a.password,
                                                 IsActive = a.is_active,
                                                 CreatedBy = a.created_by,
                                                 CreatedDate = a.created_date,
                                                 UpdateBy = a.updated_by,
                                                 UpdateDate = a.updated_date
                                             }).SingleOrDefault();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(DeliveryBoy_Model ObjDeliveryBoy)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_delivery_boy.FirstOrDefault(x => x.mobile == ObjDeliveryBoy.DeliveryBoyMobileNumber && x.id != ObjDeliveryBoy.DeliveryBoyID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_delivery_boy.SingleOrDefault(x => x.id == ObjDeliveryBoy.DeliveryBoyID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjDeliveryBoy.DeliveryBoyName))
                                IDExist.name = ObjDeliveryBoy.DeliveryBoyName;
                            if (!string.IsNullOrEmpty(ObjDeliveryBoy.DeliveryBoyEmailID))
                                IDExist.email = ObjDeliveryBoy.DeliveryBoyEmailID;
                            if (!string.IsNullOrEmpty(ObjDeliveryBoy.DeliveryBoyMobileNumber))
                                IDExist.mobile = ObjDeliveryBoy.DeliveryBoyMobileNumber;
                            if (!string.IsNullOrEmpty(ObjDeliveryBoy.Vehicle_No))
                                IDExist.vehicle_no = ObjDeliveryBoy.Vehicle_No;
                            if (!string.IsNullOrEmpty(ObjDeliveryBoy.Licence_No))
                                IDExist.licence_no = ObjDeliveryBoy.Licence_No;
                            if (ObjDeliveryBoy.Password != null)
                                IDExist.password = ObjDeliveryBoy.Password;
                            if (ObjDeliveryBoy.Image != null)
                                IDExist.image = API.CommonClasses.SaveBase64File.SaveBase64Image(iRes, "DeliveryBox", ObjDeliveryBoy.DeliveryBoyName, ObjDeliveryBoy.Image, ".jpg");
                            if (ObjDeliveryBoy.Address != null)
                                IDExist.address = ObjDeliveryBoy.Address;
                            if (ObjDeliveryBoy.Languages != null)
                                IDExist.languages_known = ObjDeliveryBoy.Languages;
                            if (ObjDeliveryBoy.IsActive != null)
                                IDExist.is_active = ObjDeliveryBoy.IsActive;
                            IDExist.updated_by = ObjDeliveryBoy.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjDeliveryBoy.DeliveryBoyID;
                        }
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeliveryBoyLogin(DeliveryBoy_Model objDBoy)
        {
            int iRes = 0;
            DeliveryBoy_Model ObjDeliveryBoy = new DeliveryBoy_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var ObjDelivery = (from a in Context.t_delivery_boy
                                             where a.mobile == objDBoy.DeliveryBoyMobileNumber
                                             && a.password == objDBoy.Password && a.is_active ==true
                                             select new DeliveryBoy_Model
                                             {
                                                 DeliveryBoyID = a.id,
                                                 DeliveryBoyName = a.name,
                                                 DeliveryBoyEmailID = a.email,
                                                 DeliveryBoyMobileNumber = a.mobile,
                                                 Vehicle_No = a.vehicle_no,
                                                 Licence_No = a.licence_no,
                                                 Image = a.image,
                                                 Address = a.address,
                                                 Languages = a.languages_known,
                                                 Password = a.password,
                                                 IsActive = a.is_active,
                                                 CreatedBy = a.created_by,
                                                 CreatedDate = a.created_date,
                                                 UpdateBy = a.updated_by,
                                                 UpdateDate = a.updated_date
                                             }).SingleOrDefault();
                    iRes = ObjDelivery.DeliveryBoyID;
                    return iRes;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Register(DeliveryBoy_Model ObjDeliveryBoy)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_delivery_boy.FirstOrDefault(x => x.mobile == ObjDeliveryBoy.DeliveryBoyMobileNumber);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_delivery_boy Obj = new t_delivery_boy();
                        Obj.name = ObjDeliveryBoy.DeliveryBoyName;
                        Obj.mobile = ObjDeliveryBoy.DeliveryBoyMobileNumber;
                        Obj.email = ObjDeliveryBoy.DeliveryBoyEmailID;
                        Obj.address = ObjDeliveryBoy.Address;
                        Obj.vehicle_no = ObjDeliveryBoy.Vehicle_No;
                        Obj.licence_no = ObjDeliveryBoy.Licence_No;

                        Obj.languages_known = ObjDeliveryBoy.Languages;
                        Obj.password = ObjDeliveryBoy.Password;
                        Obj.is_active = false;
                        Obj.created_by = ObjDeliveryBoy.CreatedBy;
                        Obj.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_delivery_boy.Add(Obj);
                        Context.SaveChanges();
                       // Obj.image = API.CommonClasses.SaveBase64File.SaveBase64Image(Obj.id, "DeliveryBox", ObjDeliveryBoy.DeliveryBoyName, ObjDeliveryBoy.Image, ".jpg");

                        t_deliveryboy_incentives objinc = new t_deliveryboy_incentives();
                        objinc.delivery_boy_id = Obj.id;
                        objinc.amount_paid = 0;
                        objinc.balance = 0;
                        objinc.incentives = 0;
                        Context.t_deliveryboy_incentives.Add(objinc);

                        t_deliveryboy_deviceid objdvc = new t_deliveryboy_deviceid();
                        objdvc.delivery_boy_id = Obj.id;
                        objdvc.deviceId = ObjDeliveryBoy.DeliveryBoy_DeviceID;
                        Context.t_deliveryboy_deviceid.Add(objdvc);
                        Context.SaveChanges();
                        iRes = Obj.id;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Forgot_Password(DeliveryBoy_Model ObjDeliveryBoy)
        {
            int Res = 0;
            DeliveryBoy_Model Obj = new DeliveryBoy_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var IdExist = Context.t_delivery_boy.FirstOrDefault(c => c.mobile == ObjDeliveryBoy.DeliveryBoyMobileNumber);

                    if (IdExist == null)
                    {
                        Res = -4;
                    }
                    else
                    {
                        var SMSbody = "MaxClean DeliveryBoy Password Recovery" + Environment.NewLine + "Dear " + IdExist.name +","+ Environment.NewLine + "User Name: " + IdExist.mobile + Environment.NewLine + "PassWord: " + IdExist.password + Environment.NewLine;


                        StringBuilder baseurl = new StringBuilder();
                        baseurl.Append(EmailSMSCredentials.SMS_URL);
                        baseurl.Append("sender=" + EmailSMSCredentials.SMS_SENDER_NAME);
                        baseurl.Append("&route=" + EmailSMSCredentials.Route);
                        baseurl.Append("&mobiles=" + IdExist.mobile);
                        baseurl.Append("&authkey=" + EmailSMSCredentials.SMS_OauthKey);
                        baseurl.Append("&country=" + EmailSMSCredentials.SMS_Country);
                        baseurl.Append("&message=" + SMSbody);

                        WebClient client = new WebClient();
                        Stream data = client.OpenRead(baseurl.ToString());
                        StreamReader reader = new StreamReader(data);
                        var Status = reader.ReadToEnd();
                        data.Close();
                        reader.Close();
                        Res = IdExist.id;
                    }
                    return Res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        public long AddDeviceID(DeliveryBoy_Model obj)
        {

            long IRes = 0;
            try
            {
                if (obj.DeliveryBoy_DeviceID != null)
                {
                    using (maxcleanEntities context = new maxcleanEntities())
                    {
                        t_deliveryboy_deviceid objdevice = new t_deliveryboy_deviceid();
                        if(obj.DeliveryBoyID !=0 )
                        objdevice.delivery_boy_id = obj.DeliveryBoyID;
                        objdevice.deviceId = obj.DeliveryBoy_DeviceID;
                        context.t_deliveryboy_deviceid.Add(objdevice);
                        context.SaveChanges();
                        IRes = objdevice.id;

                    }

                }
                return IRes;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}