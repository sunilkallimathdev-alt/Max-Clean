using ADMIN.API_Repository;
using API.Models;
using API.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class OrderDetailsController : Controller
    {
        // GET: OrderDetails
        public ActionResult Index()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["production"]))
            {
                if (Session["logindetails"] == null)
                    Response.Redirect("/AdminLogin/Index");
            }
            return View();
        }

        public ActionResult EditOrders()
        {
            API_OrderCycleStage obj = new API_OrderCycleStage();
            ViewBag.OrderStatusList = obj.GetActiveOrderCycleList();

            API_DeliveryBoy objDeliveryBoy = new API_DeliveryBoy();
            ViewBag.DeliveryBoyList = objDeliveryBoy.GetActiveDeliveryBoyList();

            API_Category Objcat = new API_Category();
            ViewBag.CategoryList = Objcat.GetActiveCategory();


            API_Service objsrc = new API_Service();
            ViewBag.ServiceList = objsrc.GetServiceList();
            return View();
        }

        public ActionResult editOrder()
        {
            if (Session["logindetails"] == null)
            {
                Response.Redirect("/AdminLogin/Index");
            }
            API_OrderCycleStage obj = new API_OrderCycleStage();
            ViewBag.OrderStatusList = obj.GetActiveOrderCycleList();

            API_DeliveryBoy objDeliveryBoy = new API_DeliveryBoy();
            ViewBag.DeliveryBoyList = objDeliveryBoy.GetActiveDeliveryBoyList();

            API_Category Objcat = new API_Category();
            ViewBag.CategoryList = Objcat.GetActiveCategory();


            API_Service objsrc = new API_Service();
            ViewBag.ServiceList = objsrc.GetServiceList();
            return View();
        }

        public dynamic GetOrdersById(int ID)
        {
            try
            {
                API_PurchaseOrder Objcategory = new API_PurchaseOrder();
                return JsonConvert.SerializeObject(Objcategory.GetOrderByID(ID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic Updateorder(string ObjPO)
        {
            try
            {
                API_PurchaseOrder obj = new API_PurchaseOrder();
                return JsonConvert.SerializeObject(obj.UpdateOrder(ObjPO));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetCategoryByServiceID(int ServiceID)
        {
            API_Category obj = new API_Category();
            return JsonConvert.SerializeObject(obj.GetCategoryByServiceID(ServiceID));
        }

        public dynamic GetServicePrice()
        {
            API_Product Objpro = new API_Product();
            return Objpro.GetServicePrice();
        }

        public dynamic remove_dlvcharges(int orderID)
        {
            if (Session["logindetails"] == null)
            {
                Response.Redirect("/AdminLogin/Index");
            }
            var UserDetails = (JObject)JsonConvert.DeserializeObject(Session["logindetails"].ToString());
            AdminUser_Model user = UserDetails.ToObject<AdminUser_Model>();

            int res = 0;
            try
            {
                using (API.DataAccess.maxcleanEntities context = new API.DataAccess.maxcleanEntities())
                {
                    var orderexist = context.t_orders.FirstOrDefault(c => c.order_id == orderID);
                    if (orderexist != null)
                    {
                        if (orderexist.delivery_charge > 0)
                        {
                            List<PurchaseOrderDetail> orderdetails = (from a in context.t_order_details
                                                                      where a.order_id == orderID
                                                                      select new PurchaseOrderDetail
                                                                      {
                                                                          OrderDetailsID = a.order_details_id,
                                                                          OrderID = a.order_id,
                                                                          ProductID = a.product_id,
                                                                          Price = a.total_price,

                                                                      }).ToList();
                            double? carttotal = 0;
                            double? grandtotal = 0;
                            foreach (var prod in orderdetails)
                            {
                                carttotal += prod.Price;
                            }
                            grandtotal += carttotal;
                            grandtotal += orderexist.old_balance;
                            grandtotal += orderexist.express_delivery_price;
                            if (orderexist.is_promocode_applied == true)
                            {
                                grandtotal -= orderexist.promocode_amount;
                            }
                            //newly added
                            if (orderexist.promocode.TrimStart().ToLower().StartsWith("special discount"))
                            {
                                grandtotal -= orderexist.promocode_amount;
                            }
                            //newly added
                            if (orderexist.rewardsapplied > 0)
                            {
                                grandtotal -= orderexist.rewardsapplied;
                            }
                            orderexist.grandtotal_price = grandtotal;
                            orderexist.balance_amount = grandtotal - orderexist.paid_amount;
                            orderexist.delivery_charge = 0;
                            orderexist.updated_by = user.UserID;
                            orderexist.updated_date = API.CommonClasses.StaticData.GetCurrentDataTime;
                            res = orderID;
                        }
                        context.SaveChanges();
                    }
                    else
                    {
                        res = -1;
                    }
                }
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public long ApplyDiscount(string order)
        {
            long res = 0;
            try
            {
                if (Session["logindetails"] == null)
                {
                    Response.Redirect("/AdminLogin/Index");
                }

                var Order_ = (JObject)JsonConvert.DeserializeObject(order);
                PurchaseOrder_Model Order = Order_.ToObject<PurchaseOrder_Model>();

                using (API.DataAccess.maxcleanEntities context = new API.DataAccess.maxcleanEntities())
                {
                    var orderexist = context.t_orders.FirstOrDefault(c => c.order_id == Order.OrderID);
                    if (orderexist != null)
                    {
                        List<PurchaseOrderDetail> orderdetails = (from a in context.t_order_details
                                                                  where a.order_id == Order.OrderID
                                                                  select new PurchaseOrderDetail
                                                                  {
                                                                      OrderDetailsID = a.order_details_id,
                                                                      OrderID = a.order_id,
                                                                      ProductID = a.product_id,
                                                                      Price = a.total_price,
                                                                  }).ToList();
                        double? carttotal = 0;
                        double? grandtotal = 0;
                        foreach (var prod in orderdetails)
                        {
                            carttotal += prod.Price;
                        }
                        grandtotal += carttotal;
                        grandtotal += orderexist.old_balance;
                        grandtotal += orderexist.express_delivery_price;
                        grandtotal += orderexist.delivery_charge;
                        grandtotal -= Order.Promocode_Amount;
                        grandtotal -= orderexist.promocode_amount;


                        if (orderexist.rewardsapplied > 0)
                        {
                            grandtotal -= orderexist.rewardsapplied;
                        }
                        orderexist.promocode_amount += Order.Promocode_Amount;
                        orderexist.promocode += " Special Discount " + Order.Promocode_Amount;
                        orderexist.grandtotal_price = grandtotal;
                        orderexist.balance_amount = grandtotal - orderexist.paid_amount;

                        res = Order.OrderID;

                        context.SaveChanges();
                    }
                    else
                    {
                        res = -1;
                    }
                }
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Send_Notification(int orderID)
        {
            try
            {
                using (API.DataAccess.maxcleanEntities context = new API.DataAccess.maxcleanEntities())
                {
                    var orderexist = context.t_orders.FirstOrDefault(c => c.order_id == orderID);
                    if (orderexist != null)
                    {
                        if (orderexist.order_status < 3)
                        {
                            PushNotification_Model objp = new PushNotification_Model();
                            objp.Title = "New Order Ready for Pickup";
                            objp.Body = "Order ID : " + orderID;

                            WebClient client = new WebClient();
                            client.Headers[HttpRequestHeader.Accept] = "application/json";
                            client.Headers[HttpRequestHeader.ContentType] = "application/json";
                            var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "PushNotification", "POST", JsonConvert.SerializeObject(objp)));
                        }
                        else if (orderexist.order_status > 6 && orderexist.order_status < 9)
                        {
                            PushNotification_Model objp = new PushNotification_Model();
                            objp.Title = "New Order Ready for Delivery";
                            objp.Body = "Order ID : " + orderID;

                            WebClient client = new WebClient();
                            client.Headers[HttpRequestHeader.Accept] = "application/json";
                            client.Headers[HttpRequestHeader.ContentType] = "application/json";
                            var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "PushNotification", "POST", JsonConvert.SerializeObject(objp)));
                        }



                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public dynamic CancelOrder(string order)
        {
            try
            {
                API_CancelOrder obj = new API_CancelOrder();
                return JsonConvert.SerializeObject(obj.UpdateOrder(order));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
