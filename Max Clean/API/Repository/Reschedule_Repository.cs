using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Reschedule_Repository : Reschedule_IRepository
    {
        public long Update(PurchaseOrder_Model Obj)
        {
            long iRes = 0;
            DateTime TempFromDate = new DateTime();
            DateTime TempToDate = new DateTime();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_orders.FirstOrDefault(x => x.order_id == Obj.OrderID);
                    if (Exist == null)
                    {
                        iRes = -1;
                    }
                    else
                    {

                        if (Obj.PickupDate != null)
                        {
                            //if (Obj.RescheduledNote != null)
                            //{
                                TempFromDate = Convert.ToDateTime(Exist.pickup_date);
                                TempToDate = Convert.ToDateTime(Obj.PickupDate);
                                Exist.note += " PickUp Rescheduled : " + TempFromDate.Day+"-"+ TempFromDate.Month+ "-"+ TempFromDate.Year+" " + Exist.pickup_time + " to " + TempToDate.Day + "-" + TempToDate.Month + "-" + TempToDate.Year + " " + Obj.PickupTime + " Note : " + Obj.RescheduledNote + "</br>";
                           // }
                            Exist.pickup_date = Obj.PickupDate;
                            var orderexist = Context.t_orders.FirstOrDefault(c => c.order_id==Obj.OrderID);
                            if (orderexist != null)
                            {
                                if (orderexist.order_status > 1)
                                {
                                    orderexist.order_status = 2;
                                    orderexist.pickedup_by = null;
                                }
                            }
                        }
                        if (Obj.ExpectingDeliveryDate != null)
                        {

                            //if (Obj.RescheduledNote != null)
                          //  {
                                TempFromDate = Convert.ToDateTime(Exist.delivery_date);
                                TempToDate = Convert.ToDateTime(Obj.ExpectingDeliveryDate);
                                Exist.note += "Delivery Rescheduled : " + TempFromDate.Day + "-" + TempFromDate.Month + "-" + TempFromDate.Year + " " + Exist.delivery_time + " to " + TempToDate.Day + "-" + TempToDate.Month + "-" + TempToDate.Year + " " + Obj.DeliveryTime + " Note : " + Obj.RescheduledNote+"</br>";
                          //  }
                            Exist.delivery_date = Convert.ToDateTime(Obj.ExpectingDeliveryDate);
                            var orderexist = Context.t_orders.FirstOrDefault(c => c.order_id == Obj.OrderID);
                            if (orderexist != null && orderexist.order_status > 7)
                            {
                                orderexist.order_status = 7;
                                orderexist.delivered_by = null;
                            }
                        }
                        if (Obj.ExpectingDeliveryDate != null)
                            Exist.expected_delivery_date = Obj.ExpectingDeliveryDate;
                       
                        if (Obj.PickupTime != null)
                            Exist.pickup_time = Obj.PickupTime;
                        if (Obj.DeliveryTime != null)
                            Exist.delivery_time = Obj.DeliveryTime;

                        Context.SaveChanges();
                        iRes = Obj.OrderID;

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