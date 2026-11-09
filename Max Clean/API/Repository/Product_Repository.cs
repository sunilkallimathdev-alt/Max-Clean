using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.DataAccess;
using API.IRepository;
using API.Models;

namespace API.Repository
{
    public class Product_Repository : Product_IRepository
    {
        public int Add(Product_Model ObjProduct)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_product.FirstOrDefault(x => x.category_id == ObjProduct.CategoryId && x.product_name == ObjProduct.ProductName);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_product ObjPro = new t_product();
                        ObjPro.category_id = ObjProduct.CategoryId;

                        ObjPro.product_name = ObjProduct.ProductName;
                        //  ObjPro.price = ObjProduct.Price;
                        //  ObjPro.unit = ObjProduct.UnitName;
                        ObjPro.is_active = ObjProduct.IsActive;
                        //ObjPro.created_by = ObjProduct.CreatedBy;
                        //ObjPro.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_product.Add(ObjPro);
                        Context.SaveChanges();
                        iRes = ObjPro.product_id;
                        ObjPro.product_image = API.CommonClasses.SaveBase64File.SaveBase64Image(iRes, "ProductImage", ObjProduct.ProductName, ObjProduct.ProductImage, ".jpg");
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

        public int Update(Product_Model ObjProduct)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_product.FirstOrDefault(x => x.category_id == ObjProduct.CategoryId && x.product_name == ObjProduct.ProductName && x.product_id != ObjProduct.ProductId);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_product.SingleOrDefault(x => x.product_id == ObjProduct.ProductId);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (ObjProduct.CategoryId != null)
                                IDExist.category_id = ObjProduct.CategoryId;

                            if (!string.IsNullOrEmpty(ObjProduct.ProductName))
                                IDExist.product_name = ObjProduct.ProductName;
                            if (!string.IsNullOrEmpty(ObjProduct.ProductImage))
                                IDExist.product_image = API.CommonClasses.SaveBase64File.SaveBase64Image(ObjProduct.ProductId, "ProductImage", ObjProduct.ProductName, ObjProduct.ProductImage, ".jpg");

                            if (ObjProduct.IsActive != null)
                                IDExist.is_active = ObjProduct.IsActive;
                            //IDExist.updated_by = ObjProduct.UpdateBy;
                            //IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;

                            //if (ObjProduct.UnitName != null)
                            //    //     IDExist.unit = ObjProduct.UnitName;
                            //    if (ObjProduct.Price != null)
                            //        //     IDExist.price = ObjProduct.Price;


                                    Context.SaveChanges();
                            iRes = ObjProduct.ProductId;
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

