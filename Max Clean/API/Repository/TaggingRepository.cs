using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using API.CommonClasses;
using API.DataAccess;
using API.IRepository;
using API.Models;
using System.Linq.Dynamic;

namespace API.Repository
{
    public class TaggingRepository : Tag_IRepository
    {
        public long UpdateTag(Tag_Model obj)
        {
            long IRes = 0;
            try
            {
                using (maxcleanEntities context = new maxcleanEntities())
                {

                    var TagExist = context.t_taggs.FirstOrDefault(x => x.tag_id == obj.Tag_ID);
                    if (TagExist == null)
                    {
                        IRes = -1;
                    }
                    else
                    {

                        TagExist.tag_note = obj.Tag_Note;
                        IRes = obj.Tag_ID;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IRes;
        }

        public Base_Model GetAllTaggedOrders(string length, string start, string sortColumn, string sortColumnDir, string draw, string search)
        {
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            Base_Model obj = new Base_Model();
            try
            {
                using (maxcleanEntities context = new maxcleanEntities())
                {
                    // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
                    context.Configuration.LazyLoadingEnabled = false;

                    var tags = (from a in context.t_taggs
                                select new Tag_Model
                                {
                                    Tag_ID = a.tag_id,
                                    Tag_Number = a.tag_number,
                                    TaggedDate = a.created_date,
                                    Order_ID=a.order_id,
                                    Sent_to_factory = a.sent_to_factory_on,
                                    Received_from_factory=a.received_from_factory,
                                    Tag_Note= a.tag_note,
                                    Order_Status = a.t_orders.t_order_cylce.order_cycle_stage,
                                    DeliveryDate = a.t_orders.expected_delivery_date
                                }).OrderByDescending(x=>x.Tag_ID).ToList();

                    // Verification.   
                    if (!string.IsNullOrEmpty(search) &&
                        !string.IsNullOrWhiteSpace(search) && (search != "$0$"))
                    {
                        // Apply search   
                        tags = tags.Where(
                         p => p.Tag_ID.ToString().Contains(search) ||
                        p.Tag_Number.ToString().Contains(search) ||
                         p.Order_ID.ToString().Contains(search) ||
                          p.TaggedDate.ToString().Contains(search) ||
                           p.Order_Status.Replace(" ", "").ToLower().Trim().Contains(search.ToLower())

                         ).ToList();
                    }


                    ////SORT
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        tags = tags.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                    }

                    recordsTotal = tags.Count();
                    var data = tags.Skip(skip).Take(pageSize).ToList();
                    Base_Model objbase = new Base_Model();
                    objbase.draw = draw;
                    objbase.recordsTotal = recordsTotal;
                    objbase.data = data;

                    return objbase;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tag_Model Get_Tag_ById(long id)
        {
            Tag_Model tag = new Tag_Model();
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    return tag = (from a in Context.t_taggs
                                  where a.tag_id == id
                                  select new Tag_Model
                                  {
                                      Tag_ID = a.tag_id,
                                      Tag_Number = a.tag_number,
                                      TaggedDate = a.created_date,
                                      Order_ID = a.order_id,
                                      Sent_to_factory = a.sent_to_factory_on,
                                      Received_from_factory = a.received_from_factory,
                                      Tag_Note = a.tag_note,
                                      Order_Status = a.t_orders.t_order_cylce.order_cycle_stage,

                                  }).SingleOrDefault();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public long ChangeOrders_Status(OrderList_Model obj)
        {
            PurchaseOrder_Repository purchase = new PurchaseOrder_Repository();
            long Res = 0;
            try {
                using (maxcleanEntities context = new maxcleanEntities()) {
                    foreach (var p in obj.Orders)
                    {
                        var OrderIDExist = context.t_orders.FirstOrDefault(x=>x.order_id== p.OrderID);
                        if (OrderIDExist == null)
                        {
                            Res = -1;
                        }
                        else
                        {
                            if (OrderIDExist.order_status > obj.Order_Status)
                            {
                                Res = -11;
                            }
                            else
                            {
                                Res = OrderIDExist.order_id;
                                if (obj.Order_Status == 2)
                                {
                                    OrderIDExist.order_status = obj.Order_Status;
                                    PushNotification_Model objp = new PushNotification_Model();
                                    objp.Title = "New Order Ready for Pickup";
                                    objp.Body = "Order ID : " + OrderIDExist.order_id;
                                    purchase.pushnotification(objp);
                                }
                                else if (obj.Order_Status == 3)
                                {
                                    Res = -11;
                                }
                                else if (obj.Order_Status == 4)
                                {
                                    Res = -11;
                                }
                                else if (obj.Order_Status == 5)
                                {
                                    if (OrderIDExist.order_status == 2 || OrderIDExist.order_status == 3)
                                    {
                                        Res = -11;
                                    }
                                    else
                                    {
                                        OrderIDExist.order_status = obj.Order_Status;
                                        OrderIDExist.is_tagged = true;
                                        var tag_exist = context.t_taggs.FirstOrDefault(x => x.order_id == p.OrderID);
                                        int? total_quant = OrderIDExist.t_order_details.Where(x => x.order_id == p.OrderID).Sum(c => c.quantity);
                                        if (tag_exist == null)
                                        {
                                            t_taggs tag = new t_taggs();
                                            tag.order_id = p.OrderID;
                                            tag.tag_number = p.OrderID + "/" + total_quant;
                                            tag.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                                            context.t_taggs.Add(tag);
                                        }
                                    }
                                }
                                else if (obj.Order_Status == 6)
                                {
                                    if (OrderIDExist.is_tagged == true && OrderIDExist.order_status == 5)
                                    {
                                        OrderIDExist.order_status = obj.Order_Status;
                                        var tag_exist = context.t_taggs.FirstOrDefault(x => x.order_id == p.OrderID);
                                        if (tag_exist != null)
                                        {
                                            tag_exist.sent_to_factory_on = CommonClasses.StaticData.GetCurrentDataTime;
                                        }
                                    }
                                    else
                                    {
                                        Res = -10;
                                    }

                                }
                                else if (obj.Order_Status == 7)
                                {
                                    if (OrderIDExist.order_status == 6)
                                    {
                                        OrderIDExist.order_status = obj.Order_Status;
                                        var tag_exist = context.t_taggs.FirstOrDefault(x => x.order_id == p.OrderID);
                                        if (tag_exist != null)
                                        {
                                            tag_exist.received_from_factory = CommonClasses.StaticData.GetCurrentDataTime;
                                        }
                                        OrderIDExist.order_status = 8;
                                        var customerdetails = context.t_customer.SingleOrDefault(x => x.customer_id == OrderIDExist.customer_id);
                                        var storedetails = context.t_store.SingleOrDefault(x => x.store_id == OrderIDExist.store_id);

                                        var orderdetails = context.t_orders.SingleOrDefault(x => x.order_id == p.OrderID);
                                        //var SMSbody = "Dear " + customerdetails.customer_name + ",Thanks for your order with Company Order No : " + orderdetails.order_id + " Quantity : " + orderdetails.t_order_details.Sum(x => x.quantity) + " Amount : " + orderdetails.grandtotal_price + ".00 INR Status : Order Confirmed For any queries, please reach out to 9900318000. Order online from www.maxclean.com Outlet: " + storedetails.store_name + " Open Hours : 09:00 AM to 09:00 PM";
                                        //var SMSbody = "Dear " + customerdetails.customer_name + ",Company Order No : " + orderdetails.order_id + " Qty.: " + ObjPO.PurchaseOrderDetail.Sum(x => x.Quantity) + " Amt.   : " + orderdetails.grandtotal_price + ".00 INR Status : Order Confirmed ";
                                        var SMSbody = "Dear " + customerdetails.customer_name + "," + Environment.NewLine + "Order No.: " + orderdetails.order_id + Environment.NewLine + "Quantity: " + orderdetails.t_order_details.Sum(x => x.quantity) + Environment.NewLine + "Total Amount: " + orderdetails.grandtotal_price + " INR" + Environment.NewLine + "Paying Amt: " + orderdetails.balance_amount + " INR" + Environment.NewLine + "Status: Order Ready For Delivery" + Environment.NewLine + "For any queries, please reach out to 9900318000." + Environment.NewLine + "Order online from www.maxcleanlaundry.in" + Environment.NewLine + "Outlet: " + storedetails.store_name + Environment.NewLine + "Open Hours: 09:00 AM to 09:00 PM";
                                        StringBuilder baseurl = new StringBuilder();
                                        baseurl.Append(EmailSMSCredentials.SMS_URL);
                                        baseurl.Append("sender=" + EmailSMSCredentials.SMS_SENDER_NAME);
                                        baseurl.Append("&route=" + EmailSMSCredentials.Route);
                                        baseurl.Append("&mobiles=" + customerdetails.mobile_no);
                                        baseurl.Append("&authkey=" + EmailSMSCredentials.SMS_OauthKey);
                                        baseurl.Append("&country=" + EmailSMSCredentials.SMS_Country);
                                        baseurl.Append("&message=" + SMSbody);

                                        WebClient client = new WebClient();
                                        Stream data = client.OpenRead(baseurl.ToString());
                                        StreamReader reader = new StreamReader(data);
                                        var Status = reader.ReadToEnd();
                                        data.Close();
                                        reader.Close();
                                    }
                                    else
                                    {
                                        Res = -11;
                                    }

                                }
                                else if (obj.Order_Status == 8)
                                {
                                    if (OrderIDExist.order_status == 7)
                                    {
                                        OrderIDExist.order_status = obj.Order_Status;
                                        var customerdetails = context.t_customer.SingleOrDefault(x => x.customer_id == OrderIDExist.customer_id);
                                        var storedetails = context.t_store.SingleOrDefault(x => x.store_id == OrderIDExist.store_id);

                                        var orderdetails = context.t_orders.SingleOrDefault(x => x.order_id == p.OrderID);
                                        //var SMSbody = "Dear " + customerdetails.customer_name + ",Thanks for your order with Company Order No : " + orderdetails.order_id + " Quantity : " + orderdetails.t_order_details.Sum(x => x.quantity) + " Amount : " + orderdetails.grandtotal_price + ".00 INR Status : Order Confirmed For any queries, please reach out to 9900318000. Order online from www.maxclean.com Outlet: " + storedetails.store_name + " Open Hours : 09:00 AM to 09:00 PM";
                                        //var SMSbody = "Dear " + customerdetails.customer_name + ",Company Order No : " + orderdetails.order_id + " Qty.: " + ObjPO.PurchaseOrderDetail.Sum(x => x.Quantity) + " Amt.   : " + orderdetails.grandtotal_price + ".00 INR Status : Order Confirmed ";
                                        var SMSbody = "Dear " + customerdetails.customer_name + "," + Environment.NewLine + "Order No.: " + orderdetails.order_id + Environment.NewLine + "Quantity: " + orderdetails.t_order_details.Sum(x => x.quantity) + Environment.NewLine + "Total Amount: " + orderdetails.grandtotal_price + " INR" + Environment.NewLine + "Paying Amt: " + orderdetails.balance_amount + " INR" + Environment.NewLine + "Status: Order Ready For Delivery" + Environment.NewLine + "For any queries, please reach out to 9900318000." + Environment.NewLine + "Order online from www.maxcleanlaundry.in" + Environment.NewLine + "Outlet: " + storedetails.store_name + Environment.NewLine + "Open Hours: 09:00 AM to 09:00 PM";
                                        StringBuilder baseurl = new StringBuilder();
                                        baseurl.Append(EmailSMSCredentials.SMS_URL);
                                        baseurl.Append("sender=" + EmailSMSCredentials.SMS_SENDER_NAME);
                                        baseurl.Append("&route=" + EmailSMSCredentials.Route);
                                        baseurl.Append("&mobiles=" + customerdetails.mobile_no);
                                        baseurl.Append("&authkey=" + EmailSMSCredentials.SMS_OauthKey);
                                        baseurl.Append("&country=" + EmailSMSCredentials.SMS_Country);
                                        baseurl.Append("&message=" + SMSbody);

                                        WebClient client = new WebClient();
                                        Stream data = client.OpenRead(baseurl.ToString());
                                        StreamReader reader = new StreamReader(data);
                                        var Status = reader.ReadToEnd();
                                        data.Close();
                                        reader.Close();
                                    }
                                    else
                                    {
                                        Res = -11;
                                    }
                                }
                                else if (obj.Order_Status == 9)
                                {

                                    Res = -11;

                                }
                                else if (obj.Order_Status == 10)
                                {

                                    Res = -11;

                                }
                                else if (obj.Order_Status == 11)
                                {
                                    if (OrderIDExist.order_status > 6)
                                    {
                                        double? paidPercent = (OrderIDExist.paid_amount / OrderIDExist.grandtotal_price) * 100;
                                        if (paidPercent >= 80)
                                        {
                                            OrderIDExist.order_status = obj.Order_Status;
                                            OrderIDExist.delivery_date = CommonClasses.StaticData.GetCurrentDataTime;
                                            purchase.updateRewards(OrderIDExist.customer_id, OrderIDExist.paid_amount);
                                            var custexist = context.t_customer.FirstOrDefault(x => x.customer_id == OrderIDExist.customer_id);
                                            if (custexist != null)
                                            {
                                                custexist.old_balance += (OrderIDExist.grandtotal_price - OrderIDExist.paid_amount);
                                            }
                                        }
                                        else
                                        {
                                            Res = -9;
                                        }
                                    }
                                    else
                                    {
                                        Res = -11;
                                    }
                                }
                                else
                                {
                                    OrderIDExist.order_status = obj.Order_Status;
                                }
                                
                                if (obj.Order_Status == 8)
                                {
                                    if (OrderIDExist.order_status == 7)
                                    {
                                        OrderIDExist.order_status = obj.Order_Status;
                                        PushNotification_Model objp = new PushNotification_Model();
                                        objp.Title = "New Order Ready for Delivery";
                                        objp.Body = "Order ID : " + OrderIDExist.order_id;
                                        purchase.pushnotification(objp);
                                    }
                                }


                                //Res = OrderIDExist.order_id;
                                context.SaveChanges();
                            }
                        }

                    }
                }
                return Res;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<PurchaseOrder_Model> GetOrders_buStatusID(int id)
        {
            List<PurchaseOrder_Model> orderlist = new List<PurchaseOrder_Model>();
            try
            {
                using(maxcleanEntities context = new maxcleanEntities())
                {
                    return orderlist = (from a in context.t_orders
                                        where a.order_status == id
                                        select new PurchaseOrder_Model
                                        {
                                            OrderID = a.order_id,
                                            OrderDate = a.order_date,
                                            CustomerName = a.t_customer.customer_name,
                                            CustomerMobile=a.t_customer.mobile_no
                                        }).ToList();
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}