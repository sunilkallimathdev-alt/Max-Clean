using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models;
using API.IRepository;

namespace API.Repository
{
    public class OrderCancelRequests_Repository : OrderCancelRequests_IRepository
    {
        public List<OrderCancel_Model> GetOrderCancelList(int id)
        {
             
            List<OrderCancel_Model> ObjOrderCanReq = new List<OrderCancel_Model>();
            try
            {
                 
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    if (id==0) {
                        var Obj = (from a in Context.t_orders
                                   where a.is_cancel != null 
                                   select new OrderCancel_Model
                                   {
                                       store_code = a.t_store.store_code,
                                       order_id = a.order_id,
                                       order_date = a.order_date,
                                       is_cancel = a.is_cancel,
                                       grandtotal_price = a.grandtotal_price
                                   });
                        return ObjOrderCanReq = Obj.ToList().Select(q => new OrderCancel_Model
                        {
                            store_code = q.store_code,
                            order_id = q.order_id,
                            CancelDate = (q.order_date == null) ? "" : q.order_date.Value.ToString("dd-MM-yyyy"),
                            is_cancel = q.is_cancel,
                            grandtotal_price = q.grandtotal_price
                        }).ToList();
                    }
                    else
                    {
                        var Obj = (from a in Context.t_orders
                                   where a.store_id==id && a.is_cancel != null
                                   select new OrderCancel_Model
                                   {
                                       store_code = a.t_store.store_code,
                                       order_id = a.order_id,
                                       order_date = a.order_date,
                                       is_cancel = a.is_cancel,
                                       grandtotal_price = a.grandtotal_price
                                   });

                        return ObjOrderCanReq = Obj.ToList().Select(q => new OrderCancel_Model
                        {
                            store_code = q.store_code,
                            order_id = q.order_id,
                            CancelDate = (q.order_date == null) ? "" : q.order_date.Value.ToString("dd-MM-yyyy"),
                            is_cancel = q.is_cancel,
                            grandtotal_price = q.grandtotal_price
                        }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}