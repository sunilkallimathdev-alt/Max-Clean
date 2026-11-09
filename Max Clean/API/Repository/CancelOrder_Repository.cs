using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using API.DataAccess;
using API.IRepository;
using API.Models;

namespace API.Repository
{
    public class CancelOrder_Repository : CancelOrder_IRepositiry
    {
        public int PUT([FromBody] PurchaseOrder_Model Order)
        {

            int iRes = 0;
            try
            {
                if (Order.OrderID != 0)
                {
                    //managers requsting to cancel
                    using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                    {
                        var Exist = Context.t_orders.FirstOrDefault(x => x.order_id == Order.OrderID && x.is_cancel == null);
                        if (Exist == null)
                        {
                            iRes = -1;
                        }
                        else
                        {
                            Exist.is_cancel = 1;
                            Exist.reason_order_cancel = Order.reason_order_cancel;
                            Context.SaveChanges();
                            iRes =(int)Exist.is_cancel;
                        }
                    }
                    return iRes;
                }
                else
                {
                    //Admin cancel order or reject to cancel order
                    using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                    {
                        var Exist = Context.t_orders.FirstOrDefault(x => x.order_id == Order.order_id && x.is_cancel == 1 );
                        if (Exist == null)
                        {
                            iRes = -1;
                        }
                        else
                        {
                            Exist.is_cancel = Order.is_cancel;                          
                            Context.SaveChanges();
                            iRes = (int)Exist.is_cancel;
                        }
                    }
                    return iRes;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<OrderCancel_Model> GetAllOrderCancelRequest(int id)
        {

            List<OrderCancel_Model> ObjOrderCanlist = new List<OrderCancel_Model>();
            try
            {

                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    if (id == 0)
                    {
                        var res = (from a in Context.t_orders
                                   where a.is_cancel != null
                                   select new OrderCancel_Model
                                   {
                                       store_code = a.t_store.store_code,
                                       order_id = a.order_id,
                                       order_date = a.order_date,
                                       is_cancel = a.is_cancel,
                                       grandtotal_price=a.grandtotal_price

                                   }).ToList();

                        return ObjOrderCanlist = res.ToList().Select(q => new OrderCancel_Model

                        {
                            store_code = q.store_code,
                            order_id = q.order_id,
                            CancelDate = (q.order_date == null) ? "" : q.order_date.Value.ToString("dd-MM-yyyy"),
                            is_cancel = q.is_cancel,
                            grandtotal_price = q.grandtotal_price
                        }).OrderByDescending(x => x.order_id).ToList();
                    }
                    else
                    {
                        var res = (from a in Context.t_orders
                                   where a.is_cancel != null && a.store_id==id                                 
                                   select new OrderCancel_Model
                                   {
                                       store_code = a.t_store.store_code,
                                       order_id = a.order_id,
                                       order_date = a.order_date,
                                       is_cancel = a.is_cancel,
                                       grandtotal_price = a.grandtotal_price
                                   }).ToList();

                        return ObjOrderCanlist = res.ToList().Select(q => new OrderCancel_Model

                        {
                            store_code = q.store_code,
                            order_id = q.order_id,
                            CancelDate = (q.order_date == null) ? "" : q.order_date.Value.ToString("dd-MM-yyyy"),
                            is_cancel = q.is_cancel,
                            grandtotal_price = q.grandtotal_price
                        }).OrderByDescending(x => x.order_id).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Base_Model GetOrdersCancelList_BystoreID(string length, string start, string sortColumn, string sortColumnDir, string draw, string search, long id, string FromDate, string ToDate)
        {
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            DateTime? fromDate;
            DateTime? toDate;
            if (FromDate != "abc")
            {
                fromDate = Convert.ToDateTime(FromDate);
            }
            else
            {
                fromDate = null;
            }
            if (ToDate != "abc")
            {
                toDate = Convert.ToDateTime(ToDate);
            }
            else
            {
                toDate = null;
            }

            List<OrderCancel_Model> ObjordersList = new List<OrderCancel_Model>();
            using (API.DataAccess.maxcleanEntities Context = new API.DataAccess.maxcleanEntities())
            {
                // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
                Context.Configuration.LazyLoadingEnabled = false;

                var Objorders = (from a in Context.t_orders
                                 select new OrderCancel_Model
                                 {
                                     store_code=a.t_store.store_code,
                                     order_id = a.order_id,
                                     order_date = a.order_date,
                                     is_cancel = a.is_cancel,
                                     grandtotal_price=a.grandtotal_price
                                 });


                if (fromDate != null)
                {
                    Objorders = Objorders.Where(a => a.order_date >= fromDate).AsQueryable();
                }
                if (toDate != null)
                {
                    Objorders = Objorders.Where(a => a.order_date <= toDate).AsQueryable();
                }
                if (id != 0)
                {
                    Objorders = Objorders.Where(a => a.store_id == id).AsQueryable();
                }
                ObjordersList = Objorders.OrderByDescending(x => x.order_id).ToList();

                // Verification.   
                if (!string.IsNullOrEmpty(search) &&
                    !string.IsNullOrWhiteSpace(search) && (search != "$0$"))
                {
                    // Apply search   
                    ObjordersList = ObjordersList.Where(
                     p => p.order_id.ToString().Contains(search)
                     ).ToList();
                }


                ////SORT
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                //{
                //    ObjordersList = ObjordersList.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                //}

                recordsTotal = ObjordersList.Count();
                var data = ObjordersList.Skip(skip).Take(pageSize).ToList();
                Base_Model obj = new Base_Model();
                obj.draw = draw;
                obj.recordsTotal = recordsTotal;
                obj.data = data;

                return obj;
            }
        }
    }
}