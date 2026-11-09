
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;
using API.DataAccess;
using Newtonsoft.Json;



namespace API.Repository
{
    public class Category_Repository : Category_IRepository
    {
        public int Add(Category_Model ObjCategory)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_category.FirstOrDefault(x => x.category_name == ObjCategory.CategoryName);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_category ObjCoun = new t_category();
                        ObjCoun.category_name = ObjCategory.CategoryName;
                        ObjCoun.service_id = ObjCategory.ServiceID;
                       
                        ObjCoun.description = ObjCategory.Description;
                        ObjCoun.display_order = ObjCategory.DisplayOrder;
                        ObjCoun.is_active = ObjCategory.IsActive;
                        ObjCoun.created_by = ObjCategory.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_category.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.category_id;
                        ObjCoun.category_icon = API.CommonClasses.SaveBase64File.SaveBase64Image(iRes, "CategoryIcon", ObjCategory.CategoryName, ObjCategory.CategoryIcon, ".jpg");
                        ObjCoun.category_banner = API.CommonClasses.SaveBase64File.SaveBase64Image(iRes, "CategoryBanner", ObjCategory.CategoryName, ObjCategory.CategoryBanner, ".jpg");
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

        public int Update(Category_Model ObjCategory)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_category.FirstOrDefault(x => x.category_name == ObjCategory.CategoryName && x.category_id != ObjCategory.CategoryId);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_category.SingleOrDefault(x => x.category_id == ObjCategory.CategoryId);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjCategory.CategoryName))
                                IDExist.category_name = ObjCategory.CategoryName;
                            if (ObjCategory.ServiceID != null)
                                IDExist.service_id = ObjCategory.ServiceID;
                            if (!string.IsNullOrEmpty(ObjCategory.CategoryIcon))
                                IDExist.category_icon = API.CommonClasses.SaveBase64File.SaveBase64Image(ObjCategory.CategoryId, "CategoryIcon", ObjCategory.CategoryName, ObjCategory.CategoryIcon, ".jpg");
                            if (!string.IsNullOrEmpty(ObjCategory.CategoryBanner))
                                IDExist.category_banner = API.CommonClasses.SaveBase64File.SaveBase64Image(ObjCategory.CategoryId, "CategoryBanner", ObjCategory.CategoryName, ObjCategory.CategoryBanner, ".jpg");
                            if (!string.IsNullOrEmpty(ObjCategory.Description))
                                IDExist.description = ObjCategory.Description;
                            if (ObjCategory.DisplayOrder != null)
                                IDExist.display_order = ObjCategory.DisplayOrder;
                            if (ObjCategory.IsActive != null)
                                IDExist.is_active = ObjCategory.IsActive;
                            IDExist.updated_by = ObjCategory.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjCategory.CategoryId;
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

        public int Delete(int CategoryId)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_category category = Context.t_category.SingleOrDefault(x => x.category_id == CategoryId);
                    Context.t_category.Remove(category);
                    Context.SaveChanges();
                }
                return iRes = CategoryId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Category_Model> GetActiveCategory()
        {
            List<Category_Model> ObjCategories = new List<Category_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCategories = (from a in Context.t_category
                                            where a.is_active == true
                                            select new Category_Model
                                            {
                                                CategoryId = a.category_id,
                                                //ServiceID = a.service_id,
                                                //ServiceName = a.t_service.service_name,
                                                CategoryName = a.category_name,
                                                CategoryBanner = a.category_banner,
                                                DisplayOrder = a.display_order,
                                                Description = a.description,
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

        public List<Category_Model> GetAllCategory()
        {

            List<Category_Model> ObjCategories = new List<Category_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCategories = (from a in Context.t_category
                                            select new Category_Model
                                            {
                                                CategoryId = a.category_id,
                                                //ServiceID = a.service_id,
                                                //ServiceName = a.t_service.service_name,
                                                CategoryName = a.category_name,
                                                CategoryBanner = a.category_banner,
                                                CategoryIcon = a.category_icon,
                                                DisplayOrder = a.display_order,
                                                Description = a.description,
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

        public Category_Model GetCategoryByID(int CategoryId)
        {
            Category_Model ObjCategories = new Category_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCategories = (from a in Context.t_category
                                            where a.category_id == CategoryId
                                            select new Category_Model
                                            {
                                                CategoryId = a.category_id,
                                                //ServiceID = a.service_id,
                                                //ServiceName = a.t_service.service_name,
                                                CategoryName = a.category_name,
                                                CategoryBanner = a.category_banner,
                                                CategoryIcon = a.category_icon,
                                                DisplayOrder = a.display_order,
                                                Description = a.description,
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

        public List<Shop_Model> GetCategoryByServiceID(int ServiceID)
        {
            List<Shop_Model> ObjCategories = new List<Shop_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Res = (from a in Context.t_service_price
                               where a.service_id == ServiceID 
                               where a.t_product.t_category.is_active == true
                               select new
                               {
                                   CategoryID = a.t_product.category_id,
                                   CategoryName = a.t_product.t_category.category_name,
                                   CategoryBanner = a.t_product.t_category.category_banner,
                                   CategoryIcon = a.t_product.t_category.category_icon,

                                   ProductId = a.product_id,
                                   ProductName = a.t_product.product_name,
                                   Price = a.price,
                                   ProductImage = a.t_product.product_image

                               }).ToList();

                    var abc = Res.GroupBy(x => x.CategoryID).Select(x => x.FirstOrDefault());
                    var CatList = abc.Select(x => new Shop_Model { CategoryID = x.CategoryID, CategoryName = x.CategoryName, CategoryIcon = x.CategoryIcon, CategoryBanner = x.CategoryBanner }).ToList();
                    foreach(var cat in CatList)
                    {
                        cat.ProductList = Res.Where(x => x.CategoryID == cat.CategoryID).Select(x => new Shop_Product { ProductId = x.ProductId, ProductImage = x.ProductImage, Price = x.Price, ProductName = x.ProductName }).ToList();
                    }
                    return ObjCategories = CatList;



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<mCategory_Model> GetCategoriesByServiceID(int ServiceID)
        {
            List<mCategory_Model> ObjCategories = new List<mCategory_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    ObjCategories = (from a in Context.t_category
                               where a.service_id == ServiceID
                               where a.is_active == true
                               select new mCategory_Model
                               {
                                   CategoryID = a.category_id,
                                   CategoryName = a.category_name,
                                   CategoryBanner = a.category_banner,
                                   CategoryIcon = a.category_icon
                               }).ToList();



                    return ObjCategories;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Base_Model GetCategoryList(string length, string start, string sortColumn, string sortColumnDir, string draw,string search)
        {
            
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            List<Category_Model> ObjCategories = new List<Category_Model>();
            using (API.DataAccess.maxcleanEntities Context = new API.DataAccess.maxcleanEntities())
            {
                // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
                Context.Configuration.LazyLoadingEnabled = false;

                ObjCategories = (from a in Context.t_category
                                 select new Category_Model
                                 {
                                     CategoryId = a.category_id,
                                     CategoryName = a.category_name,
                                     CategoryBanner = a.category_banner,
                                     CategoryIcon = a.category_icon,
                                     DisplayOrder = a.display_order,
                                     Description = a.description,
                                     IsActive = a.is_active,
                                     CreatedBy = a.created_by,
                                     CreatedDate = a.created_date,
                                     UpdateBy = a.updated_by,
                                     UpdateDate = a.updated_date
                                 }).ToList();

                // Verification.   
                if (!string.IsNullOrEmpty(search) &&
                    !string.IsNullOrWhiteSpace(search) && (search != "$0$"))
                {
                    // Apply search   
                    ObjCategories = ObjCategories.Where(p => p.CategoryName.ToString().ToLower().Contains(search.ToLower()) ||
                        p.DisplayOrder.ToString().Contains(search.ToLower()) 
                     ).ToList();
                }


                ////SORT
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    ObjCategories = ObjCategories.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                }

                recordsTotal = ObjCategories.Count();
                var data = ObjCategories.Skip(skip).Take(pageSize).ToList();
                Base_Model obj = new Base_Model();
                obj.draw = draw;
                obj.recordsTotal = recordsTotal;
                obj.data = data;
              
                return obj;
            }
        }
    }
}