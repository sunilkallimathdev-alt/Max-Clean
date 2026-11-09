using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Order_Cycle_Repository : IOrder_CycleStage_Repository
    {
        public int Add(orderCycle_Model ObjOrderCycle)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_order_cylce.FirstOrDefault(x => x.order_cycle_stage == ObjOrderCycle.OrderCycleStage || x.diplay_order == ObjOrderCycle.DisplayOrder);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_order_cylce ObjCycle = new t_order_cylce();
                        ObjCycle.order_cycle_stage = ObjOrderCycle.OrderCycleStage;
                        ObjCycle.diplay_order = ObjOrderCycle.DisplayOrder;
                        ObjCycle.is_active = ObjOrderCycle.IsActive;
                        ObjCycle.created_by = ObjOrderCycle.CreatedBy;
                        ObjCycle.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_order_cylce.Add(ObjCycle);
                        Context.SaveChanges();
                        iRes = ObjCycle.order_cycle_id;
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int OrderCycleID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_order_cylce ordercyc = Context.t_order_cylce.SingleOrDefault(x => x.order_cycle_id == OrderCycleID);
                    Context.t_order_cylce.Remove(ordercyc);
                    Context.SaveChanges();
                }
                return iRes = OrderCycleID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<orderCycle_Model> GetActiveOrderCycle()
        {
            List<orderCycle_Model> ObjCycle = new List<orderCycle_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCycle = (from a in Context.t_order_cylce
                                       where a.is_active == true
                                       select new orderCycle_Model
                                       {
                                           OrderCycleID = a.order_cycle_id,
                                           OrderCycleStage = a.order_cycle_stage,
                                           DisplayOrder = a.diplay_order,
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

        public List<orderCycle_Model> GetAllOrderCycle()
        {
            List<orderCycle_Model> ObjCycle = new List<orderCycle_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCycle = (from a in Context.t_order_cylce
                                       select new orderCycle_Model
                                       {
                                           OrderCycleID = a.order_cycle_id,
                                           OrderCycleStage = a.order_cycle_stage,
                                           DisplayOrder = a.diplay_order,
                                           IsActive = a.is_active,
                                           CreatedBy = a.created_by,
                                           CreatedDate = a.created_date,
                                           UpdateBy = a.updated_by,
                                           UpdateDate = a.updated_date
                                       }).OrderBy(x => x.DisplayOrder).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public orderCycle_Model GetOrderCycleByID(int OrderCycleID)
        {
            orderCycle_Model ObjCycle = new orderCycle_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCycle = (from a in Context.t_order_cylce
                                       where a.order_cycle_id == OrderCycleID
                                       select new orderCycle_Model
                                       {
                                           OrderCycleID = a.order_cycle_id,
                                           OrderCycleStage = a.order_cycle_stage,
                                           DisplayOrder = a.diplay_order,
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

        public int Update(orderCycle_Model ObjOrderCycle)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_order_cylce.FirstOrDefault(x =>( x.order_cycle_stage == ObjOrderCycle.OrderCycleStage  || x.diplay_order == ObjOrderCycle.DisplayOrder) && x.order_cycle_id != ObjOrderCycle.OrderCycleID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_order_cylce.SingleOrDefault(x => x.order_cycle_id == ObjOrderCycle.OrderCycleID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjOrderCycle.OrderCycleStage))
                                IDExist.order_cycle_stage = ObjOrderCycle.OrderCycleStage;
                            if (ObjOrderCycle.IsActive != null)
                                IDExist.is_active = ObjOrderCycle.IsActive;
                            if (ObjOrderCycle.DisplayOrder != null)
                                IDExist.diplay_order = ObjOrderCycle.DisplayOrder;
                            IDExist.updated_by = ObjOrderCycle.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjOrderCycle.OrderCycleID;
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

        public dynamic GetOrderByCustomerID(int CusId)
        {
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var res = (from a in Context.t_order_details
                                       where a.t_orders.customer_id == CusId

                                       select new
                                       {
                                           delivery_date = a.t_orders.delivery_date,
                                           OrderID = a.t_orders.order_id,
                                           order_status = a.t_orders.t_order_cylce.order_cycle_stage,
                                           isActive = a.t_orders.is_active,
                                           DeliveryAddress = a.t_orders.delivery_address,
                                           ordereddate = a.t_orders.order_date,
                                           delivery_time = a.t_orders.delivery_time,
                                           pickup_address = a.t_orders.pickup_address,
                                           pickup_date = a.t_orders.pickup_date,
                                           pickup_time = a.t_orders.pickup_time,
                                           paid_amount = a.t_orders.paid_amount,
                                           payment_Status = a.t_orders.payment_Status,
                                           payment_type = a.t_orders.payment_type,
                                           store_id = a.t_orders.store_id,
                                           product = a.t_product.product_name,
                                           service = a.t_service.service_name,
                                           quantity = a.quantity,
                                           //price = (a.total_price - a.extra_charge_price,)/a.quantity,
                                           subtotal = a.total_price,
                                           extraPrice = a.extra_charge_price,
                                           expectedDeliveryDate = a.t_orders.expected_delivery_date,
                                           balanceAmount = a.t_orders.old_balance,
                                           total = a.t_orders.grandtotal_price,
                                           CustomerRewardPoints = a.t_orders.rewardsapplied

                                       }).OrderByDescending(x => x.OrderID).ToList();

                    var TempOrdersList = res.GroupBy(x => x.OrderID).Select(x => x.FirstOrDefault()).ToList();
                    var OrdersList = TempOrdersList.Select(x => new CustmoerOrders_Model
                    {
                        OrderID = x.OrderID,
                        DeliveryAddress = x.DeliveryAddress,
                        ordereddate = x.ordereddate,
                        order_status = x.order_status,
                        deliverydate = x.delivery_date,
                        expectedDeliveryDate =  x.expectedDeliveryDate,
                        pickup_date = x.pickup_date,
                        pickup_time = x.pickup_time,
                        pickup_address= x.pickup_address,
                        delivery_time = x.delivery_time,
                        paid_amount = x.paid_amount,
                        balanceAmount = x.balanceAmount,
                        payment_Status = x.payment_Status,
                        payment_type = x.payment_type,
                        total = x.total,
                        isActive = x.isActive,
                        CustomerRewardPoints = x.CustomerRewardPoints

                    }).ToList();

                    foreach (var Order in OrdersList)
                    {
                        Order.orderList = res.Where(x => x.OrderID == Order.OrderID).Select(x => new CustmoerOrderDetails_Model
                        {
                            product = x.product,
                            service = x.service,
                            extraPrice = x.extraPrice,
                            quantity = x.quantity,
                            subtotal = x.subtotal 

                        }).ToList();
                    
                    }
                    //Orderlist,
                    // calculate order total


                    List<CustmoerOrders_Model> CusOrders = new List<CustmoerOrders_Model>();
                    CusOrders = OrdersList;
                   
                    return CusOrders;
                }






            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}