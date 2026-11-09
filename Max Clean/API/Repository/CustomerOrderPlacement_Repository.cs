using API.CommonClasses;
using API.DataAccess;
using API.IRepository;
using API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace API.Repository
{
    public class CustomerOrderPlacement_Repository : ICustomerOrderPlacement_Repository
    {
        public long Add(PurchaseOrder_Model ObjPO)
        {
            DateTime today = DateTime.Now; //For orderdate assignment
            long iRes = 0;
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    var CustomerExist = Context.t_customer.FirstOrDefault(x => x.mobile_no == ObjPO.CustomerMobile);

                    if (CustomerExist != null)
                    {
                        ObjPO.CustomerID = CustomerExist.customer_id;
                    }
                    else
                    {


                        #region t_customer
                        t_customer cst = new t_customer();
                        cst.mobile_no = ObjPO.CustomerMobile;
                        cst.customer_name = ObjPO.CustomerName;
                        cst.email_id = ObjPO.CustomerEmail;
                        cst.address = ObjPO.PickupAddress;
                        cst.deliveryAddress = ObjPO.DeliveryAddress;
                        cst.old_balance = 0;
                        cst.password = CommonClasses.Base64Encode_Decode.base64Encode(ObjPO.CustomerMobile);
                        cst.is_active = true;

                        cst.pickup_address_type = ObjPO.pickup_address_type;
                        cst.delivery_address_type = ObjPO.delivery_address_type;
                        cst.city = ObjPO.City;
                        Context.t_customer.Add(cst);
                        Context.SaveChanges();
                        ObjPO.CustomerID = cst.customer_id;
                        #endregion

                        #region t_reward
                        t_reward objrwd = new t_reward();
                        objrwd.customer_id = ObjPO.CustomerID;
                        objrwd.customer_rewardpoints = 300;
                        objrwd.is_active = true;
                        objrwd.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_reward.Add(objrwd);
                        Context.SaveChanges();
                        #endregion

                        CustomerExist = Context.t_customer.FirstOrDefault(x => x.customer_id == ObjPO.CustomerID);
                    }



                    t_orders obj = new t_orders();
                    obj.customer_id = ObjPO.CustomerID;
                    obj.store_id = ObjPO.StoreID;
                    obj.order_date = CommonClasses.StaticData.GetCurrentDataTime;
                    obj.delivery_date = ObjPO.ActualDeliveryDate;
                    obj.expected_delivery_date = ObjPO.ActualDeliveryDate;
                    obj.pickup_date = ObjPO.PickupDate;
                    obj.delivery_address = ObjPO.DeliveryAddress;
                    obj.pickup_time = ObjPO.PickupTime;
                    obj.is_mobileorder = ObjPO.Is_MobileOrder;
                    obj.timeslab_id = ObjPO.TimeslabId;
                    obj.delivery_time = ObjPO.DeliveryTime;
                    obj.confirmed_by = ObjPO.ConfirmedBy;
                    obj.old_balance = ObjPO.OldBalanceApplied;
                    obj.rewardsapplied = ObjPO.RewardPointsApplied;

                    obj.pickup_address_type = ObjPO.pickup_address_type;
                    obj.delivery_address_type = ObjPO.delivery_address_type;


                    //obj.pickedup_by = ObjPO.PickedbyID;
                    obj.grandtotal_price = ObjPO.GrandTotal;
                    if (ObjPO.PaidAmount == null)
                    {
                        ObjPO.PaidAmount = 0;
                    }
                    obj.paid_amount = ObjPO.PaidAmount;
                    obj.balance_amount = ObjPO.GrandTotal - ObjPO.PaidAmount;
                    //if (ObjPO.PaidAmount < ObjPO.GrandTotal)
                    //    obj.payment_Status = false;
                    //else
                    obj.payment_Status = "pending";
                    obj.order_status = ObjPO.orderstatusID;

                    if (ObjPO.IsReward == true)
                    {
                        obj.rewardsapplied = ObjPO.RewardPointsApplied;
                        if (ObjPO.RewardPointsApplied > 0 && ObjPO.RewardPointsApplied != null)
                        {
                            var custrwd = Context.t_reward.FirstOrDefault(x => x.customer_id == ObjPO.CustomerID);
                            custrwd.customer_rewardpoints -= ObjPO.RewardPointsApplied;
                            custrwd.customer_usedrewards += ObjPO.RewardPointsApplied;
                        }

                    }
                    else
                        obj.rewardsapplied = 0;

                    obj.is_promocode_applied = ObjPO.IsPromocodeApplied;
                    obj.promocode = ObjPO.Promocode;
                    obj.promocode_discount = ObjPO.Promocode_Discount;
                    obj.promocode_amount = ObjPO.Promocode_Amount;
                    obj.delivery_charge = ObjPO.Delivery_charge;
                    obj.pincode = ObjPO.Pincode;
                    obj.is_outlet_order = false;
                    obj.remark = ObjPO.Remark;
                    obj.pickup_address = ObjPO.PickupAddress;
                    obj.payment_type = ObjPO.PaymentType;
                    obj.created_by = ObjPO.CreatedBy;
                    obj.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                    obj.is_express_delivery = ObjPO.IsExpressDelivery;
                    obj.express_delivery_price = ObjPO.ExpressChargeApplied;
                    obj.is_tagged = false;
                    Context.t_orders.Add(obj);
                    Context.SaveChanges();
                    iRes = obj.order_id;

                    CustomerExist.old_balance -= ObjPO.OldBalanceApplied;
                    CustomerExist.address = ObjPO.PickupAddress;
                    CustomerExist.deliveryAddress = ObjPO.DeliveryAddress;
                    CustomerExist.pincode = ObjPO.Pincode;
                    CustomerExist.pickup_address_type = ObjPO.pickup_address_type;
                    CustomerExist.delivery_address_type = ObjPO.delivery_address_type;

                    StringBuilder sb = new StringBuilder();
                    double? GrandTotal = 0;
                    foreach (PurchaseOrderDetail product in ObjPO.PurchaseOrderDetail)
                    {
                        if (product.ExtraPrice == null)
                        {
                            product.ExtraPrice = 0;
                        }
                        var Prod = Context.t_service_price.SingleOrDefault(x => x.product_id == product.ProductID && x.service_id == product.ServiceID);
                        t_order_details Objdetail = new t_order_details();
                        Objdetail.order_id = iRes;
                        Objdetail.service_id = product.ServiceID;
                        Objdetail.product_id = product.ProductID;
                        Objdetail.total_price = (Prod.price * product.Quantity) + product.ExtraPrice;
                        Objdetail.extra_charge_percentage = product.ExtraPercent;
                        Objdetail.extra_charge_price = product.ExtraPrice;
                        Objdetail.quantity = product.Quantity;
                        Objdetail.remaining_quantity = product.Quantity;
                        Objdetail.delivered_quantity = 0;
                        Objdetail.extra_charge_percentage = 0;
                        Objdetail.extra_charge_price = 0;
                        Objdetail.unit = product.Unit;
                        Context.t_order_details.Add(Objdetail);

                        //var Prod = Context.t_service_price.SingleOrDefault(x => x.product_id == product.ProductID && x.service_id == product.ServiceID);
                        sb.Append("<tr>");
                        sb.Append("<td>" + Prod.t_product.product_name + "</td>");

                        sb.Append("<td>" + Objdetail.quantity + "</td>");
                        sb.Append("<td>" + Prod.price + "</td>");
                        sb.Append("<td>" + product.ExtraPercent + "</td>");
                        sb.Append("<td>" + product.ExtraPrice + "</td>");
                        sb.Append("<td>" + ((Prod.price * Objdetail.quantity) + product.ExtraPrice) + "</td>");
                        sb.Append("</tr>");
                        GrandTotal = GrandTotal + (Prod.price * product.Quantity);
                    }
                    Context.SaveChanges();

                    var customerdetails = Context.t_customer.SingleOrDefault(x => x.customer_id == ObjPO.CustomerID);
                    var storedetails = Context.t_store.SingleOrDefault(x => x.store_id == ObjPO.StoreID);
                    var Orderstatus = Context.t_order_cylce.SingleOrDefault(x => x.order_cycle_id == ObjPO.orderstatusID);
                    var orderdetails = Context.t_orders.SingleOrDefault(x => x.order_id == iRes);

                    string HTML = new WebClient().DownloadString("http://maxcleanapi.myprelaunch.in/EmailTemplates/OrderInvoice.html");
                    HTML = HTML.Replace(@"$CUSTOMERNAME$", customerdetails.customer_name);
                    HTML = HTML.Replace(@"$CUSTOMERADDRESS$", customerdetails.address);
                    HTML = HTML.Replace(@"$CUSTOMERMOBILE$", customerdetails.mobile_no);
                    HTML = HTML.Replace(@"$STOREMOBILE$", storedetails.mobile_no);


                    HTML = HTML.Replace(@"$STATUS$", Orderstatus.order_cycle_stage);
                    HTML = HTML.Replace(@"$ORDERDATE$", orderdetails.order_date.ToString());
                    HTML = HTML.Replace(@"$DELIVERYDATE$", orderdetails.delivery_date.ToString("dd/MM/yyyy"));
                    HTML = HTML.Replace(@"$DELIVERYCHARGE$", (orderdetails.delivery_charge != null) ? orderdetails.delivery_charge.ToString() : "0");
                    HTML = HTML.Replace(@"$REWARDS$", (orderdetails.rewardsapplied != null) ? orderdetails.rewardsapplied.ToString() : "0");
                    HTML = HTML.Replace(@"$PROMOCODE$", (orderdetails.promocode_amount != null) ? orderdetails.promocode_amount.ToString() : "0");
                    HTML = HTML.Replace(@"$OlDBALANCE$", orderdetails.old_balance.ToString());
                    HTML = HTML.Replace(@"$PAIDAMOUNT$", orderdetails.paid_amount.ToString());
                    HTML = HTML.Replace(@"$BALANCEAMOUNT$", orderdetails.balance_amount.ToString());
                    var DateTime = new DateTime();
                    DateTime = Convert.ToDateTime(ObjPO.ExpectingDeliveryDate);
                    HTML = HTML.Replace(@"$STATUS$", "<b><label style='color:green'>" + Orderstatus.order_cycle_stage + "</label></b>");
                    // HTML = HTML.Replace(@"$EXPDATE$", "Expecting Delivery On <b>" + DateTime.ToString("dd-MM-yyyy hh:mm:ss tt") + "</b>");
                    HTML = HTML.Replace(@"$REMARK$", ObjPO.Remark);
                    HTML = HTML.Replace(@"$ORDERID$", obj.order_id.ToString());
                    HTML = HTML.Replace(@"$STORENAME$", storedetails.store_name);
                    HTML = HTML.Replace(@"$STOREADDRESS$", storedetails.address);

                    HTML = HTML.Replace(@"$PRODUCTDETAILS$", sb.ToString());
                    HTML = HTML.Replace(@"$GRANDTOTAL$", orderdetails.grandtotal_price.ToString());
                    if (orderdetails.is_express_delivery == true)
                    {
                        HTML = HTML.Replace(@"$EXPRESS$", (ObjPO.ExpressChargeApplied).ToString());
                    }
                    else
                    {
                        HTML = HTML.Replace(@"$EXPRESS$", ("0"));
                    }


                    var SMSbody = "Dear " + customerdetails.customer_name + "," + Environment.NewLine + "Thanks for your order with Company" + Environment.NewLine + "Order No.: " + orderdetails.order_id + Environment.NewLine + "Quantity: " + ObjPO.PurchaseOrderDetail.Sum(x => x.Quantity) + Environment.NewLine + "Total Amount: " + orderdetails.grandtotal_price + " INR" + Environment.NewLine + "Status: Order Confirmed" + Environment.NewLine + "For any queries, please reach out to 9900318000." + Environment.NewLine + "Order online from www.maxcleanlaundry.in " + Environment.NewLine + "Outlet: " + storedetails.store_name + Environment.NewLine + "Open Hours: 09:00 AM to 09:00 PM";
                    //var SMSbody = "Dear " + customerdetails.customer_name + ",Company Order No : " + orderdetails.order_id + " Qty.: " + ObjPO.PurchaseOrderDetail.Sum(x => x.Quantity) + " Amt.   : " + orderdetails.grandtotal_price + ".00 INR Status : Order Confirmed ";

                    StringBuilder baseurl = new StringBuilder();
                    baseurl.Append(EmailSMSCredentials.SMS_URL);
                    baseurl.Append("sender=" + EmailSMSCredentials.SMS_SENDER_NAME);
                    baseurl.Append("&route=" + EmailSMSCredentials.Route);
                    baseurl.Append("&mobiles=" + customerdetails.mobile_no);
                    baseurl.Append("&authkey=" + EmailSMSCredentials.SMS_OauthKey);
                    baseurl.Append("&country=" + EmailSMSCredentials.SMS_Country);
                    baseurl.Append("&message=" + SMSbody);


#if !DEBUG   //This module is costly. So please only use it on production.

                    WebClient client = new WebClient();
                    Stream data = client.OpenRead(baseurl.ToString());
                    StreamReader reader = new StreamReader(data);
                    var Status = reader.ReadToEnd();
                    data.Close();
                    reader.Close();

#endif


                    // MailTo,Subject,MailBody,CC,BCC
                    var Res = MailSender.SendMail(customerdetails.email_id, "Order Invoice", HTML, storedetails.email_id, "bookings@maxcleanlaundry.in");
                }
                if (ObjPO.orderstatusID == 2)
                {
                    PushNotification_Model obj = new PushNotification_Model();
                    obj.Title = "New Order Ready for Pickup";
                    obj.Body = "Order ID : " + iRes;
                    PurchaseOrder_Repository proc = new PurchaseOrder_Repository();
                    proc.pushnotification(obj);
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