        public int Delete(int ProductID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_product product = Context.t_product.SingleOrDefault(x => x.product_id == ProductID);
                    Context.t_product.Remove(product);
                    Context.SaveChanges();
                }
                return iRes = ProductID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Product_Model> GetActiveProduct()
        {
            List<Product_Model> ObjProduct = new List<Product_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjProduct = (from a in Context.t_product

                                         where a.is_active == true
                                         select new Product_Model
                                         {
                                             CategoryId = a.category_id,
                                             CategoryName = a.t_category.category_name,

                                             ProductId = a.product_id,
                                             //  UnitName=a.unit,
                                             ProductName = a.product_name,
                                             ProductImage = a.product_image,

                                             //   Price = a.price,

                                             IsActive = a.is_active,

                                         }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Product_Model> GetProductByCategoryID(int CategoryID)
        {
            List<Product_Model> ObjProduct = new List<Product_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjProduct = (from a in Context.t_product

                                         where a.category_id == CategoryID && a.is_active == true
                                         select new Product_Model
                                         {
                                             CategoryId = a.category_id,
                                             CategoryName = a.t_category.category_name,

                                             ProductId = a.product_id,
                                             // UnitName=a.unit,
                                             ProductName = a.product_name,
                                             ProductImage = a.product_image,

                                             //  Price = a.price,


                                             IsActive = a.is_active,
                                             //CreatedBy = a.created_by,
                                             //CreatedDate = a.created_date,
                                             //UpdateBy = a.updated_by,
                                             //UpdateDate = a.updated_date
                                         }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Product_Model> GetAllProduct()
        {
            List<Product_Model> ObjProduct = new List<Product_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjProduct = (from a in Context.t_product

                                         select new Product_Model
                                         {
                                             CategoryId = a.category_id,
                                             CategoryName = a.t_category.category_name,
                                             //  UnitName=a.unit,
                                             ProductId = a.product_id,

                                             ProductName = a.product_name,
                                             ProductImage = a.product_image,

                                             //       Price = a.price,

                                             IsActive = a.is_active,
                                             //CreatedBy = a.created_by,
                                             //CreatedDate = a.created_date,
                                             //UpdateBy = a.updated_by,
                                             //UpdateDate = a.updated_date
                                         }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Product_Model GetProductByID(int ProductID)
        {
            Product_Model ObjProduct = new Product_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjProduct = (from a in Context.t_product

                                         where a.product_id == ProductID
                                         select new Product_Model
                                         {
                                             CategoryId = a.category_id,
                                             CategoryName = a.t_category.category_name,
                                             //    UnitName=a.unit,
                                             ProductId = a.product_id,

                                             ProductName = a.product_name,
                                             ProductImage = a.product_image,

                                             //        Price = a.price,

                                             IsActive = a.is_active,
                                             //CreatedBy = a.created_by,
                                             //CreatedDate = a.created_date,
                                             //UpdateBy = a.updated_by,
                                             //UpdateDate = a.updated_date
                                         }).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Product_Model> GetServicePrice()
        {
            List<Product_Model> obj = new List<Product_Model>();
            try
            {
                using (maxcleanEntities context = new maxcleanEntities())
                {
                    return obj = (from a in context.t_service_price
                                  where a.t_product.is_active == true
                                  select new Product_Model
                                  {

                                      ServiceProductID = a.product_id,
                                     
                                      ProductName = a.t_product.product_name,
                                      CategoryId = a.t_product.t_category.category_id,
                                      CategoryName = a.t_product.t_category.category_name,
                                      ServiceID = a.service_id,
                                      ServiceName = a.t_service.service_name,
                                      ServicePrice = a.price,
                                      ProductImage = a.t_product.product_image,
                                      CategoryIcon = a.t_product.t_category.category_icon,
                                      CategoryBanner = a.t_product.t_category.category_banner

                                  }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Service_Model> GetProductPrices()
        {
            try
            {
                List<Service_Model> ServiceList = new List<Service_Model>();
                Category_Repository categoryRepository = new Category_Repository();
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                     ServiceList = (from s in Context.t_service
                                       where s.is_active == true
                                       select new Service_Model()
                                       {
                                           ServiceID = s.service_id,
                                           ServiceName = s.service_name,
                                           DisplayOrder = s.display_order,
                                           IsActive = s.is_active,

                                       }).ToList();

                    foreach(var srv in ServiceList)
                    {
                        var Res = (from a in Context.t_service_price
                                   where a.service_id == srv.ServiceID
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
                        foreach (var cat in CatList)
                        {
                            cat.ProductList = Res.Where(x => x.CategoryID == cat.CategoryID).Select(x => new Shop_Product { ProductId = x.ProductId, ProductImage = x.ProductImage, Price = x.Price, ProductName = x.ProductName }).ToList();
                        }
                        srv.Categorylist = CatList;

                    }
                }
                return ServiceList;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<mProduct_Model> GetProductByCategoryServiceID(int ServiceID,int CategoryID)
        {
            List<mProduct_Model> ObjProduct = new List<mProduct_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjProduct = (from a in Context.t_product
                                         join p in Context.t_service_price on a.product_id equals p.product_id
                                         where a.category_id == CategoryID && p.service_id == ServiceID
                                         select new mProduct_Model
                                         {
                                             
                                             ProductId = a.product_id,
                                             ProductName = a.product_name,
                                             ProductImage = a.product_image,
                                              Price = p.price,
                                         }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}