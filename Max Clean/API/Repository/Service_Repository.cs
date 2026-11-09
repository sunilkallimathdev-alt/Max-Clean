using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Service_Repository : Service_IRepository
    {
        public int Add(Service_Model ObjService)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_service.FirstOrDefault(x => x.service_name == ObjService.ServiceName);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_service ObjCoun = new t_service();
                        ObjCoun.service_name = ObjService.ServiceName;
                        ObjCoun.display_order = ObjService.DisplayOrder;
                        ObjCoun.description = ObjService.Description;
                        ObjCoun.is_active = ObjService.IsActive;
                        ObjCoun.created_by = ObjService.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_service.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.service_id;
                        ObjCoun.image = API.CommonClasses.SaveBase64File.SaveBase64Image(iRes, "Image", ObjService.ServiceName, ObjService.Image, ".jpg");
                       
                        Context.SaveChanges();
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int ServiceID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_service service = Context.t_service.SingleOrDefault(x => x.service_id == ServiceID);
                    Context.t_service.Remove(service);
                    Context.SaveChanges();
                }
                return iRes = ServiceID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Service_Model ObjService)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_service.FirstOrDefault(x => x.service_name == ObjService.ServiceName && x.service_id != ObjService.ServiceID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_service.SingleOrDefault(x => x.service_id == ObjService.ServiceID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjService.ServiceName))
                                IDExist.service_name = ObjService.ServiceName;
                            if (!string.IsNullOrEmpty(ObjService.Image))
                                IDExist.image = API.CommonClasses.SaveBase64File.SaveBase64Image(ObjService.ServiceID, "Image", ObjService.ServiceName, ObjService.Image, ".jpg");
                            if (ObjService.DisplayOrder != null)
                                IDExist.display_order = ObjService.DisplayOrder;
                            if (!string.IsNullOrEmpty(ObjService.Description))
                                IDExist.description = ObjService.Description;

                            if (ObjService.IsActive != null)
                                IDExist.is_active = ObjService.IsActive;
                            IDExist.update_by = ObjService.UpdateBy;
                            IDExist.update_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjService.ServiceID;
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

        public List<Service_Model> GetActiveService()
        {
            List<Service_Model> ObjService = new List<Service_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjService = (from a in Context.t_service
                                            where a.is_active == true
                                            select new Service_Model
                                            {
                                                ServiceID = a.service_id,
                                                ServiceName = a.service_name,
                                                Image = a.image,
                                                DisplayOrder = a.display_order,
                                                Description = a.description,
                                                IsActive = a.is_active,
                                                CreatedBy = a.created_by,
                                                CreatedDate = a.created_date,
                                                UpdateBy = a.update_by,
                                                UpdateDate = a.update_date
                                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Service_Model> GetAllService()
        {
            List<Service_Model> ObjService = new List<Service_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjService = (from a in Context.t_service
                                            select new Service_Model
                                            {
                                                ServiceID = a.service_id,
                                                ServiceName = a.service_name,
                                                Image = a.image,
                                                DisplayOrder = a.display_order,
                                                Description = a.description,
                                                IsActive = a.is_active,
                                                CreatedBy = a.created_by,
                                                CreatedDate = a.created_date,
                                                UpdateBy = a.update_by,
                                                UpdateDate = a.update_date
                                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
       

        public Service_Model GetServiceByID(int ServiceID)
        {
            Service_Model ObjService = new Service_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjService = (from a in Context.t_service
                                            where a.service_id == ServiceID
                                            select new Service_Model
                                            {
                                                ServiceID = a.service_id,
                                                ServiceName = a.service_name,
                                                Image = a.image,
                                                DisplayOrder = a.display_order,
                                                Description = a.description,
                                                IsActive = a.is_active,
                                                CreatedBy = a.created_by,
                                                CreatedDate = a.created_date,
                                                UpdateBy = a.update_by,
                                                UpdateDate = a.update_date
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