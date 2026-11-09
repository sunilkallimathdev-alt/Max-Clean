using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Batch_Repository : Batch_IRepository
    {
        public long Add(Batch_Model ObjBatch)
        {
            long iRes = 0;
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    var Exist = Context.t_batch.FirstOrDefault(x => x.batch_name == ObjBatch.BatchName);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_batch ObjCoun = new t_batch();
                        ObjCoun.store_id = ObjBatch.StoreID;
                        ObjCoun.batch_name = ObjBatch.BatchName;
                        ObjCoun.batch_started = CommonClasses.StaticData.GetCurrentDataTime;
                        //ObjCoun.batch_ended = ObjBatch.BatchEnded;
                        ObjCoun.exp_date = ObjBatch.Exp_Date;
                        ObjCoun.is_completed = false;
                        ObjCoun.created_by = ObjBatch.CreatedBy;
                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_batch.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.batch_id;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long Update(Batch_Model ObjBatch)
        {
            long iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_batch.FirstOrDefault(x => x.batch_name == ObjBatch.BatchName && x.batch_id != ObjBatch.BatchID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_batch.SingleOrDefault(x => x.batch_id == ObjBatch.BatchID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjBatch.BatchName))
                                IDExist.batch_name = ObjBatch.BatchName;
                            if (ObjBatch.BatchStarted != null)
                                IDExist.batch_started = ObjBatch.BatchStarted;
                            if (ObjBatch.BatchEnded != null)
                                IDExist.batch_ended = CommonClasses.StaticData.GetCurrentDataTime;
                            if (ObjBatch.Exp_Date != null)
                                IDExist.exp_date = ObjBatch.Exp_Date;
                            if (ObjBatch.AveragePrice != null)
                                IDExist.average_price = ObjBatch.AveragePrice;
                            if (ObjBatch.IsActive != null)
                                IDExist.is_completed = ObjBatch.IsActive;
                            IDExist.updated_by = ObjBatch.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjBatch.BatchID;
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

        public List<Batch_Model> GetAllBatch()
        {
            List<Batch_Model> ObjTicket = new List<Batch_Model>();
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    return ObjTicket = (from a in Context.t_batch
                                        select new Batch_Model
                                        {
                                            BatchID = a.batch_id,
                                            BatchName = a.batch_name,
                                            StoreID = a.store_id,
                                            StoreName = a.t_store.store_name,
                                            StoreMobile = a.t_store.mobile_no,
                                            BatchStarted = a.batch_started,
                                            BatchEnded = a.batch_ended,
                                            Exp_Date = a.exp_date,
                                            IsActive = a.is_completed,
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

        public List<BatchVendor_Model> GetAllInActiveBatch()
        {
            List<BatchVendor_Model> ObjTicket = new List<BatchVendor_Model>();
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    ObjTicket = (from a in Context.t_batch
                                 where a.is_completed == false
                                 select new BatchVendor_Model
                                 {
                                     BatchID = a.batch_id,
                                     BatchName = a.batch_name,
                                     StoreID = a.store_id,
                                     StoreName = a.t_store.store_name,
                                     StoreMobile = a.t_store.mobile_no,
                                     BatchStarted = a.batch_started,
                                     BatchEnded = a.batch_ended,
                                     Exp_Date = a.exp_date,
                                     IsActive = a.is_completed,
                                     CreatedBy = a.created_by,
                                     CreatedDate = a.created_date,
                                     UpdateBy = a.updated_by,
                                     UpdateDate = a.updated_date
                                 }).ToList();
                    foreach (var batch in ObjTicket)
                    {
                        batch.Qunatity = Context.t_batch_vendor.Where(x => x.batch_id == batch.BatchID).Sum(x => x.quantity) ?? 0;
                    }
                    return ObjTicket;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Batch_Model GetBatchByID(long BatchID)
        {
            Batch_Model ObjRole = new Batch_Model();
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    return ObjRole = (from a in Context.t_batch
                                      where a.batch_id == BatchID
                                      select new Batch_Model
                                      {
                                          BatchID = a.batch_id,
                                          BatchName = a.batch_name,
                                          StoreID = a.store_id,
                                          StoreName = a.t_store.store_name,
                                          StoreMobile = a.t_store.mobile_no,
                                          BatchStarted = a.batch_started,
                                          BatchEnded = a.batch_ended,
                                          Exp_Date = a.exp_date,
                                          IsActive = a.is_completed,
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