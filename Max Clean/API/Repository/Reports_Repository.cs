using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using API.DataAccess;
using System.Web;

namespace API.Repository
{
    public class Reports_Repository : Reports_IRepository
    {

        public dynamic GetReports(PurchaseOrder_Model Objpo) {

            if(Objpo.Reports == 1)
            {
               return GetOrders(Objpo);
            }
            else
            {
                return "";
            }

        }
        
        private List<PurchaseOrder_Model> GetOrders(PurchaseOrder_Model Obj)
        {
            List<PurchaseOrder_Model> ObjRequest = new List<PurchaseOrder_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Object = (from a in Context.t_orders
                                   //where a.is_outlet_order == true
                               select new PurchaseOrder_Model
                               {
                                   OrderID = a.order_id,
                                   CustomerID = a.customer_id,
                                   CustomerName = a.t_customer.customer_name,
                                   CustomerMobile = a.t_customer.mobile_no,
                                   CustomerAddress = a.t_customer.address,
                                   StoreID = a.store_id,
                                   StoreName = a.t_store.store_name,
                                   Store_code = a.t_store.store_code,
                                   StoreMobileNo = a.t_store.mobile_no,
                                   StoreAddress = a.t_store.address,
                                   ConfirmedBy = a.confirmed_by,
                                   ConfirmedUser = a.t_user.user_name,
                                   PickedbyID = a.pickedup_by,
                                   Pickedby = a.t_delivery_boy.name,
                                   DeliveredbyID = a.delivered_by,
                                   DeliveryboyName = (a.delivered_by != null) ? Context.t_delivery_boy.FirstOrDefault(p => p.id == a.delivered_by).name : null,
                                   OrderDate = a.order_date,
                                   ExpectingDeliveryDate = a.expected_delivery_date,
                                   ActualDeliveryDate = a.delivery_date,
                                   DeliveryAddress = a.delivery_address,
                                   PickupTime = a.pickup_time,
                                   DeliveryTime = a.delivery_time,
                                   PaidAmount = a.paid_amount,
                                   RemainingAmount = a.balance_amount,
                                   GrandTotal = a.grandtotal_price,
                                   TotalQuantity = (a.order_id != 0) ? Context.t_order_details.Where(p => p.order_id == a.order_id).Sum(c => c.quantity) : 0,
                                   orderstatusID = a.order_status,
                                   OrderStatus = a.t_order_cylce.order_cycle_stage

                               });
                    if (Obj.FromDate != null)
                    {
                        Object = Object.Where(a => a.ExpectingDeliveryDate >= Obj.FromDate).AsQueryable();
                    }
                    if (Obj.ToDate != null)
                    {
                        Object = Object.Where(a => a.ExpectingDeliveryDate <= Obj.ToDate).AsQueryable();
                    }
                    if (Obj.orderstatusID!= null)
                    {
                        Object = Object.Where(a => a.orderstatusID == Obj.orderstatusID).AsQueryable();
                    }
                    if (Obj.StoreID != null && Obj.StoreID != 0)
                    {
                        Object = Object.Where(a => a.StoreID == Obj.StoreID).AsQueryable();
                    }
                    var list = Object.OrderByDescending(a => a.ExpectingDeliveryDate).ToList();
                    return ObjRequest = list;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Inventory_Model> GetInventory(int id)
        {
            List<Inventory_Model> objlist = new List<Inventory_Model>();
            try
            {
                using(maxcleanEntities Context = new maxcleanEntities())
                {
                    var  ObjProduct = (from a in Context.t_product

                                         where a.is_active == true
                                         select new Inventory_Model
                                         {
                                             CategoryID = a.category_id,
                                             CategoryName = a.t_category.category_name,

                                             ProductID = a.product_id,
                                           //  Unit = a.unit,
                                             ProductName = a.product_name,
                                             

                                           

                                         }).ToList();
                    foreach (var obj in ObjProduct)
                    {

                        obj.Quantity = Context.t_order_details.Where(x => x.t_orders.order_status > 4 && x.t_orders.order_status < 10 && x.product_id == obj.ProductID && x.t_orders.store_id==id).Sum(x => x.quantity);
                    }

                    objlist = ObjProduct;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return objlist;
        }

        public List<Inventory_Model> GetInventoryManagement(int id1, int id2)
        {
           
            List<Inventory_Model> objlist = new List<Inventory_Model>();
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    var ObjProduct = (from a in Context.t_product

                                      where a.is_active == true
                                      select new Inventory_Model
                                      {
                                          CategoryID = a.category_id,
                                          CategoryName = a.t_category.category_name,
                                          ProductID = a.product_id,
                                          // Unit = a.unit,
                                          ProductName = a.product_name,
                                      }).ToList();
                    foreach (var obj in ObjProduct)
                    {
                        if (id1 != 0)                    //here id1 = storeid ; id2 = orderstatusid ani ardham
                        {
                            obj.Quantity = Context.t_order_details.Where(x => x.t_orders.order_status == id2 && x.product_id == obj.ProductID && x.t_orders.store_id == id1).Sum(x => x.quantity);
                        }
                        else
                        {
                            obj.Quantity = Context.t_order_details.Where(x => x.t_orders.order_status == id2 && x.product_id == obj.ProductID).Sum(x => x.quantity);
                        }
                    }

                    objlist = ObjProduct;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objlist;
        }
    }
}