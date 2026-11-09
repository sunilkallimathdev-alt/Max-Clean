using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Gallery_Repository : Gallery_IRepository
    {
        public int Add(Gallery_Model ObjGallery)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_gallery.FirstOrDefault(x => x.title == ObjGallery.Title);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_gallery ObjCoun = new t_gallery();
                        ObjCoun.title = ObjGallery.Title;
                        ObjCoun.is_image = ObjGallery.Is_Image;


                        if (ObjGallery.Is_Image == false)
                            ObjCoun.url = ObjGallery.Url;
                        ObjCoun.is_active = ObjGallery.IsActive;
                        ObjCoun.created_by = ObjGallery.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_gallery.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.gallery_id;
                        if (ObjGallery.Is_Image == true)
                            ObjCoun.url = API.CommonClasses.SaveBase64File.SaveBase64Image(iRes, "Url", ObjGallery.Title, ObjGallery.Url, ".jpg");
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

        public int Delete(int GalleryID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_gallery gallery = Context.t_gallery.SingleOrDefault(x => x.gallery_id == GalleryID);
                    Context.t_gallery.Remove(gallery);
                    Context.SaveChanges();
                }
                return iRes = GalleryID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Gallery_Model ObjGallery)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_gallery.FirstOrDefault(x => x.title == ObjGallery.Title && x.gallery_id != ObjGallery.GalleryID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_gallery.SingleOrDefault(x => x.gallery_id == ObjGallery.GalleryID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjGallery.Title))
                                IDExist.title = ObjGallery.Title;

                            if (ObjGallery.Is_Image != null)
                                IDExist.is_image = ObjGallery.Is_Image;

                            if (ObjGallery.Is_Image == false)
                                if (!string.IsNullOrEmpty(ObjGallery.Url))
                                    IDExist.url = ObjGallery.Url;

                            if (ObjGallery.Is_Image == true)
                                IDExist.url = API.CommonClasses.SaveBase64File.SaveBase64Image(ObjGallery.GalleryID, "Url", ObjGallery.Title, ObjGallery.Url, ".jpg");
                                       Context.SaveChanges();

                            //if (ObjGallery.Url.Contains("http"))
                            //    if (ObjGallery.Url != null)

                            //        if (!ObjGallery.Url.Contains("http"))
                            //        {
                            //            IDExist.url = API.CommonClasses.SaveBase64File.SaveBase64Image(ObjGallery.GalleryID, "Url", ObjGallery.Title, ObjGallery.Url, ".jpg");
                            //            Context.SaveChanges();
                            //        }

                            //if (ObjGallery.Is_Image != null)
                            //    IDExist.is_image = ObjGallery.Is_Image;
                            //if (ObjGallery.Url != null)
                            //    IDExist.url = ObjGallery.Url;
                            if (ObjGallery.IsActive != null)
                                IDExist.is_active = ObjGallery.IsActive;
                            IDExist.update_by = ObjGallery.UpdateBy;
                            IDExist.update_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjGallery.GalleryID;
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

        public List<Gallery_Model> GetActiveGallery()
        {
            List<Gallery_Model> ObjGallery = new List<Gallery_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjGallery = (from a in Context.t_gallery
                                         where a.is_active == true
                                         select new Gallery_Model
                                         {
                                             GalleryID = a.gallery_id,
                                             Title = a.title,
                                             Is_Image = a.is_image,
                                             Url = a.url,

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

        public List<Gallery_Model> GetAllGallery()
        {
            List<Gallery_Model> ObjGallery = new List<Gallery_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjGallery = (from a in Context.t_gallery
                                         select new Gallery_Model
                                         {
                                             GalleryID = a.gallery_id,
                                             Title = a.title,
                                             Is_Image = a.is_image,
                                             Url = a.url,

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

        public Gallery_Model GetGalleryByID(int GalleryID)
        {
            Gallery_Model ObjGallery = new Gallery_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjGallery = (from a in Context.t_gallery
                                         where a.gallery_id == GalleryID
                                         select new Gallery_Model
                                         {
                                             GalleryID = a.gallery_id,
                                             Title = a.title,
                                             Is_Image = a.is_image,
                                             Url = a.url,

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