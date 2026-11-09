using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class CustomerAddress_Repository : CustomerAddress_IRepository
    {
        public int Add(CustomerAddress_Model ObjCustomerAddress)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_customer_address.FirstOrDefault(x => x.customer_id == ObjCustomerAddress.CustomerID);  //x.subcategory_name == ObjSubCategory.SubCategoryName);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_customer_address ObjCoun = new t_customer_address();
                        ObjCoun.customer_id = ObjCustomerAddress.CustomerID;
                        ObjCoun.address_type = ObjCustomerAddress.AddressType;
                        ObjCoun.address = ObjCustomerAddress.Address;
                        ObjCoun.city = ObjCustomerAddress.City;
                        ObjCoun.state = ObjCustomerAddress.State;
                        ObjCoun.country = ObjCustomerAddress.Country;
                        ObjCoun.pincode = ObjCustomerAddress.Pincode;
                        ObjCoun.latitude = ObjCustomerAddress.Latitude;
                        ObjCoun.longitude = ObjCustomerAddress.Longitute;
                        ObjCoun.is_active = ObjCustomerAddress.IsActive;
                        ObjCoun.created_by = ObjCustomerAddress.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_customer_address.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.customer_address_id;
                        //ObjCoun.subcategory_icon = API.CommonClasses.SaveBase64File.SaveBase64Image(iRes, "SubCategoryIcon", ObjSubCategory.SubCategoryName, ObjSubCategory.SubCategoryIcon, ".jpg");
                        //ObjCoun.subcategory_banner = API.CommonClasses.SaveBase64File.SaveBase64Image(iRes, "SubCategoryBanner", ObjSubCategory.SubCategoryName, ObjSubCategory.SubCategoryBanner, ".jpg");
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

        public int Delete(int CUstomerAddressID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_customer_address customeraddress = Context.t_customer_address.SingleOrDefault(x => x.customer_address_id == CUstomerAddressID);
                    Context.t_customer_address.Remove(customeraddress);
                    Context.SaveChanges();
                }
                return iRes = CUstomerAddressID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(CustomerAddress_Model ObjCustomerAddress)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    //var Exist = Context.t_customer_address.FirstOrDefault(x =>  x.customer_address_id != ObjCustomerAddress.CUstomerAddressID && x.customer_id == ObjCustomerAddress.CustomerID  );
                    //if (Exist != null)
                    //{
                    //    iRes = -1;
                    //}
                    //else
                    //{
                        var IDExist = Context.t_customer_address.SingleOrDefault(x => x.customer_address_id == ObjCustomerAddress.CUstomerAddressID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (ObjCustomerAddress.CustomerID != null)
                                IDExist.customer_id = ObjCustomerAddress.CustomerID;

                            if (!string.IsNullOrEmpty(ObjCustomerAddress.AddressType))
                                IDExist.address_type = ObjCustomerAddress.AddressType;
                            if (!string.IsNullOrEmpty(ObjCustomerAddress.Address))
                                IDExist.address = ObjCustomerAddress.Address;

                            if (!string.IsNullOrEmpty(ObjCustomerAddress.City))
                                IDExist.city = ObjCustomerAddress.City;
                            if (!string.IsNullOrEmpty(ObjCustomerAddress.State))
                                IDExist.state = ObjCustomerAddress.State;
                            if (!string.IsNullOrEmpty(ObjCustomerAddress.Country))
                                IDExist.country = ObjCustomerAddress.Country;
                            if (!string.IsNullOrEmpty(ObjCustomerAddress.Pincode))
                                IDExist.pincode = ObjCustomerAddress.Pincode;

                            if (!string.IsNullOrEmpty(ObjCustomerAddress.Latitude))
                                IDExist.latitude = ObjCustomerAddress.Latitude;
                            if (!string.IsNullOrEmpty(ObjCustomerAddress.Longitute))
                                IDExist.longitude = ObjCustomerAddress.Longitute;

                            //if (!string.IsNullOrEmpty(ObjCustomerAddress.SubCategoryIcon))
                            //    IDExist.subcategory_icon = API.CommonClasses.SaveBase64File.SaveBase64Image(ObjSubCategory.SubCategoryID, "CategoryIcon", ObjSubCategory.SubCategoryName, ObjSubCategory.SubCategoryIcon, ".jpg");
                            //if (!string.IsNullOrEmpty(ObjCustomerAddress.SubCategoryBanner))
                            //    IDExist.subcategory_banner = API.CommonClasses.SaveBase64File.SaveBase64Image(ObjSubCategory.SubCategoryID, "CategoryBanner", ObjSubCategory.CategoryName, ObjSubCategory.SubCategoryBanner, ".jpg");
                            //if (ObjCustomerAddress.SubCategoryDisplayOrder != null)
                            //    IDExist.display_order = ObjCustomerAddress.SubCategoryDisplayOrder;
                            if (ObjCustomerAddress.IsActive != null)
                                IDExist.is_active = ObjCustomerAddress.IsActive;
                            IDExist.updated_by = ObjCustomerAddress.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjCustomerAddress.CUstomerAddressID;
                        }
                   //}
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CustomerAddress_Model> GetActiveCustomerAddress()
        {
            List<CustomerAddress_Model> ObjCustomerAddresses = new List<CustomerAddress_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCustomerAddresses = (from a in Context.t_customer_address

                                               select new CustomerAddress_Model
                                               {
                                                   CUstomerAddressID = a.customer_address_id,
                                                   CustomerID = a.customer_id,
                                                   CustomerName = a.t_customer.customer_name,
                                                   Address = a.address,
                                                   AddressType = a.address_type,
                                                   City = a.city,
                                                   State = a.state,
                                                   Country = a.country,
                                                   Pincode = a.pincode,
                                                   Latitude = a.latitude,
                                                   Longitute = a.longitude,

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

        public List<CustomerAddress_Model> GetAllCustomerAddress()
        {
            List<CustomerAddress_Model> ObjCustomerAddresses = new List<CustomerAddress_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCustomerAddresses = (from a in Context.t_customer_address
                                                   select new CustomerAddress_Model
                                            {
                                                CUstomerAddressID = a.customer_address_id,
                                                CustomerID = a.customer_id,
                                                CustomerName = a.t_customer.customer_name,
                                                Address = a.address,
                                                AddressType = a.address_type,
                                                City = a.city,
                                                State = a.state,
                                                Country = a.country,
                                                Pincode = a.pincode,
                                                Latitude = a.latitude,
                                                Longitute = a.longitude,

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

        public List<CustomerAddress_Model> GetCustomerAddressByCustomerID(int CustomerID)
        {
            List<CustomerAddress_Model> ObjCustomerAddresses = new List<CustomerAddress_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCustomerAddresses = (from a in Context.t_customer_address
                                               where a.t_customer.customer_id == CustomerID
                                               select new CustomerAddress_Model
                                               {
                                                   CUstomerAddressID = a.customer_address_id,
                                                   CustomerID = a.customer_id,
                                                   CustomerName = a.t_customer.customer_name,
                                                   Address = a.address,
                                                   AddressType = a.address_type,
                                                   City = a.city,
                                                   State = a.state,
                                                   Country = a.country,
                                                   Pincode = a.pincode,
                                                   Latitude = a.latitude,
                                                   Longitute = a.longitude,

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

        public CustomerAddress_Model GetCustomerAddressByID(int CUstomerAddressID)
        {
            CustomerAddress_Model ObjCustomerAddresses = new CustomerAddress_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCustomerAddresses = (from a in Context.t_customer_address
                                               where a.customer_address_id == CUstomerAddressID
                                                   select new CustomerAddress_Model
                                               {
                                                   CUstomerAddressID = a.customer_address_id,
                                                   CustomerID = a.customer_id,
                                                   CustomerName = a.t_customer.customer_name,
                                                   Address = a.address,
                                                   AddressType = a.address_type,
                                                   City = a.city,
                                                   State = a.state,
                                                   Country = a.country,
                                                   Pincode = a.pincode,
                                                   Latitude = a.latitude,
                                                   Longitute = a.longitude,

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