
using API.DataAccess;
using API.IRepository;
using API.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Store_Repository : Store_IRepository
    {
        public int Add(Store_Model ObjStore)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_store.FirstOrDefault(x => x.mobile_no == ObjStore.MobileNumber && x.store_name == ObjStore.StoreName &&x.email_id == ObjStore.EmailID && x.store_code == ObjStore.Store_code);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_store ObjCoun = new t_store();
                        ObjCoun.store_name = ObjStore.StoreName;
                        ObjCoun.manager_name = ObjStore.ManagerName;
                        ObjCoun.mobile_no = ObjStore.MobileNumber;
                        ObjCoun.email_id = ObjStore.EmailID;
                        ObjCoun.address = ObjStore.Address;
                        ObjCoun.city = ObjStore.City;
                        ObjCoun.state = ObjStore.State;
                        ObjCoun.store_code = ObjStore.Store_code;
                        ObjCoun.country = ObjStore.Country;
                        ObjCoun.latitude = ObjStore.Latitude;
                        ObjCoun.longitute = ObjStore.Longitude;
                        ObjCoun.radius = ObjStore.Radius;
                        ObjCoun.pincode = ObjStore.PinCode;
                        ObjCoun.is_active = ObjStore.IsActive;
                        ObjCoun.created_by = ObjStore.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_store.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.store_id;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int StoreID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_store store = Context.t_store.SingleOrDefault(x => x.store_id == StoreID);
                    Context.t_store.Remove(store);
                    Context.SaveChanges();
                }
                return iRes = StoreID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Store_Model> GetActiveStores()
        {
            List<Store_Model> ObjStores = new List<Store_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjStores = (from a in Context.t_store
                                        where a.is_active == true
                                        select new Store_Model
                                        {
                                            StoreID = a.store_id,
                                            ManagerName = a.manager_name,
                                            StoreName = a.store_name,
                                            MobileNumber = a.mobile_no,
                                            EmailID = a.email_id,
                                            Address = a.address,
                                            City = a.city,
                                            State = a.state,
                                            Country = a.country,
                                            Radius= a.radius,
                                            Latitude = a.latitude,
                                            Longitude = a.longitute,
                                            PinCode = a.pincode,
                                            Store_code =a.store_code,
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

        public List<Store_Model> GetAllStores()
        {


            List<Store_Model> ObjCategories = new List<Store_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCategories = (from a in Context.t_store
                                            select new Store_Model
                                            {
                                                StoreID = a.store_id,
                                                ManagerName = a.manager_name,
                                                StoreName = a.store_name,
                                                MobileNumber = a.mobile_no,
                                                EmailID = a.email_id,
                                                Address = a.address,
                                                City = a.city,
                                                State = a.state,
                                                Country = a.country,
                                                Latitude = a.latitude,
                                                Radius = a.radius,
                                                Longitude = a.longitute,
                                                PinCode = a.pincode,
                                                Store_code = a.store_code,
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
        public Store_Model GetStoreByID(int StoreID)
        {
            Store_Model ObjCategories = new Store_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCategories = (from a in Context.t_store
                                            where a.store_id == StoreID
                                            select new Store_Model
                                            {
                                                StoreID = a.store_id,
                                                ManagerName = a.manager_name,
                                                StoreName = a.store_name,
                                                MobileNumber = a.mobile_no,
                                                EmailID = a.email_id,
                                                Address = a.address,
                                                City = a.city,
                                                State = a.state,
                                                Country = a.country,
                                                Latitude = a.latitude,
                                                Radius = a.radius,
                                                Store_code = a.store_code,
                                                Longitude = a.longitute,
                                                PinCode = a.pincode,
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

        public int Update(Store_Model ObjStore)
        {

            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_store.FirstOrDefault(x => (x.mobile_no == ObjStore.MobileNumber && x.store_name == ObjStore.StoreName && x.email_id == ObjStore.EmailID && x.store_code==ObjStore.Store_code) && x.store_id != ObjStore.StoreID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_store.SingleOrDefault(x => x.store_id == ObjStore.StoreID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjStore.StoreName))
                                IDExist.store_name = ObjStore.StoreName;
                            if (!string.IsNullOrEmpty(ObjStore.ManagerName))
                                IDExist.manager_name = ObjStore.ManagerName;
                            if (!string.IsNullOrEmpty(ObjStore.MobileNumber))
                                IDExist.mobile_no = ObjStore.MobileNumber;
                            if (!string.IsNullOrEmpty(ObjStore.EmailID))
                                IDExist.email_id = ObjStore.EmailID;
                            if (!string.IsNullOrEmpty(ObjStore.Address))
                                IDExist.address = ObjStore.Address;
                            if (!string.IsNullOrEmpty(ObjStore.PinCode))
                                IDExist.pincode = ObjStore.PinCode;
                            if (!string.IsNullOrEmpty(ObjStore.City))
                                IDExist.city = ObjStore.City;
                            if (!string.IsNullOrEmpty(ObjStore.State))
                                IDExist.state = ObjStore.State;
                            if (!string.IsNullOrEmpty(ObjStore.Store_code))
                                IDExist.store_code = ObjStore.Store_code;
                            if (!string.IsNullOrEmpty(ObjStore.Country))
                                IDExist.country = ObjStore.Country;
                            if (!string.IsNullOrEmpty(ObjStore.Latitude))
                                IDExist.latitude = ObjStore.Latitude;
                            if (!string.IsNullOrEmpty(ObjStore.Longitude))
                                IDExist.longitute = ObjStore.Longitude;
                            if (ObjStore.IsActive != null)
                                IDExist.is_active = ObjStore.IsActive;
                            if (ObjStore.Radius != null)
                                IDExist.radius = ObjStore.Radius;
                            IDExist.updated_by = ObjStore.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjStore.StoreID;
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

        public List<Store_Model> GetStoresByLocation(GeoLocation_Model data)
        {          
         
            try
            {
                string str = System.Configuration.ConfigurationManager.ConnectionStrings["ADOConnectionString"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(str))
                {
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter("SP_StoreAtYourLoc", connection);
                    //tell using Storeproc
                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //add parameters
                    dataAdapter.SelectCommand.Parameters.AddWithValue("UserLattitude", data.Latitude);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("UserLongitude", data.Longitude);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);

                    List<Store_Model> stores = new List<Store_Model>();
                    if (dataSet.Tables[0].Rows.Count>0) {

                        for (int i=0;i< dataSet.Tables[0].Rows.Count;i++)
                        {
                            if (dataSet.Tables[0].Rows[i]["is_active"] != "0")
                            {
                                Store_Model store = new Store_Model();
                                store.Address = Convert.ToString(dataSet.Tables[0].Rows[i]["address"]);
                                store.City = Convert.ToString(dataSet.Tables[0].Rows[i]["city"]);
                                store.Country = Convert.ToString(dataSet.Tables[0].Rows[i]["country"]);
                                store.EmailID = Convert.ToString(dataSet.Tables[0].Rows[i]["email_id"]);
                                store.Latitude = Convert.ToString(dataSet.Tables[0].Rows[i]["latitude"]);
                                store.Longitude = Convert.ToString(dataSet.Tables[0].Rows[i]["longitute"]);
                                store.ManagerName = Convert.ToString(dataSet.Tables[0].Rows[i]["manager_name"]);
                                store.MobileNumber = Convert.ToString(dataSet.Tables[0].Rows[i]["mobile_no"]);
                                store.PinCode = Convert.ToString(dataSet.Tables[0].Rows[i]["pincode"]);
                                store.State = Convert.ToString(dataSet.Tables[0].Rows[i]["state"]);
                                store.StoreID = Convert.ToInt32(dataSet.Tables[0].Rows[i]["store_id"]);
                                store.StoreName = Convert.ToString(dataSet.Tables[0].Rows[i]["store_name"]);
                                store.IsActive = Convert.ToBoolean(dataSet.Tables[0].Rows[i]["is_active"]);
                                stores.Add(store);
                            }
                           
                            
                        }
                     

                    }

                  

                    return stores;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}