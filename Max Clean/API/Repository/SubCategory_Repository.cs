using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class SubCategory_Repository : SubCategory_IRepository
    {
        public int Add(SubCategory_Model ObjSubCategory)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_subcategory.FirstOrDefault(x => x.category_id == ObjSubCategory.CategoryId && x.subcategory_name == ObjSubCategory.SubCategoryName);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_subcategory ObjCoun = new t_subcategory();
                        ObjCoun.subcategory_name = ObjSubCategory.SubCategoryName;
                        ObjCoun.category_id = ObjSubCategory.CategoryId;
                        ObjCoun.display_order = ObjSubCategory.SubCategoryDisplayOrder;
                        ObjCoun.is_active = ObjSubCategory.IsActive;
                        ObjCoun.created_by = ObjSubCategory.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_subcategory.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.subcategory_id;
                        ObjCoun.subcategory_icon = API.CommonClasses.SaveBase64File.SaveBase64Image(iRes, "SubCategoryIcon", ObjSubCategory.SubCategoryName, ObjSubCategory.SubCategoryIcon, ".jpg");
                        ObjCoun.subcategory_banner = API.CommonClasses.SaveBase64File.SaveBase64Image(iRes, "SubCategoryBanner", ObjSubCategory.SubCategoryName, ObjSubCategory.SubCategoryBanner, ".jpg");
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
        public int Update(SubCategory_Model ObjSubCategory)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_subcategory.FirstOrDefault(x => x.category_id == ObjSubCategory.CategoryId && x.subcategory_name == ObjSubCategory.SubCategoryName && x.subcategory_id != ObjSubCategory.SubCategoryID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_subcategory.SingleOrDefault(x => x.subcategory_id == ObjSubCategory.SubCategoryID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjSubCategory.SubCategoryName))
                                IDExist.subcategory_name = ObjSubCategory.SubCategoryName;
                            if (!string.IsNullOrEmpty(ObjSubCategory.SubCategoryName))
                                IDExist.subcategory_name = ObjSubCategory.SubCategoryName;
                            if (ObjSubCategory.CategoryId != null)
                                IDExist.category_id = ObjSubCategory.CategoryId;
                            if (!string.IsNullOrEmpty(ObjSubCategory.SubCategoryIcon))
                                IDExist.subcategory_icon = API.CommonClasses.SaveBase64File.SaveBase64Image(ObjSubCategory.SubCategoryID, "SubCategoryIcon", ObjSubCategory.SubCategoryName, ObjSubCategory.SubCategoryIcon, ".jpg");
                            if (!string.IsNullOrEmpty(ObjSubCategory.SubCategoryBanner))
                                IDExist.subcategory_banner = API.CommonClasses.SaveBase64File.SaveBase64Image(ObjSubCategory.SubCategoryID, "SubCategoryBanner", ObjSubCategory.SubCategoryName, ObjSubCategory.SubCategoryBanner, ".jpg");
                            if (ObjSubCategory.SubCategoryDisplayOrder != null)
                                IDExist.display_order = ObjSubCategory.SubCategoryDisplayOrder;
                            if (ObjSubCategory.IsActive != null)
                                IDExist.is_active = ObjSubCategory.IsActive;
                            IDExist.updated_by = ObjSubCategory.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjSubCategory.SubCategoryID;
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

        public int Delete(int SubCategoryID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_subcategory subcategory = Context.t_subcategory.SingleOrDefault(x => x.subcategory_id == SubCategoryID);
                    Context.t_subcategory.Remove(subcategory);
                    Context.SaveChanges();
                }
                return iRes = SubCategoryID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SubCategory_Model> GetSubCategoryByCategoryID(int CategoryID)
        {
            List<SubCategory_Model> ObjSubCategories = new List<SubCategory_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjSubCategories = (from a in Context.t_subcategory
                                               where a.t_category.category_id == CategoryID
                                               select new SubCategory_Model
                                               {
                                                   SubCategoryID = a.subcategory_id,
                                                   SubCategoryName = a.subcategory_name,
                                                   CategoryId = a.category_id,
                                                   CategoryName = a.t_category.category_name,
                                                   SubCategoryIcon=a.subcategory_icon,
                                                   SubCategoryBanner = a.subcategory_banner,
                                                   SubCategoryDisplayOrder = a.display_order,
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

        public List<SubCategory_Model> GetActiveSubCategory()
        {
            List<SubCategory_Model> ObjSubCategories = new List<SubCategory_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjSubCategories = (from a in Context.t_subcategory

                                               select new SubCategory_Model
                                               {
                                                   SubCategoryID = a.subcategory_id,
                                                   SubCategoryName = a.subcategory_name,
                                                   CategoryId = a.category_id,
                                                   CategoryName = a.t_category.category_name,
                                                   SubCategoryIcon = a.subcategory_icon,
                                                   SubCategoryBanner = a.subcategory_banner,
                                                   SubCategoryDisplayOrder = a.display_order,
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

        public List<SubCategory_Model> GetAllSubCategory()
        {

            List<SubCategory_Model> ObjCategories = new List<SubCategory_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCategories = (from a in Context.t_subcategory
                                            select new SubCategory_Model
                                            {
                                                SubCategoryID = a.subcategory_id,
                                                CategoryId = a.category_id,
                                                CategoryName = a.t_category.category_name,
                                                SubCategoryName = a.subcategory_name,
                                                SubCategoryBanner = a.subcategory_banner,
                                                SubCategoryIcon = a.subcategory_icon,
                                                SubCategoryDisplayOrder = a.display_order,
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

        public SubCategory_Model GetSubCategoryByID(int SubCategoryID)
        {
            SubCategory_Model ObjsubCategories = new SubCategory_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjsubCategories = (from a in Context.t_subcategory
                                               where a.subcategory_id == SubCategoryID
                                               select new SubCategory_Model
                                               {
                                                   SubCategoryID = a.subcategory_id,
                                                   CategoryId = a.category_id,
                                                   CategoryName = a.t_category.category_name,
                                                   SubCategoryName = a.subcategory_name,
                                                   SubCategoryBanner = a.subcategory_banner,
                                                   SubCategoryIcon = a.subcategory_icon,
                                                   SubCategoryDisplayOrder = a.display_order,
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

    }
}