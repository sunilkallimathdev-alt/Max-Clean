using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Banner_Repository : Banner_IRepository
    {
        public int Add(Banner_Model ObjBanner)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_banner.FirstOrDefault(x => x.banner_name == ObjBanner.Banner_Name);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_banner ObjCoun = new t_banner();
                        ObjCoun.banner_name = ObjBanner.Banner_Name;
                        ObjCoun.is_active = ObjBanner.IsActive;
                        ObjCoun.created_by = ObjBanner.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_banner.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.banner_id;
                        
                            ObjCoun.banner_image = API.CommonClasses.SaveBase64File.SaveBase64Image(iRes, "Banner", ObjBanner.Banner_Name, ObjBanner.Banner_Image, ".jpg");
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

        public int Delete(int BannerID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_banner banner = Context.t_banner.SingleOrDefault(x => x.banner_id == BannerID);
                    Context.t_banner.Remove(banner);
                    Context.SaveChanges();
                }
                return iRes = BannerID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Banner_Model> GetActiveBanner()
        {
            List<Banner_Model> ObjBanner = new List<Banner_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjBanner = (from a in Context.t_banner
                                         where a.is_active == true
                                         select new Banner_Model
                                         {
                                             BannerID = a.banner_id,
                                             Banner_Name = a.banner_name,
                                             Banner_Image = a.banner_image,
                                             IsActive = a.is_active,
                                             CreatedBy = a.created_by,
                                             CreatedDate = a.created_date,
                                             UpdateBy = a.update_by,
                                             UpdateDate = a.updated_date
                                         }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Banner_Model> GetAllBanner()
        {
            List<Banner_Model> ObjBanner = new List<Banner_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjBanner = (from a in Context.t_banner
                                         select new Banner_Model
                                         {
                                             BannerID = a.banner_id,
                                             Banner_Name = a.banner_name,
                                             Banner_Image = a.banner_image,
                                             IsActive = a.is_active,
                                             CreatedBy = a.created_by,
                                             CreatedDate = a.created_date,
                                             UpdateBy = a.update_by,
                                             UpdateDate = a.updated_date
                                         }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Banner_Model GetBannerByID(int BannerID)
        {
            Banner_Model ObjBanner = new Banner_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjBanner = (from a in Context.t_banner
                                         where a.banner_id == BannerID
                                         select new Banner_Model
                                         {
                                             BannerID = a.banner_id,
                                             Banner_Name = a.banner_name,
                                             Banner_Image = a.banner_image,
                                             IsActive = a.is_active,
                                             CreatedBy = a.created_by,
                                             CreatedDate = a.created_date,
                                             UpdateBy = a.update_by,
                                             UpdateDate = a.updated_date

                                         }).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Banner_Model ObjBanner)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_banner.FirstOrDefault(x => x.banner_name == ObjBanner.Banner_Name && x.banner_id != ObjBanner.BannerID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_banner.SingleOrDefault(x => x.banner_id == ObjBanner.BannerID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjBanner.Banner_Name))
                                IDExist.banner_name = ObjBanner.Banner_Name;
                            if (!string.IsNullOrEmpty(ObjBanner.Banner_Image))
                                IDExist.banner_image = API.CommonClasses.SaveBase64File.SaveBase64Image(ObjBanner.BannerID, "Banner", ObjBanner.Banner_Name, ObjBanner.Banner_Image, ".jpg");
                            
                            if (ObjBanner.IsActive != null)
                                IDExist.is_active = ObjBanner.IsActive;
                            IDExist.update_by = ObjBanner.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjBanner.BannerID;
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
    }
}