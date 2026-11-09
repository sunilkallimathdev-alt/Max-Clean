using API.CommonClasses;
using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class BatchVendor_Repository : BatchVendor_IRepository
    {
        public long AddBatchVendor(BatchVendor_Model ObjBV)
        {
            long iRes = 0;
            try
            {
                lock (ObjBV)
                {
                    if (ObjBV.RequestOrderID != null && ObjBV.BatchID != null && ObjBV.Qunatity != null)
                    {
                        using (DataAccess.coffeebrewEntities Context = new DataAccess.coffeebrewEntities())
                        {
                            //var ROID = Context.t_vpo.SingleOrDefault(x => x.vpo_id == ObjBV.RequestOrderID);
                            //if (ROID.available_stock >= ObjBV.Qunatity && ObjBV.Qunatity > 0)
                            //{
                            //    ROID.sold_stock = ROID.sold_stock + ObjBV.Qunatity;
                            //    ROID.available_stock = ROID.available_stock - ObjBV.Qunatity;

                            //    var StoreStock = Context.t_inventary_available_stock.SingleOrDefault(x => x.store_id == ROID.store_id);
                            //    StoreStock.raw_beans = StoreStock.raw_beans - ObjBV.Qunatity;

                            //    var batchstock = Context.t_batch_pro.SingleOrDefault(x => x.batch_id == ObjBV.BatchID);
                            //    batchstock.raw_beans_received = batchstock.raw_beans_received + ObjBV.Qunatity;
                            //    //batchstock.raw_start_date = StaticData.GetCurrentDataTime;

                            //    var vendorexist = Context.t_batch_vendor.SingleOrDefault(x => x.vpo_id == ROID.vpo_id && x.batch_id == ObjBV.BatchID);
                            //    if (vendorexist != null)
                            //    {
                            //        vendorexist.quantity = vendorexist.quantity + ObjBV.Qunatity;
                            //        vendorexist.updated_date = StaticData.GetCurrentDataTime;
                            //    }
                            //    else
                            //    {
                            //        t_batch_vendor Objbv = new t_batch_vendor();
                            //        Objbv.batch_id = ObjBV.BatchID;
                            //        Objbv.vpo_id = ObjBV.RequestOrderID;
                            //        Objbv.quantity = ObjBV.Qunatity;
                            //        Objbv.created_date = StaticData.GetCurrentDataTime;
                            //        Context.t_batch_vendor.Add(Objbv);
                            //    }
                            //    Context.SaveChanges();
                            //    iRes =Convert.ToInt32(ObjBV.RequestOrderID);
                            //}
                            //else
                            //{
                            //    iRes = -5;
                            //}
                        }
                    }
                    else
                    {
                        iRes = 0;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VendorProductOrder_Model> GetRequestedOrderIDStock(long BatchID)
        {
            List<VendorProductOrder_Model> Objvpod = new List<VendorProductOrder_Model>();
            try
            {
                using (DataAccess.coffeebrewEntities Context = new coffeebrewEntities())
                {
                    return Objvpod = (from a in Context.t_vpo
                                      join b in Context.t_batch on a.store_id equals b.store_id
                                      where b.batch_id == BatchID && a.available_stock > 0
                                      select new VendorProductOrder_Model
                                      {
                                          VendorPaymentOrderID = a.vpo_id,
                                          VendorID = a.vendor_id,
                                          VendorName = a.t_vendor.vendor_name,
                                          VendorMobileNO = a.t_vendor.mobile_no,
                                          StoreID = a.store_id,
                                          StoreName = a.t_store.store_name,
                                          StoreMobileNo = a.t_store.mobile_no,
                                          VendorOrderID = a.vendor_order_id,
                                          PaidAmount = a.paid_amount,
                                          RemainingAmount = a.remaining_balance,
                                          GrandTotal = a.grand_total,
                                          PricePerKG = a.price_per_kg,
                                          RequestedQuantity = a.requested_quantity,
                                          ReceivedQuantity = a.received_quantity,
                                          ExpectingDeliveryDate = a.expecting_delivery_date,
                                          ActualDeliveryDate = a.actual_delivery_date,
                                          SoldStock = a.sold_stock,
                                          AvailableStock = a.available_stock,
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
    }
}