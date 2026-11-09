using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Vendor_Repository : Vendor_IRepository
    {
        public int Add(Vendor_Model ObjVendor)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_vendor.FirstOrDefault(x => x.mobile_no == ObjVendor.MobileNumber);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_vendor ObjCoun = new t_vendor();
                        ObjCoun.vendor_name = ObjVendor.VendorName;
                        ObjCoun.mobile_no = ObjVendor.MobileNumber;
                        ObjCoun.alt_mobile_no = ObjVendor.AltMobileNumber;
                        ObjCoun.email_id = ObjVendor.EmailID;
                        ObjCoun.address = ObjVendor.Address;
                        ObjCoun.city = ObjVendor.City;
                        ObjCoun.state = ObjVendor.State;
                        ObjCoun.country = ObjVendor.Country;
                        ObjCoun.pincode = ObjVendor.PinCode;
                        ObjCoun.latitude = ObjVendor.Latitude;
                        ObjCoun.longitude = ObjVendor.Longitude;
                        ObjCoun.HSN = ObjVendor.HSN;
                        ObjCoun.bank = ObjVendor.Bank;
                        ObjCoun.bank_account_no = ObjVendor.BankAccountNo;
                        ObjCoun.IFSC_code = ObjVendor.IFSC;
                        ObjCoun.remark = ObjVendor.Remark;
                        ObjCoun.is_active = ObjVendor.IsActive;
                        ObjCoun.created_by = ObjVendor.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_vendor.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.vendor_id;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int VendorID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_vendor vendor = Context.t_vendor.SingleOrDefault(x => x.vendor_id == VendorID);
                    Context.t_vendor.Remove(vendor);
                    Context.SaveChanges();
                }
                return iRes = VendorID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Vendor_Model> GetActiveVendors()
        {
            List<Vendor_Model> ObjStores = new List<Vendor_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjStores = (from a in Context.t_vendor
                                        where a.is_active == true
                                        select new Vendor_Model
                                        {
                                            VendorID = a.vendor_id,
                                            VendorName = a.vendor_name,
                                            MobileNumber = a.mobile_no,
                                            AltMobileNumber = a.alt_mobile_no,
                                            EmailID = a.email_id,
                                            Address = a.address,
                                            City = a.city,
                                            State = a.state,
                                            Country = a.country,
                                            PinCode = a.pincode,
                                            Latitude = a.latitude,
                                            Longitude = a.longitude,
                                            HSN = a.HSN,
                                            Bank = a.bank,
                                            BankAccountNo = a.bank_account_no,
                                            IFSC = a.IFSC_code,
                                            Remark = a.remark,
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

        public List<Vendor_Model> GetAllVendors()
        {

            List<Vendor_Model> ObjCategories = new List<Vendor_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCategories = (from a in Context.t_vendor
                                            select new Vendor_Model
                                            {
                                                VendorID = a.vendor_id,
                                                VendorName = a.vendor_name,
                                                MobileNumber = a.mobile_no,
                                                AltMobileNumber = a.alt_mobile_no,
                                                EmailID = a.email_id,
                                                Address = a.address,
                                                City = a.city,
                                                State = a.state,
                                                Country = a.country,
                                                PinCode = a.pincode,
                                                Latitude = a.latitude,
                                                Longitude = a.longitude,
                                                HSN = a.HSN,
                                                Bank = a.bank,
                                                BankAccountNo = a.bank_account_no,
                                                IFSC = a.IFSC_code,
                                                Remark = a.remark,
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

        public Vendor_Model GetVendorByID(int VendorID)
        {
            Vendor_Model ObjVendors = new Vendor_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjVendors = (from a in Context.t_vendor
                                         where a.vendor_id == VendorID
                                         select new Vendor_Model
                                         {
                                             VendorID = a.vendor_id,
                                             VendorName = a.vendor_name,
                                             MobileNumber = a.mobile_no,
                                             AltMobileNumber = a.alt_mobile_no,
                                             EmailID = a.email_id,
                                             Address = a.address,
                                             City = a.city,
                                             State = a.state,
                                             Country = a.country,
                                             PinCode = a.pincode,
                                             Latitude = a.latitude,
                                             Longitude = a.longitude,
                                             HSN = a.HSN,
                                             Bank = a.bank,
                                             BankAccountNo = a.bank_account_no,
                                             Remark = a.remark,
                                             IFSC = a.IFSC_code,
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

        public int Update(Vendor_Model ObjVendor)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_vendor.FirstOrDefault(x => x.mobile_no == ObjVendor.MobileNumber && x.vendor_id != ObjVendor.VendorID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_vendor.SingleOrDefault(x => x.vendor_id == ObjVendor.VendorID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjVendor.VendorName))
                                IDExist.vendor_name = ObjVendor.VendorName;
                            if (!string.IsNullOrEmpty(ObjVendor.MobileNumber))
                                IDExist.mobile_no = ObjVendor.MobileNumber;
                            if (!string.IsNullOrEmpty(ObjVendor.AltMobileNumber))
                                IDExist.alt_mobile_no = ObjVendor.AltMobileNumber;
                            if (!string.IsNullOrEmpty(ObjVendor.EmailID))
                                IDExist.email_id = ObjVendor.EmailID;
                            if (!string.IsNullOrEmpty(ObjVendor.Address))
                                IDExist.address = ObjVendor.Address;

                            if (!string.IsNullOrEmpty(ObjVendor.City))
                                IDExist.city = ObjVendor.City;
                            if (!string.IsNullOrEmpty(ObjVendor.State))
                                IDExist.state = ObjVendor.State;
                            if (!string.IsNullOrEmpty(ObjVendor.Country))
                                IDExist.country = ObjVendor.Country;
                            if (!string.IsNullOrEmpty(ObjVendor.PinCode))
                                IDExist.pincode = ObjVendor.PinCode;
                            if (!string.IsNullOrEmpty(ObjVendor.Latitude))
                                IDExist.latitude = ObjVendor.Latitude;
                            if (!string.IsNullOrEmpty(ObjVendor.Longitude))
                                IDExist.longitude = ObjVendor.Longitude;
                            if (!string.IsNullOrEmpty(ObjVendor.HSN))
                                IDExist.HSN = ObjVendor.HSN;
                            if (!string.IsNullOrEmpty(ObjVendor.Bank))
                                IDExist.bank = ObjVendor.Bank;
                            if (!string.IsNullOrEmpty(ObjVendor.BankAccountNo))
                                IDExist.bank_account_no = ObjVendor.BankAccountNo;
                            if (!string.IsNullOrEmpty(ObjVendor.IFSC))
                                IDExist.IFSC_code = ObjVendor.IFSC;
                            if (!string.IsNullOrEmpty(ObjVendor.Remark))
                                IDExist.remark = ObjVendor.Remark;

                            if (ObjVendor.IsActive != null)
                                IDExist.is_active = ObjVendor.IsActive;
                            IDExist.updated_by = ObjVendor.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjVendor.VendorID;
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