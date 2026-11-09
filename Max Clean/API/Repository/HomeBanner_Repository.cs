using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class HomeBanner_Repository : HomeBanner_IRepository
    {
        public int Add(HomeBanner_Model ObjHome)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_home_banner.FirstOrDefault(x => x.bannertitle == ObjHome.BannerTitle);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_home_banner ObjCoun = new t_home_banner();
                        ObjCoun.bannertitle = ObjHome.BannerTitle;
                        ObjCoun.displayorder = ObjHome.DisplayOrder;
                        ObjCoun.bannertext1 = ObjHome.BannerText1;
                        ObjHome.BannerText2 = ObjHome.BannerText2;
                        ObjHome.BannnerText3 = ObjCoun.bannertext3;
                        ObjCoun.isactive = ObjHome.IsActive;
                        ObjCoun.created_by = ObjHome.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_home_banner.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.banner_id;
                        ObjCoun.bannerurl = API.CommonClasses.SaveBase64File.SaveBase64Image(iRes, "BannerUrl", ObjHome.BannerTitle, ObjHome.BannerUrl, ".jpg");
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
        public int Update(HomeBanner_Model ObjHome)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_home_banner.FirstOrDefault(x => x.bannertitle == ObjHome.BannerTitle && x.banner_id != ObjHome.BannerID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_home_banner.SingleOrDefault(x => x.banner_id == ObjHome.BannerID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjHome.BannerTitle))
                                IDExist.bannertitle = ObjHome.BannerTitle;
                            if (!string.IsNullOrEmpty(ObjHome.BannerUrl))
                                IDExist.bannerurl = API.CommonClasses.SaveBase64File.SaveBase64Image(ObjHome.BannerID, "BannerUrl", ObjHome.BannerTitle, ObjHome.BannerUrl, ".jpg");

                            if (!string.IsNullOrEmpty(ObjHome.BannerText1))
                                IDExist.bannertext1 = ObjHome.BannerText1;
                            if (!string.IsNullOrEmpty(ObjHome.BannerText2))
                                IDExist.bannertext2 = ObjHome.BannerText2;
                            if (!string.IsNullOrEmpty(ObjHome.BannnerText3))
                                IDExist.bannertext3 = ObjHome.BannnerText3;

                            if (ObjHome.DisplayOrder != null)
                                IDExist.displayorder = ObjHome.DisplayOrder;
                            if (ObjHome.IsActive != null)
                                IDExist.isactive = ObjHome.IsActive;
                            IDExist.updated_by = ObjHome.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjHome.BannerID;
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
        public int Delete(int HomeBannerID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_home_banner category = Context.t_home_banner.SingleOrDefault(x => x.banner_id == HomeBannerID);
                    Context.t_home_banner.Remove(category);
                    Context.SaveChanges();
                }
                return iRes = HomeBannerID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<HomeBanner_Model> GetActiveHomeBanner()
        {
            List<HomeBanner_Model> ObjCategories = new List<HomeBanner_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCategories = (from a in Context.t_home_banner
                                            where a.isactive == true
                                            select new HomeBanner_Model
                                            {
                                                BannerID = a.banner_id,
                                                BannerTitle = a.bannertitle,
                                                BannerUrl = a.bannerurl,
                                                DisplayOrder = a.displayorder,
                                                BannerText1 = a.bannertext1,
                                                BannerText2 = a.bannertext2,
                                                BannnerText3 = a.bannertext3,
                                                IsActive = a.isactive,
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


        public List<HomeBanner_Model> GetAllHomeBanner()
        {

            List<HomeBanner_Model> ObjCategories = new List<HomeBanner_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCategories = (from a in Context.t_home_banner
                                            select new HomeBanner_Model
                                            {
                                                BannerID = a.banner_id,
                                                BannerTitle = a.bannertitle,
                                                BannerUrl = a.bannerurl,
                                                DisplayOrder = a.displayorder,
                                                BannerText1 = a.bannertext1,
                                                BannerText2 = a.bannertext2,
                                                BannnerText3 = a.bannertext3,
                                                IsActive = a.isactive,
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

        public HomeBanner_Model GetHomeBannerByID(int HomeBannerID)
        {
            HomeBanner_Model ObjCategories = new HomeBanner_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCategories = (from a in Context.t_home_banner
                                            where a.banner_id == HomeBannerID
                                            select new HomeBanner_Model
                                            {
                                                BannerID = a.banner_id,
                                                BannerTitle = a.bannertitle,
                                                BannerUrl = a.bannerurl,
                                                DisplayOrder = a.displayorder,
                                                BannerText1 = a.bannertext1,
                                                BannerText2 = a.bannertext2,
                                                BannnerText3 = a.bannertext3,
                                                IsActive = a.isactive,
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

    }
}