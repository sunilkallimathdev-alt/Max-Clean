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
using System.Data.Entity;
using System.Transactions;
using System.Linq.Dynamic;

namespace API.Repository
{
    public class PurchaseOrder_Repository : PurchaseOrder_IRepository
    {
        public long Add(PurchaseOrder_Model ObjPO)
        {

            long iRes = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    try
                    {
                        var CustomerExist = Context.t_customer.FirstOrDefault(x => x.mobile_no == ObjPO.CustomerMobile);

                        if (CustomerExist != null)
                        {
                            ObjPO.CustomerID = CustomerExist.customer_id;
                            CustomerExist.address = ObjPO.PickupAddress;
                            CustomerExist.deliveryAddress = ObjPO.DeliveryAddress;
                            CustomerExist.pincode = ObjPO.Pincode;
                            CustomerExist.pickup_address_type = ObjPO.pickup_address_type;
                            CustomerExist.delivery_address_type = ObjPO.delivery_address_type;
                        }
                        else
                        {
                            #region t_customer
                            t_customer cst = new t_customer();
                            cst.mobile_no = ObjPO.CustomerMobile;
                            cst.customer_name = ObjPO.CustomerName;
                            cst.email_id = ObjPO.CustomerEmail;
                            cst.address = ObjPO.PickupAddress;
                            cst.old_balance = 0;
                            cst.password = CommonClasses.Base64Encode_Decode.base64Encode(ObjPO.CustomerMobile);
                            cst.is_active = true;
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
                        }
                        if (CustomerExist != null)
                            CustomerExist.old_balance -= ObjPO.OldBalanceApplied;

                        #region  t_orders
                        t_orders obj = new t_orders();
                        obj.customer_id = ObjPO.CustomerID;
                        obj.store_id = ObjPO.StoreID;
                        obj.order_date = CommonClasses.StaticData.GetCurrentDataTime;
                        obj.delivery_date = ObjPO.ActualDeliveryDate;
                        obj.expected_delivery_date = ObjPO.ActualDeliveryDate;
                        obj.pickup_date = ObjPO.PickupDate;
                        obj.delivery_address = ObjPO.DeliveryAddress;
                        obj.pickup_time = ObjPO.PickupTime;
                        obj.delivery_time = ObjPO.DeliveryTime;
                        obj.confirmed_by = ObjPO.ConfirmedBy;
                        //obj.pickedup_by = ObjPO.PickedbyID;
                        obj.grandtotal_price = ObjPO.GrandTotal;
                        if (ObjPO.PaidAmount == null)
                        {
                            ObjPO.PaidAmount = 0;
                        }
                        obj.paid_amount = ObjPO.PaidAmount;
                        obj.advance_amount = ObjPO.PaidAmount;
                        obj.old_balance = ObjPO.OldBalanceApplied;
                        obj.balance_amount = ObjPO.RemainingAmount;
                        if (ObjPO.PaidAmount == 0)
                        {
                            obj.payment_Status = "pending";
                        }
                        else if (ObjPO.PaidAmount < ObjPO.GrandTotal)
                        {
                            obj.payment_Status = "partial";
                        }
                        else
                        {
                            obj.payment_Status = "complete";
                        }
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

                        obj.is_outlet_order = ObjPO.IsOutletOrder;
                        obj.remark = ObjPO.Remark;
                        obj.pickup_address = ObjPO.PickupAddress;
                        obj.payment_type = ObjPO.PaymentType;

                        obj.Invoice_No = ObjPO.Invoice_No;
                        obj.created_by = ObjPO.CreatedBy;
                        obj.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        obj.is_express_delivery = ObjPO.IsExpressDelivery;
                        obj.express_delivery_price = ObjPO.ExpressChargeApplied;

                        obj.is_promocode_applied = ObjPO.IsPromocodeApplied;
                        obj.promocode = ObjPO.Promocode;
                        obj.promocode_discount = ObjPO.Promocode_Discount;
                        obj.promocode_amount = ObjPO.Promocode_Amount;
                        obj.delivery_charge = ObjPO.Delivery_charge;


                        if (ObjPO.orderstatusID == 5)
                            obj.is_tagged = true;
                        else
                            obj.is_tagged = false;
                        Context.t_orders.Add(obj);
                        Context.SaveChanges();
                        iRes = obj.order_id;

                        #endregion


                        StringBuilder sb = new StringBuilder();
                        double? GrandTotal = 0;
                        int? total_quant = 0;
                        foreach (PurchaseOrderDetail product in ObjPO.PurchaseOrderDetail)
                        {
                            if (product.ExtraPrice == null)
                            {
                                product.ExtraPrice = 0;
                            }
                            t_order_details Objdetail = new t_order_details();
                            Objdetail.order_id = obj.order_id;
                            Objdetail.service_id = product.ServiceID;
                            Objdetail.product_id = product.ProductID;
                            Objdetail.total_price = product.Price;
                            Objdetail.extra_charge_percentage = product.ExtraPercent;
                            Objdetail.extra_charge_price = product.ExtraPrice;
                            Objdetail.quantity = product.Quantity;
                            Objdetail.remaining_quantity = product.Quantity;
                            Objdetail.delivered_quantity = 0;
                            Objdetail.unit = product.Unit;
                            total_quant += product.Quantity;
                            Context.t_order_details.Add(Objdetail);

                            var Prod = Context.t_service_price.SingleOrDefault(x => x.product_id == product.ProductID && x.service_id == product.ServiceID);
                            sb.Append("<tr>");
                            sb.Append("<td>" + Prod.t_product.product_name + "</td>");
                            sb.Append("<td>" + product.Quantity + "</td>");
                            sb.Append("<td>" + Prod.price + "</td>");
                            sb.Append("<td>" + product.ExtraPercent + "</td>");
                            sb.Append("<td>" + product.ExtraPrice + "</td>");
                            sb.Append("<td>" + product.Price + "</td>");
                            sb.Append("</tr>");
                            GrandTotal = ObjPO.GrandTotal;
                        }
                        Context.SaveChanges();



                        #region Mail and SMS

                        var customerdetails = Context.t_customer.SingleOrDefault(x => x.customer_id == ObjPO.CustomerID);
                        var storedetails = Context.t_store.SingleOrDefault(x => x.store_id == ObjPO.StoreID);
                        var Orderstatus = Context.t_order_cylce.SingleOrDefault(x => x.order_cycle_id == ObjPO.orderstatusID);
                        var orderdetails = Context.t_orders.SingleOrDefault(x => x.order_id == obj.order_id);
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
                        HTML = HTML.Replace(@"$ORDERID$", storedetails.store_code + "-" + obj.order_id.ToString());
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
                        var Del_Date = orderdetails.expected_delivery_date.Value.ToString("dd-MM-yyyy");
                        var Del_Time = orderdetails.delivery_time.ToString();
                        var Store_MobNum = orderdetails.t_store.mobile_no;
                        //var SMSbody = "Dear " + customerdetails.customer_name + "," + Environment.NewLine + "Thanks for your order with Company" + Environment.NewLine + "Order No.: " + storedetails.store_code + "-" + orderdetails.order_id + Environment.NewLine + "Quantity: " + ObjPO.PurchaseOrderDetail.Sum(x => x.Quantity) + Environment.NewLine + "Total Amount: " + orderdetails.grandtotal_price + ".00 INR" + Environment.NewLine + "Status: Order Confirmed" + Environment.NewLine + "Del Date: " + Del_Date + " " + Del_Time + Environment.NewLine + "For any queries, please reach out to " + Store_MobNum + "." + Environment.NewLine + "Order online from www.MaxCleanlaundry.in " + Environment.NewLine + "Outlet: " + storedetails.store_name + Environment.NewLine + "Open Hours: 09:00 AM to 09:00 PM";
                        //var SMSbody = "Dear " + customerdetails.customer_name + "," + Environment.NewLine + "Thanks for your order with Company" + Environment.NewLine + "Order No.: " + storedetails.store_code + "-" + orderdetails.order_id + Environment.NewLine + "Quantity: " + ObjPO.PurchaseOrderDetail.Sum(x => x.Quantity) + Environment.NewLine + "Total Amount: " + orderdetails.grandtotal_price + ".00 INR" + Environment.NewLine + "Status: Order Confirmed" +  Environment.NewLine + "For any queries, please reach out to " + Environment.NewLine + "Order online from www.MaxCleanlaundry.in " + Environment.NewLine + "Outlet: " + storedetails.store_name + Environment.NewLine + "Open Hours: 09:00 AM to 09:00 PM";
                        //var SMSbody = "Dear " + customerdetails.customer_name + ",Company Order No : " + orderdetails.order_id + " Qty.: " + ObjPO.PurchaseOrderDetail.Sum(x => x.Quantity) + " Amt.   : " + orderdetails.grandtotal_price + ".00 INR Status : Order Confirmed ";
                        var SMSbody = "Test";

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


                        // MailTo,Subject,MailBody,CC,BCC
                        var Res = MailSender.SendMail(customerdetails.email_id, "Order Invoice", HTML, storedetails.email_id, "bookings@maxcleanlaundry.in");



                        if (ObjPO.orderstatusID == 2)
                        {
                            PushNotification_Model objpush = new PushNotification_Model();
                            objpush.Title = "New Order Ready for Pickup";
                            objpush.Body = "Order ID : " + obj.order_id;
                            pushnotification(objpush);

                            //var SMSbody2 = "New Order Ready for Pickup";
                            ////var SMSbody = "Dear " + customerdetails.customer_name + ",Company Order No : " + orderdetails.order_id + " Qty.: " + ObjPO.PurchaseOrderDetail.Sum(x => x.Quantity) + " Amt.   : " + orderdetails.grandtotal_price + ".00 INR Status : Order Confirmed ";
                            //List<DeliveryBoy_Model> db = (from a in Context.t_delivery_boy
                            //                              select new DeliveryBoy_Model
                            //                              {
                            //                                  DeliveryBoyMobileNumber = a.mobile
                            //                              }).ToList();
                            //string mobiles = "";
                            //foreach (var mobile in db)
                            //{
                            //    mobiles += mobile.DeliveryBoyMobileNumber + ",";
                            //}
                            //StringBuilder baseurl2 = new StringBuilder();
                            //baseurl2.Append(EmailSMSCredentials.SMS_URL);
                            //baseurl2.Append("sender=" + EmailSMSCredentials.SMS_SENDER_NAME);
                            //baseurl2.Append("&route=" + EmailSMSCredentials.Route);
                            //baseurl2.Append("&mobiles=" + mobiles);
                            //baseurl2.Append("&authkey=" + EmailSMSCredentials.SMS_OauthKey);
                            //baseurl2.Append("&country=" + EmailSMSCredentials.SMS_Country);
                            //baseurl2.Append("&message=" + SMSbody2);

                            //WebClient client1 = new WebClient();
                            //Stream data1 = client1.OpenRead(baseurl2.ToString());
                            //StreamReader reader1 = new StreamReader(data1);
                            //var Status1 = reader1.ReadToEnd();
                            //data1.Close();
                            //reader1.Close();

                        }

                        if (ObjPO.orderstatusID == 5)
                        {
                            var tag_exist = Context.t_taggs.FirstOrDefault(x => x.order_id == obj.order_id);
                            if (tag_exist != null)
                            {
                                iRes = -1;
                            }
                            else
                            {
                                t_taggs tag = new t_taggs();
                                tag.order_id = obj.order_id;
                                tag.tag_number = obj.order_id + "/" + total_quant;
                                tag.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                                Context.t_taggs.Add(tag);
                            }


                        }
                        //dbContextTransaction.Commit();
                        Context.SaveChanges();
                        #endregion

                        scope.Complete();

                        return obj.order_id;
                    }
                    catch (Exception ex)
                    {
                        //dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
            }
        }


        public dynamic pushnotification(PushNotification_Model obj)
        {
            try
            {
                var BaseURL = "http://api.maxcleanlaundry.in/api/";
                //var BaseURL = "http://maxcleanapilocal.myprelaunch.in/api/";
                //var BaseURL = "http://maxcleanapi.myprelaunch.in/api/";
                //var BaseURL = "http://localhost:49170/api/";
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(BaseURL + "PushNotification", "POST", JsonConvert.SerializeObject(obj)));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public long UpdateDetails(PurchaseOrder_Model ObjPO)
        {
            long IRes = 0;
            try
            {
                using (maxcleanEntities context = new maxcleanEntities())
                {
                    var IDExist = context.t_orders.FirstOrDefault(x => x.order_id == ObjPO.OrderID);

                 //newly added
                    IDExist.delivery_address = ObjPO.DeliveryAddress;
                    IDExist.pickup_address = ObjPO.PickupAddress;
                    IDExist.remark = ObjPO.Remark;
                 //newly added compled
                    if (IDExist == null)
                    {
                        IRes = -4;
                    }
                    else
                    {

                        //if (ObjPO.orderstatusID==5)
                        //{
                        //  var rewards = context.t_reward.FirstOrDefault(x => x.customer_id == IDExist.customer_id);
                        //  rewards.customer_rewardpoints = rewards.customer_rewardpoints + (Convert.ToInt32(IDExist.grandtotal_price / 100) * 10);
                        //}
                        if (ObjPO.PickedbyID != null && IDExist.pickedup_by == null && IDExist.order_status < 3)
                        {
                            IDExist.pickedup_by = ObjPO.PickedbyID;
                            IDExist.order_status = 3;
                        }
                        else
                        {
                            IRes = -6;
                        }
                        if (ObjPO.DeliveredbyID != null && IDExist.delivered_by == null && IDExist.order_status > 7)
                        {
                            IDExist.delivered_by = ObjPO.DeliveredbyID;
                            IDExist.order_status = 9;
                        }
                        else
                        {
                            IRes = -6;
                        }

                        context.SaveChanges();
                        IRes = IDExist.order_id;
                    }

                    return IRes;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// this api used for both mobile and web
        /// </summary>
        /// <param name="ObjPO"></param>
        /// <returns></returns>
        public long Update(PurchaseOrder_Model ObjPO)
        {
            long IRes = 0;
            try
            {
                using (maxcleanEntities context = new maxcleanEntities())
                {
                    var IDExist = context.t_orders.FirstOrDefault(x => x.order_id == ObjPO.OrderID);
                    if (IDExist == null)
                    {
                        IRes = -4;
                    }
                    else
                    {
                        if (ObjPO.PickedbyID != 0)
                            IDExist.order_status = 4;
                        else
                        {
                            IDExist.order_status = 5;

                        }
                        IDExist.Invoice_No = (ObjPO.PaymentType == "Card") ? ObjPO.Invoice_No : null;
                        IDExist.old_balance = ObjPO.OldBalanceApplied;
                        IDExist.express_delivery_price = ObjPO.ExpressChargeApplied;
                        IDExist.rewardsapplied = ObjPO.RewardPointsApplied;


                        IDExist.is_promocode_applied = ObjPO.IsPromocodeApplied;
                        IDExist.promocode = ObjPO.Promocode;
                        IDExist.promocode_discount = ObjPO.Promocode_Discount;
                        IDExist.promocode_amount = ObjPO.Promocode_Amount;
                        IDExist.delivery_charge = ObjPO.Delivery_charge;


                        IDExist.grandtotal_price = ObjPO.GrandTotal;
                        IDExist.paid_amount = ObjPO.PaidAmount;
                        IDExist.advance_amount = ObjPO.PaidAmount;
                        IDExist.balance_amount = ObjPO.GrandTotal - ObjPO.PaidAmount;
                        if (ObjPO.PaidAmount == 0)
                        {
                            IDExist.payment_Status = "pending";
                        }
                        else if (ObjPO.PaidAmount < ObjPO.GrandTotal)
                        {
                            IDExist.payment_Status = "partial";
                        }
                        else
                        {
                            IDExist.payment_Status = "complete";
                        }
                        if (ObjPO.Custm_Sign_At_pickup != null)
                            IDExist.cust_sign_at_pickup = API.CommonClasses.SaveBase64File.SaveBase64Image(ObjPO.OrderID, "Cust_Sign_at_Pickup", "Customer", ObjPO.Custm_Sign_At_pickup, ".jpg");
                        StringBuilder sb = new StringBuilder();
                        foreach (PurchaseOrderDetail Orderdetails in ObjPO.PurchaseOrderDetail)
                        {
                            if (Orderdetails.Quantity == null)
                                Orderdetails.Quantity = 0;

                            var order = context.t_order_details.FirstOrDefault(p => p.order_id == ObjPO.OrderID && p.product_id == Orderdetails.ProductID && p.service_id == Orderdetails.ServiceID);

                            if (order != null)
                            {
                                var PrdPrice = context.t_service_price.FirstOrDefault(p => p.product_id == Orderdetails.ProductID && p.service_id == Orderdetails.ServiceID);
                                if (Orderdetails.Quantity == 0)
                                {
                                    var ord = context.t_order_details.Where(p => p.product_id == Orderdetails.ProductID && p.service_id == Orderdetails.ServiceID && p.order_id == ObjPO.OrderID).SingleOrDefault();
                                    if (ord != null)
                                    {
                                        context.t_order_details.Remove(ord);
                                        context.SaveChanges();
                                    }
                                }
                                else
                                {
                                    order.quantity = Orderdetails.Quantity;
                                    order.extra_charge_percentage = Orderdetails.ExtraPercent;
                                    order.extra_charge_price = Orderdetails.ExtraPrice;
                                    order.total_price = (Orderdetails.Quantity * PrdPrice.price);
                                    context.SaveChanges();
                                }

                                sb.Append("<tr>");
                                sb.Append("<td>" + PrdPrice.t_product.product_name + "</td>");
                                sb.Append("<td>" + PrdPrice.price + "</td>");
                                sb.Append("<td>" + order.quantity + "</td>");
                                sb.Append("<td></td>");
                                sb.Append("<td>" + PrdPrice.price * order.quantity + "</td>");
                                sb.Append("</tr>");
                            }
                            else
                            {
                                var PrdPrice = context.t_service_price.FirstOrDefault(p => p.product_id == Orderdetails.ProductID && p.service_id == Orderdetails.ServiceID);

                                t_order_details obj = new t_order_details();
                                obj.order_id = ObjPO.OrderID;
                                obj.service_id = Orderdetails.ServiceID;
                                obj.product_id = Orderdetails.ProductID;
                                obj.quantity = Orderdetails.Quantity;
                                obj.extra_charge_price = Orderdetails.ExtraPrice;
                                obj.extra_charge_percentage = Orderdetails.ExtraPercent;
                                obj.total_price = (Orderdetails.Quantity * PrdPrice.price);
                                context.t_order_details.Add(obj);
                                sb.Append("<tr>");
                                sb.Append("<td>" + PrdPrice.t_product.product_name + "</td>");
                                sb.Append("<td>" + PrdPrice.price + "</td>");
                                sb.Append("<td>" + obj.quantity + "</td>");
                                sb.Append("<td></td>");
                                sb.Append("<td>" + PrdPrice.price * obj.quantity + "</td>");
                                sb.Append("</tr>");
                                context.SaveChanges();
                            }
                        }
                        var customerdetails = context.t_customer.SingleOrDefault(x => x.customer_id == IDExist.customer_id);
                        var storedetails = context.t_store.SingleOrDefault(x => x.store_id == IDExist.store_id);
                        var Orderstatus = context.t_order_cylce.SingleOrDefault(x => x.order_cycle_id == IDExist.order_status);
                        var orderdetails = context.t_orders.SingleOrDefault(x => x.order_id == ObjPO.OrderID);

                        if (orderdetails.order_status == 5)
                        {
                            orderdetails.is_tagged = true;
                            var tag_exist = context.t_taggs.FirstOrDefault(x => x.order_id == orderdetails.order_id);
                            int? total_quant = orderdetails.t_order_details.Where(x => x.order_id == orderdetails.order_id).Sum(c => c.quantity);
                            if (tag_exist != null)
                            {


                                tag_exist.tag_number = orderdetails.order_id + "/" + total_quant;
                                tag_exist.created_date = CommonClasses.StaticData.GetCurrentDataTime;

                            }
                        }
                        context.SaveChanges();
                        string HTML = new WebClient().DownloadString("http://maxcleanapi.myprelaunch.in/EmailTemplates/OrderInvoice.html");
                        HTML = HTML.Replace(@"$CUSTOMERNAME$", customerdetails.customer_name);
                        HTML = HTML.Replace(@"$CUSTOMERADDRESS$", customerdetails.address);
                        HTML = HTML.Replace(@"$CUSTOMERMOBILE$", customerdetails.mobile_no);
                        HTML = HTML.Replace(@"$STOREMOBILE$", storedetails.mobile_no);


                        HTML = HTML.Replace(@"$STATUS$", Orderstatus.order_cycle_stage);
                        HTML = HTML.Replace(@"$ORDERDATE$", orderdetails.order_date.ToString());
                        HTML = HTML.Replace(@"$DELIVERYDATE$", orderdetails.delivery_date.ToString());
                        HTML = HTML.Replace(@"$PAIDAMOUNT$", orderdetails.paid_amount.ToString());
                        HTML = HTML.Replace(@"$BALANCEAMOUNT$", orderdetails.balance_amount.ToString());
                        HTML = HTML.Replace(@"$DELIVERYCHARGE$", (orderdetails.delivery_charge != null) ? orderdetails.delivery_charge.ToString() : "0");
                        HTML = HTML.Replace(@"$REWARDS$", (orderdetails.rewardsapplied != null) ? orderdetails.rewardsapplied.ToString() : "0");
                        HTML = HTML.Replace(@"$PROMOCODE$", (orderdetails.promocode_amount != null) ? orderdetails.promocode_amount.ToString() : "0");
                        var DateTime = new DateTime();
                        DateTime = Convert.ToDateTime(ObjPO.ExpectingDeliveryDate);
                        HTML = HTML.Replace(@"$STATUS$", "<b><label style='color:green'>" + Orderstatus.order_cycle_stage + "</label></b>");
                        // HTML = HTML.Replace(@"$EXPDATE$", "Expecting Delivery On <b>" + DateTime.ToString("dd-MM-yyyy hh:mm:ss tt") + "</b>");
                        HTML = HTML.Replace(@"$REMARK$", orderdetails.remark);
                        HTML = HTML.Replace(@"$ORDERID$", ObjPO.OrderID.ToString());
                        HTML = HTML.Replace(@"$STORENAME$", storedetails.store_name);
                        HTML = HTML.Replace(@"$STOREADDRESS$", storedetails.address);

                        HTML = HTML.Replace(@"$PRODUCTDETAILS$", sb.ToString());
                        HTML = HTML.Replace(@"$GRANDTOTAL$", ObjPO.GrandTotal.ToString());

                        // MailTo,Subject,MailBody,CC,BCC
                        //var Res = MailSender.SendMail(customerdetails.email_id, "Order Invoice", HTML, storedetails.email_id + ",manjunathece041@gmail.com", "");

                        IRes = ObjPO.OrderID;
                    }
                    return IRes;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PurchaseOrder_Model> GetAllOrdersList()
        {

            List<PurchaseOrder_Model> ObjRequest = new List<PurchaseOrder_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjRequest = (from a in Context.t_orders

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
                                             PaymentStatus = a.payment_Status,
                                             PaymentType = a.payment_type,
                                             Invoice_No = a.Invoice_No,


                                             PickedbyID = a.pickedup_by,
                                             Pickedby = a.t_delivery_boy.name,
                                             DeliveredbyID = a.delivered_by,
                                             DeliveryboyName = (a.delivered_by != null) ? Context.t_delivery_boy.FirstOrDefault(p => p.id == a.delivered_by).name : null,



                                             IsExpressDelivery = a.is_express_delivery,
                                             OrderDate = a.order_date,
                                             ExpectingDeliveryDate = a.expected_delivery_date,
                                             ActualDeliveryDate = a.delivery_date,
                                             DeliveryAddress = a.delivery_address,
                                             PickupTime = a.pickup_time,
                                             DeliveryTime = a.delivery_time,
                                             Advance_Amount = a.advance_amount,
                                             PaidAmount = a.paid_amount,
                                             RemainingAmount = a.balance_amount,
                                             GrandTotal = a.grandtotal_price,
                                             PickupAddress = a.pickup_address,
                                             orderstatusID = a.order_status,
                                             OrderStatus = a.t_order_cylce.order_cycle_stage,
                                             IsPromocodeApplied = a.is_promocode_applied,
                                             Promocode = a.promocode,
                                             Promocode_Discount = a.promocode_discount,
                                             Promocode_Amount = a.promocode_amount,
                                             Delivery_charge = a.delivery_charge,
                                             pickup_address_type = a.pickup_address_type,
                                             delivery_address_type = a.delivery_address_type,
                                         }).OrderByDescending(x => x.OrderID).ToList();



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PurchaseOrder_Model> GetAllOnlineOutletOrders(bool? IsOutlet)
        {

            List<PurchaseOrder_Model> ObjRequest = new List<PurchaseOrder_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Obj = (from a in Context.t_orders

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
                                   Invoice_No = a.Invoice_No,
                                   PickedbyID = a.pickedup_by,
                                   Pickedby = a.t_delivery_boy.name,
                                   DeliveredbyID = a.delivered_by,
                                   DeliveryboyName = (a.delivered_by != null) ? Context.t_delivery_boy.FirstOrDefault(p => p.id == a.delivered_by).name : null,
                                   IsOutletOrder = a.is_outlet_order,
                                   OrderDate = a.order_date,
                                   ExpectingDeliveryDate = a.expected_delivery_date,
                                   ActualDeliveryDate = a.delivery_date,
                                   DeliveryAddress = a.delivery_address,
                                   PickupTime = a.pickup_time,
                                   DeliveryTime = a.delivery_time,
                                   PaidAmount = a.paid_amount,
                                   RemainingAmount = a.balance_amount,
                                   Advance_Amount = a.advance_amount,
                                   GrandTotal = a.grandtotal_price,
                                   PickupAddress = a.pickup_address,
                                   orderstatusID = a.order_status,
                                   OrderStatus = a.t_order_cylce.order_cycle_stage,
                                   PaymentStatus = a.payment_Status,
                                   PaymentType = a.payment_type,
                                   IsExpressDelivery = a.is_express_delivery,
                                   IsPromocodeApplied = a.is_promocode_applied,
                                   Promocode = a.promocode,
                                   Promocode_Discount = a.promocode_discount,
                                   Promocode_Amount = a.promocode_amount,
                                   Delivery_charge = a.delivery_charge,
                                   pickup_address_type = a.pickup_address_type,
                                   delivery_address_type = a.delivery_address_type,
                               });

                    if (IsOutlet != null)
                    {
                        Obj = Obj.Where(a => a.IsOutletOrder == IsOutlet).AsQueryable();
                    }
                    var list = Obj.OrderByDescending(x => x.OrderID).ToList();

                    return ObjRequest = list;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PurchaseOrder_Model> GetOrdersByStoreId(int StoreID)
        {

            List<PurchaseOrder_Model> ObjRequest = new List<PurchaseOrder_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Obj = (from a in Context.t_orders
                               where a.store_id == StoreID
                               select new PurchaseOrder_Model
                               {
                                   OrderID = a.order_id,
                                   CustomerID = a.customer_id,
                                   CustomerName = a.t_customer.customer_name,
                                   CustomerMobile = a.t_customer.mobile_no,
                                   CustomerAddress = a.t_customer.address,
                                   StoreID = a.store_id,
                                   StoreName = a.t_store.store_name,
                                   StoreMobileNo = a.t_store.mobile_no,
                                   StoreAddress = a.t_store.address,
                                   Store_code = a.t_store.store_code,
                                   ConfirmedBy = a.confirmed_by,
                                   ConfirmedUser = a.t_user.user_name,
                                   Invoice_No = a.Invoice_No,
                                   OldBalanceApplied = a.old_balance,
                                   PickedbyID = a.pickedup_by,
                                   Pickedby = a.t_delivery_boy.name,
                                   DeliveredbyID = a.delivered_by,
                                   DeliveryboyName = (a.delivered_by != null) ? Context.t_delivery_boy.FirstOrDefault(p => p.id == a.delivered_by).name : null,
                                   TotalQuantity = (a.order_id != 0) ? Context.t_order_details.Where(p => p.order_id == a.order_id).Sum(c => c.quantity) : 0,
                                   IsOutletOrder = a.is_outlet_order,
                                   OrderDate = a.order_date,
                                   ExpectingDeliveryDate = a.expected_delivery_date,
                                   ActualDeliveryDate = a.delivery_date,
                                   DeliveryAddress = a.delivery_address,
                                   PickupTime = a.pickup_time,
                                   DeliveryTime = a.delivery_time,
                                   PaidAmount = a.paid_amount,
                                   RemainingAmount = a.balance_amount,
                                   Advance_Amount = a.advance_amount,
                                   GrandTotal = a.grandtotal_price,
                                   PickupAddress = a.pickup_address,
                                   orderstatusID = a.order_status,
                                   OrderStatus = a.t_order_cylce.order_cycle_stage,
                                   PaymentStatus = a.payment_Status,
                                   PaymentType = a.payment_type,
                                   IsExpressDelivery = a.is_express_delivery,
                                   IsPromocodeApplied = a.is_promocode_applied,
                                   Promocode = a.promocode,
                                   Promocode_Discount = a.promocode_discount,
                                   Promocode_Amount = a.promocode_amount,
                                   Delivery_charge = a.delivery_charge,
                                   pickup_address_type = a.pickup_address_type,
                                   delivery_address_type = a.delivery_address_type,
                               }).OrderByDescending(x => x.OrderID).ToList();



                    return ObjRequest = Obj;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Base_Model GetOrdersList_BystoreID(string length, string start, string sortColumn, string sortColumnDir, string draw, string search, long id, string FromDate, string ToDate)
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

            List<PurchaseOrder_Model> ObjordersList = new List<PurchaseOrder_Model>();
            using (API.DataAccess.maxcleanEntities Context = new API.DataAccess.maxcleanEntities())
            {
                // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
                Context.Configuration.LazyLoadingEnabled = false;

                var Objorders = (from a in Context.t_orders
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
                                     Invoice_No = a.Invoice_No,
                                     OldBalanceApplied = a.old_balance,
                                     PickedbyID = a.pickedup_by,
                                     Pickedby = a.t_delivery_boy.name,
                                     DeliveredbyID = a.delivered_by,
                                     DeliveryboyName = (a.delivered_by != null) ? Context.t_delivery_boy.FirstOrDefault(p => p.id == a.delivered_by).name : null,
                                     TotalQuantity = (a.order_id != 0) ? Context.t_order_details.Where(p => p.order_id == a.order_id).Sum(c => c.quantity) : 0,
                                     IsOutletOrder = a.is_outlet_order,
                                     OrderDate = a.order_date,
                                     ExpectingDeliveryDate = a.expected_delivery_date,
                                     ActualDeliveryDate = a.delivery_date,
                                     DeliveryAddress = a.delivery_address,
                                     PickupTime = a.pickup_time,
                                     DeliveryTime = a.delivery_time,
                                     PaidAmount = a.paid_amount,
                                     RemainingAmount = a.balance_amount,
                                     Advance_Amount = a.advance_amount,
                                     GrandTotal = a.grandtotal_price,
                                     PickupAddress = a.pickup_address,
                                     orderstatusID = a.order_status,
                                     OrderStatus = a.t_order_cylce.order_cycle_stage,
                                     PaymentStatus = a.payment_Status,
                                     PaymentType = a.payment_type,
                                     IsExpressDelivery = a.is_express_delivery,
                                     IsPromocodeApplied = a.is_promocode_applied,
                                     Promocode = a.promocode,
                                     Promocode_Discount = a.promocode_discount,
                                     Promocode_Amount = a.promocode_amount,
                                     Delivery_charge = a.delivery_charge,
                                     pickup_address_type = a.pickup_address_type,
                                     delivery_address_type = a.delivery_address_type,
                                     is_cancel=a.is_cancel
                                 });


                if (fromDate != null)
                {
                    Objorders = Objorders.Where(a => a.OrderDate >= fromDate).AsQueryable();
                }
                if (toDate != null)
                {
                    Objorders = Objorders.Where(a => a.OrderDate <= toDate).AsQueryable();
                }
                if (id != 0)
                {
                    Objorders = Objorders.Where(a => a.StoreID == id).AsQueryable();
                }
                ObjordersList = Objorders.OrderByDescending(x => x.OrderID).ToList();

                // Verification.   
                if (!string.IsNullOrEmpty(search) &&
                    !string.IsNullOrWhiteSpace(search) && (search != "$0$"))
                {
                    // Apply search   
                    ObjordersList = ObjordersList.Where(
                     p => p.OrderID.ToString().Contains(search) ||
                    p.GrandTotal.ToString().Contains(search) ||
                     p.RemainingAmount.ToString().Contains(search) ||
                      p.CustomerMobile.ToString().Contains(search) ||
                       p.CustomerName.ToString().ToLower().Contains(search.ToLower()) ||
                       p.OrderStatus.Replace(" ", "").ToLower().Trim().Contains(search.ToLower())

                     ).ToList();
                }


                ////SORT
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    ObjordersList = ObjordersList.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                }

                recordsTotal = ObjordersList.Count();
                var data = ObjordersList.Skip(skip).Take(pageSize).ToList();
                Base_Model obj = new Base_Model();
                obj.draw = draw;
                obj.recordsTotal = recordsTotal;
                obj.data = data;

                return obj;
            }
        }

        public List<PurchaseOrder_Model> GetAllOutletOrders()
        {

            List<PurchaseOrder_Model> ObjRequest = new List<PurchaseOrder_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjRequest = (from a in Context.t_orders
                                         where a.is_outlet_order == true
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
                                             Invoice_No = a.Invoice_No,
                                             DeliveredbyID = a.delivered_by,
                                             //DeliveryboyName = a.t_delivery_boy.name,
                                             DeliveryboyName = (a.delivered_by != null) ? Context.t_delivery_boy.FirstOrDefault(p => p.id == a.delivered_by).name : null,

                                             OrderDate = a.order_date,
                                             ExpectingDeliveryDate = a.expected_delivery_date,
                                             ActualDeliveryDate = a.delivery_date,
                                             DeliveryAddress = a.delivery_address,
                                             PickupTime = a.pickup_time,
                                             DeliveryTime = a.delivery_time,
                                             PaidAmount = a.paid_amount,
                                             Advance_Amount = a.advance_amount,
                                             RemainingAmount = a.balance_amount,
                                             GrandTotal = a.grandtotal_price,
                                             PickupAddress = a.pickup_address,
                                             orderstatusID = a.order_status,
                                             OrderStatus = a.t_order_cylce.order_cycle_stage,
                                             PaymentStatus = a.payment_Status,
                                             PaymentType = a.payment_type,
                                             IsExpressDelivery = a.is_express_delivery,
                                             IsPromocodeApplied = a.is_promocode_applied,
                                             Promocode = a.promocode,
                                             Promocode_Discount = a.promocode_discount,
                                             Promocode_Amount = a.promocode_amount,
                                             Delivery_charge = a.delivery_charge,
                                             pickup_address_type = a.pickup_address_type,
                                             delivery_address_type = a.delivery_address_type,
                                         }).OrderByDescending(x => x.OrderID).ToList();


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PurchaseOrder_Model GetOrderByID(long OrderID)
        {
            PurchaseOrder_Model ObjOrder = new PurchaseOrder_Model();
            var id = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Order = (from a in Context.t_orders
                                 where a.order_id == OrderID
                                 select new PurchaseOrder_Model
                                 {
                                     OrderID = a.order_id,
                                     CustomerID = a.customer_id,
                                     CustomerName = a.t_customer.customer_name,
                                     CustomerMobile = a.t_customer.mobile_no,
                                     CustomerAddress = a.t_customer.address,
                                     StoreID = a.store_id,
                                     StoreName = a.t_store.store_name,
                                     StoreMobileNo = a.t_store.mobile_no,
                                     Store_code = a.t_store.store_code,
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
                                     PickupDate = a.pickup_date,
                                     PickupTime = a.pickup_time,
                                     DeliveryTime = a.delivery_time,
                                     PaidAmount = a.paid_amount,
                                     Advance_Amount = a.advance_amount,
                                     RemainingAmount = a.balance_amount,
                                     GrandTotal = a.grandtotal_price,
                                     PickupAddress = a.pickup_address,
                                     orderstatusID = a.order_status,
                                     OrderStatus = a.t_order_cylce.order_cycle_stage,
                                     Remark = a.remark,
                                     RewardPointsApplied = a.rewardsapplied,
                                     PaymentType = a.payment_type,
                                     PaymentStatus = a.payment_Status,
                                     OldBalanceApplied = a.old_balance,
                                     ExpressChargeApplied = a.express_delivery_price,
                                     RescheduledNote = a.note,
                                     //TagNumber = a.t_tag_details.Where(x => x.order_id == OrderID).Select(x => x.t_taggs.tag_number).FirstOrDefault(),
                                     IsExpressDelivery = a.is_express_delivery,
                                     Invoice_No = a.Invoice_No,
                                     UpdateBy = a.updated_by,
                                     Admin_name = (a.updated_by != null) ? Context.t_user.FirstOrDefault(p => p.user_id == a.updated_by).user_name : null,
                                     UpdateDate = a.updated_date,
                                     IsPromocodeApplied = a.is_promocode_applied,
                                     Promocode = a.promocode,
                                     Promocode_Discount = a.promocode_discount,
                                     Promocode_Amount = a.promocode_amount,
                                     Delivery_charge = a.delivery_charge,
                                     pickup_address_type = (a.pickup_address_type != null) ? a.pickup_address_type : 0,
                                     delivery_address_type = (a.delivery_address_type != null) ? a.delivery_address_type : 0,
                                     is_cancel= a.is_cancel,
                                 }).SingleOrDefault();


                    Order.rating = Context.t_customer_rating.Where(x => x.order_id == OrderID).Select(x => x.rating).SingleOrDefault();
                    if (Order.rating == null)
                    {
                        Order.rating = 0;
                    }
                    Order.comments = Context.t_customer_rating.Where(x => x.order_id == OrderID).Select(x => x.comments).SingleOrDefault();


                    Order.PurchaseOrderDetail = (from a in Context.t_order_details
                                                 where a.order_id == OrderID
                                                 select new PurchaseOrderDetail
                                                 {
                                                     OrderID = a.order_id,
                                                     OrderDetailsID = a.order_details_id,
                                                     ServiceID = a.service_id,
                                                     ServiceName = a.t_service.service_name,
                                                     CategoryID = a.t_product.t_category.category_id,
                                                     CategoryName = a.t_product.t_category.category_name,
                                                     Price = a.total_price,
                                                     ProductID = a.product_id,
                                                     DeliveredQuantity = a.delivered_quantity,
                                                     RemainingQuantity = a.remaining_quantity,
                                                     //UnitPrice = (a.service_id != null && a.product_id != null) ? Context.t_service_price.FirstOrDefault(p => p.service_id == a.service_id && p.product_id==a.product_id).price : null,
                                                     // UnitPrice = Context.t_service_price.FirstOrDefault(p => p.service_id == a.service_id && p.product_id == a.product_id).price,
                                                     ProductName = a.t_product.product_name,
                                                     ExtraPercent = a.extra_charge_percentage,
                                                     ExtraPrice = a.extra_charge_price,
                                                     Quantity = a.quantity,

                                                     Unit = a.unit

                                                 }).ToList();


                    foreach (var orddetails in Order.PurchaseOrderDetail)
                    {
                        var prc = Context.t_service_price.Where(x => x.product_id == orddetails.ProductID && x.service_id == orddetails.ServiceID).Select(a => a.price);
                        orddetails.UnitPrice = prc.FirstOrDefault();
                    }
                    Order.TagNumber = Context.t_taggs.Where(x => x.order_id == OrderID).Select(x => x.tag_number).SingleOrDefault();
                    Order.TotalQuantity = Context.t_order_details.Where(c => c.order_id == OrderID).Select(c => c.quantity).Count();
                    ObjOrder = Order;

                    return ObjOrder;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PurchaseOrder_Model> GetAllOrders(PurchaseOrder_Model dates)
        {

            List<PurchaseOrder_Model> ObjRequest = new List<PurchaseOrder_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Obj = (from a in Context.t_orders
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
                                   StoreMobileNo = a.t_store.mobile_no,
                                   Store_code = a.t_store.store_code,
                                   StoreAddress = a.t_store.address,
                                   ConfirmedBy = a.confirmed_by,
                                   ConfirmedUser = a.t_user.user_name,
                                   PickedbyID = a.pickedup_by,
                                   Pickedby = a.t_delivery_boy.name,
                                   DeliveredbyID = a.delivered_by,
                                   DeliveryboyName = (a.delivered_by != null) ? Context.t_delivery_boy.FirstOrDefault(p => p.id == a.delivered_by).name : null,
                                   TotalQuantity = (a.order_id != 0) ? Context.t_order_details.Where(p => p.order_id == a.order_id).Select(c => c.quantity).Count() : 0,
                                   OrderDate = a.order_date,
                                   ExpectingDeliveryDate = a.expected_delivery_date,
                                   ActualDeliveryDate = a.delivery_date,
                                   DeliveryAddress = a.delivery_address,
                                   PickupTime = a.pickup_time,
                                   DeliveryTime = a.delivery_time,
                                   PaidAmount = a.paid_amount,
                                   Advance_Amount = a.advance_amount,
                                   RemainingAmount = a.balance_amount,
                                   GrandTotal = a.grandtotal_price,
                                   PaymentStatus = a.payment_Status,
                                   PaymentType = a.payment_type,
                                   orderstatusID = a.order_status,
                                   OrderStatus = a.t_order_cylce.order_cycle_stage,
                                   IsExpressDelivery = a.is_express_delivery,
                                   Invoice_No = a.Invoice_No,
                                   IsPromocodeApplied = a.is_promocode_applied,
                                   Promocode = a.promocode,
                                   Promocode_Discount = a.promocode_discount,
                                   Promocode_Amount = a.promocode_amount,
                                   Delivery_charge = a.delivery_charge,
                                   pickup_address_type = a.pickup_address_type,
                                   delivery_address_type = a.delivery_address_type,
                               });
                    if (dates.FromDate != null)
                    {
                        Obj = Obj.Where(a => a.ExpectingDeliveryDate >= dates.FromDate).AsQueryable();
                    }
                    if (dates.ToDate != null)
                    {
                        Obj = Obj.Where(a => a.ExpectingDeliveryDate <= dates.ToDate).AsQueryable();
                    }
                    if (dates.StoreID != null)
                    {
                        Obj = Obj.Where(a => a.StoreID == dates.StoreID).AsQueryable();
                    }
                    if (dates.OrderStatus != null)
                    {
                        Obj = Obj.Where(a => a.orderstatusID == dates.orderstatusID).AsQueryable();
                    }
                    var list = Obj.OrderByDescending(a => a.ExpectingDeliveryDate).ToList();
                    return ObjRequest = list;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long payBalance(PurchaseOrder_Model ObjPO)
        {
            long Res = 0;
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    var order = Context.t_orders.FirstOrDefault(x => x.order_id == ObjPO.OrderID);
                    if (order == null)
                    {
                        Res = -4;
                    }
                    else
                    {

                        order.balance_amount = order.grandtotal_price - (order.paid_amount + ObjPO.PaidAmount);
                        order.paid_amount = order.paid_amount + ObjPO.PaidAmount;
                        if ((order.grandtotal_price - order.paid_amount) == 0)
                        {
                            order.payment_Status = "complete";
                        }
                        else if ((order.grandtotal_price - order.paid_amount) == order.grandtotal_price)
                        {
                            order.payment_Status = "pending";
                        }
                        else
                        {
                            order.payment_Status = "partial";
                        }
                        Res = order.order_id;
                        Context.SaveChanges();
                    }
                }
                return Res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long UpdateStatus(PurchaseOrder_Model obj)
        {
            long Res = 0;
            try
            {
                using (maxcleanEntities context = new maxcleanEntities())
                {
                    var OrderIDExist = context.t_orders.FirstOrDefault(x => x.order_id == obj.OrderID);
                    if (OrderIDExist == null)
                    {
                        Res = -1;
                    }
                    else
                    {
                        if (OrderIDExist.order_status > obj.orderstatusID)
                        {
                            Res = -11;
                        }
                        else
                        {
                            if (obj.orderstatusID == 2)
                            {
                                OrderIDExist.order_status = obj.orderstatusID;
                                PushNotification_Model objp = new PushNotification_Model();
                                objp.Title = "New Order Ready for Pickup";
                                objp.Body = "Order ID : " + obj.OrderID;
                                pushnotification(objp);

                                //var SMSbody2 = "New Order Ready for Pickup";
                                ////var SMSbody = "Dear " + customerdetails.customer_name + ",Company Order No : " + orderdetails.order_id + " Qty.: " + ObjPO.PurchaseOrderDetail.Sum(x => x.Quantity) + " Amt.   : " + orderdetails.grandtotal_price + ".00 INR Status : Order Confirmed ";
                                //List<DeliveryBoy_Model> db = (from a in context.t_delivery_boy
                                //                              select new DeliveryBoy_Model
                                //                              {
                                //                                  DeliveryBoyMobileNumber = a.mobile
                                //                              }).ToList();
                                //string mobiles = "";
                                //foreach (var mobile in db)
                                //{
                                //    mobiles += mobile.DeliveryBoyMobileNumber + ",";
                                //}
                                //StringBuilder baseurl2 = new StringBuilder();
                                //baseurl2.Append(EmailSMSCredentials.SMS_URL);
                                //baseurl2.Append("sender=" + EmailSMSCredentials.SMS_SENDER_NAME);
                                //baseurl2.Append("&route=" + EmailSMSCredentials.Route);
                                //baseurl2.Append("&mobiles=" + mobiles);
                                //baseurl2.Append("&authkey=" + EmailSMSCredentials.SMS_OauthKey);
                                //baseurl2.Append("&country=" + EmailSMSCredentials.SMS_Country);
                                //baseurl2.Append("&message=" + SMSbody2);

                                //WebClient client1 = new WebClient();
                                //Stream data1 = client1.OpenRead(baseurl2.ToString());
                                //StreamReader reader1 = new StreamReader(data1);
                                //var Status1 = reader1.ReadToEnd();
                                //data1.Close();
                                //reader1.Close();


                                Res = OrderIDExist.order_id;
                            }
                            else if (obj.orderstatusID == 3)
                            {
                                Res = -11;
                            }
                            else if (obj.orderstatusID == 4)
                            {
                                Res = -11;
                            }
                            else if (obj.orderstatusID == 5)
                            {
                                if (OrderIDExist.order_status == 2 || OrderIDExist.order_status == 3)
                                {
                                    Res = -11;
                                }
                                else
                                {
                                    OrderIDExist.order_status = obj.orderstatusID;
                                    OrderIDExist.is_tagged = true;
                                    var tag_exist = context.t_taggs.FirstOrDefault(x => x.order_id == obj.OrderID);
                                    int? total_quant = OrderIDExist.t_order_details.Where(x => x.order_id == obj.OrderID).Sum(c => c.quantity);
                                    if (tag_exist == null)
                                    {
                                        t_taggs tag = new t_taggs();
                                        tag.order_id = obj.OrderID;
                                        tag.tag_number = obj.OrderID + "/" + total_quant;
                                        tag.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                                        context.t_taggs.Add(tag);
                                    }
                                    Res = OrderIDExist.order_id;
                                }
                            }
                            else if (obj.orderstatusID == 6)
                            {
                                if (OrderIDExist.is_tagged == true && OrderIDExist.order_status == 5)
                                {
                                    OrderIDExist.order_status = obj.orderstatusID;
                                    var tag_exist = context.t_taggs.FirstOrDefault(x => x.order_id == obj.OrderID);
                                    if (tag_exist != null)
                                    {
                                        tag_exist.sent_to_factory_on = CommonClasses.StaticData.GetCurrentDataTime;
                                    }
                                    Res = OrderIDExist.order_id;
                                }
                                else
                                {
                                    Res = -10;
                                }

                            }
                            else if (obj.orderstatusID == 7)
                            {
                                if (OrderIDExist.order_status == 6)
                                {
                                    //OrderIDExist.order_status = obj.orderstatusID;
                                    //var tag_exist = context.t_taggs.FirstOrDefault(x => x.order_id == obj.OrderID);
                                    //if (tag_exist != null)
                                    //{
                                    //    tag_exist.received_from_factory = CommonClasses.StaticData.GetCurrentDataTime;
                                    //}
                                    //Res = OrderIDExist.order_id;
                                    if (OrderIDExist.is_outlet_order == false)
                                    {
                                        OrderIDExist.order_status = 8;
                                        var customerdetails = context.t_customer.SingleOrDefault(x => x.customer_id == OrderIDExist.customer_id);
                                        var storedetails = context.t_store.SingleOrDefault(x => x.store_id == OrderIDExist.store_id);

                                        var orderdetails = context.t_orders.SingleOrDefault(x => x.order_id == obj.OrderID);
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
                                        Res = OrderIDExist.order_id;

                                        PushNotification_Model objp = new PushNotification_Model();
                                        objp.Title = "New Order Ready for Delivery";
                                        objp.Body = "Order ID : " + obj.OrderID;
                                        pushnotification(objp);

                                        //var SMSbody2 = "New Order Ready for Delivery";
                                        ////var SMSbody = "Dear " + customerdetails.customer_name + ",Company Order No : " + orderdetails.order_id + " Qty.: " + ObjPO.PurchaseOrderDetail.Sum(x => x.Quantity) + " Amt.   : " + orderdetails.grandtotal_price + ".00 INR Status : Order Confirmed ";
                                        //List<DeliveryBoy_Model> db = (from a in context.t_delivery_boy
                                        //                              select new DeliveryBoy_Model
                                        //                              {
                                        //                                  DeliveryBoyMobileNumber = a.mobile
                                        //                              }).ToList();
                                        //string mobiles = "";
                                        //foreach (var mobile in db)
                                        //{
                                        //    mobiles += mobile.DeliveryBoyMobileNumber + ",";
                                        //}
                                        //StringBuilder baseurl2 = new StringBuilder();
                                        //baseurl2.Append(EmailSMSCredentials.SMS_URL);
                                        //baseurl2.Append("sender=" + EmailSMSCredentials.SMS_SENDER_NAME);
                                        //baseurl2.Append("&route=" + EmailSMSCredentials.Route);
                                        //baseurl2.Append("&mobiles=" + mobiles);
                                        //baseurl2.Append("&authkey=" + EmailSMSCredentials.SMS_OauthKey);
                                        //baseurl2.Append("&country=" + EmailSMSCredentials.SMS_Country);
                                        //baseurl2.Append("&message=" + SMSbody2);

                                        //WebClient client1 = new WebClient();
                                        //Stream data1 = client1.OpenRead(baseurl2.ToString());
                                        //StreamReader reader1 = new StreamReader(data1);
                                        //var Status1 = reader1.ReadToEnd();
                                        //data1.Close();
                                        //reader1.Close();

                                    }
                                    else
                                    {
                                        OrderIDExist.order_status = 7;
                                        var customerdetails = context.t_customer.SingleOrDefault(x => x.customer_id == OrderIDExist.customer_id);
                                        var storedetails = context.t_store.SingleOrDefault(x => x.store_id == OrderIDExist.store_id);

                                        var orderdetails = context.t_orders.SingleOrDefault(x => x.order_id == obj.OrderID);
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
                                        Res = OrderIDExist.order_id;
                                    }
                                }
                                else
                                {
                                    Res = -11;
                                }

                            }
                            else if (obj.orderstatusID == 8)
                            {
                                if (OrderIDExist.order_status == 7)
                                {
                                    OrderIDExist.order_status = obj.orderstatusID;
                                    var customerdetails = context.t_customer.SingleOrDefault(x => x.customer_id == OrderIDExist.customer_id);
                                    var storedetails = context.t_store.SingleOrDefault(x => x.store_id == OrderIDExist.store_id);

                                    var orderdetails = context.t_orders.SingleOrDefault(x => x.order_id == obj.OrderID);
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
                                    Res = OrderIDExist.order_id;


                                    PushNotification_Model objp = new PushNotification_Model();
                                    objp.Title = "New Order Ready for Delivery";
                                    objp.Body = "Order ID : " + obj.OrderID;
                                    pushnotification(objp);

                                    //var SMSbody2 = "New Order Ready for Delivery";
                                    ////var SMSbody = "Dear " + customerdetails.customer_name + ",Company Order No : " + orderdetails.order_id + " Qty.: " + ObjPO.PurchaseOrderDetail.Sum(x => x.Quantity) + " Amt.   : " + orderdetails.grandtotal_price + ".00 INR Status : Order Confirmed ";
                                    //List<DeliveryBoy_Model> db = (from a in context.t_delivery_boy
                                    //                              select new DeliveryBoy_Model
                                    //                              {
                                    //                                  DeliveryBoyMobileNumber = a.mobile
                                    //                              }).ToList();
                                    //string mobiles = "";
                                    //foreach (var mobile in db)
                                    //{
                                    //    mobiles += mobile.DeliveryBoyMobileNumber + ",";
                                    //}
                                    //StringBuilder baseurl2 = new StringBuilder();
                                    //baseurl2.Append(EmailSMSCredentials.SMS_URL);
                                    //baseurl2.Append("sender=" + EmailSMSCredentials.SMS_SENDER_NAME);
                                    //baseurl2.Append("&route=" + EmailSMSCredentials.Route);
                                    //baseurl2.Append("&mobiles=" + mobiles);
                                    //baseurl2.Append("&authkey=" + EmailSMSCredentials.SMS_OauthKey);
                                    //baseurl2.Append("&country=" + EmailSMSCredentials.SMS_Country);
                                    //baseurl2.Append("&message=" + SMSbody2);

                                    //WebClient client1 = new WebClient();
                                    //Stream data1 = client1.OpenRead(baseurl2.ToString());
                                    //StreamReader reader1 = new StreamReader(data1);
                                    //var Status1 = reader1.ReadToEnd();
                                    //data1.Close();
                                    //reader1.Close();


                                }
                                else
                                {
                                    Res = -11;
                                }
                            }
                            else if (obj.orderstatusID == 9)
                            {

                                Res = -11;

                            }
                            else if (obj.orderstatusID == 10)
                            {

                                Res = -11;

                            }
                            else if (obj.orderstatusID == 11)
                            {
                                if (OrderIDExist.order_status > 6)
                                {
                                    double? paidPercent = (OrderIDExist.paid_amount / OrderIDExist.grandtotal_price) * 100;
                                    if (paidPercent >= 80)
                                    {
                                        OrderIDExist.order_status = obj.orderstatusID;
                                        OrderIDExist.delivery_date = CommonClasses.StaticData.GetCurrentDataTime;
                                        updateRewards(OrderIDExist.customer_id, OrderIDExist.paid_amount);
                                        var custexist = context.t_customer.FirstOrDefault(x => x.customer_id == OrderIDExist.customer_id);
                                        if (custexist != null)
                                        {
                                            custexist.old_balance += (OrderIDExist.grandtotal_price - OrderIDExist.paid_amount);
                                        }
                                        Res = OrderIDExist.order_id;
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
                                OrderIDExist.order_status = obj.orderstatusID;
                                Res = OrderIDExist.order_id;
                            }




                            if (obj.orderstatusID == 8)
                            {
                                if (OrderIDExist.order_status == 7)
                                {
                                    OrderIDExist.order_status = obj.orderstatusID;
                                    PushNotification_Model objp = new PushNotification_Model();
                                    objp.Title = "New Order Ready for Delivery";
                                    objp.Body = "Order ID : " + obj.OrderID;
                                    pushnotification(objp);


                                    //var SMSbody2 = "New Order Ready for Delivery";
                                    ////var SMSbody = "Dear " + customerdetails.customer_name + ",Company Order No : " + orderdetails.order_id + " Qty.: " + ObjPO.PurchaseOrderDetail.Sum(x => x.Quantity) + " Amt.   : " + orderdetails.grandtotal_price + ".00 INR Status : Order Confirmed ";
                                    //List<DeliveryBoy_Model> db = (from a in context.t_delivery_boy
                                    //                              select new DeliveryBoy_Model
                                    //                              {
                                    //                                  DeliveryBoyMobileNumber = a.mobile
                                    //                              }).ToList();
                                    //string mobiles = "";
                                    //foreach (var mobile in db)
                                    //{
                                    //    mobiles += mobile.DeliveryBoyMobileNumber + ",";
                                    //}
                                    //StringBuilder baseurl2 = new StringBuilder();
                                    //baseurl2.Append(EmailSMSCredentials.SMS_URL);
                                    //baseurl2.Append("sender=" + EmailSMSCredentials.SMS_SENDER_NAME);
                                    //baseurl2.Append("&route=" + EmailSMSCredentials.Route);
                                    //baseurl2.Append("&mobiles=" + mobiles);
                                    //baseurl2.Append("&authkey=" + EmailSMSCredentials.SMS_OauthKey);
                                    //baseurl2.Append("&country=" + EmailSMSCredentials.SMS_Country);
                                    //baseurl2.Append("&message=" + SMSbody2);

                                    //WebClient client1 = new WebClient();
                                    //Stream data1 = client1.OpenRead(baseurl2.ToString());
                                    //StreamReader reader1 = new StreamReader(data1);
                                    //var Status1 = reader1.ReadToEnd();
                                    //data1.Close();
                                    //reader1.Close();


                                }
                            }


                            // Res = OrderIDExist.order_id;
                            context.SaveChanges();
                        }
                    }
                }
                return Res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updateRewards(int? customerID, double? grandtotal)
        {
            try
            {
                using (maxcleanEntities context = new maxcleanEntities())
                {
                    var CustRwds = context.t_reward.FirstOrDefault(x => x.customer_id == customerID);
                    if (CustRwds != null)
                    {
                        CustRwds.customer_rewardpoints = CustRwds.customer_rewardpoints + (Convert.ToInt32(grandtotal / 100) * CommonClasses.StaticData.Rewards);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //*******************************Mobile APIs******************************************//

        public List<PurchaseOrder_Model> GetAllPickupReadyOrders()//2
        {
            List<PurchaseOrder_Model> ObjRequest = new List<PurchaseOrder_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    List<PurchaseOrder_Model> Res = (from a in Context.t_orders
                                                     where a.order_status == 2
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
                                                         ExpressChargeApplied = a.express_delivery_price,
                                                         OldBalanceApplied = a.old_balance,
                                                         RewardPointsApplied = a.rewardsapplied,
                                                         PickupDate = a.pickup_date,

                                                         DeliveryboyName = (a.delivered_by != null) ? Context.t_delivery_boy.FirstOrDefault(p => p.id == a.delivered_by).name : null,
                                                         OrderDate = a.order_date,
                                                         ExpectingDeliveryDate = a.expected_delivery_date,
                                                         ActualDeliveryDate = a.delivery_date,
                                                         DeliveryAddress = a.delivery_address,
                                                         PickupTime = a.pickup_time,
                                                         DeliveryTime = a.delivery_time,
                                                         PaidAmount = a.paid_amount,
                                                         Advance_Amount = a.advance_amount,
                                                         RemainingAmount = a.balance_amount,
                                                         GrandTotal = a.grandtotal_price,
                                                         PickupAddress = a.pickup_address,
                                                         orderstatusID = a.order_status,
                                                         OrderStatus = a.t_order_cylce.order_cycle_stage,
                                                         Remark = a.remark,
                                                         PaymentStatus = a.payment_Status,
                                                         PaymentType = a.payment_type,
                                                         //OrderDetailsID = p.order_details_id,
                                                         //CategoryName = p.t_product.t_category.category_name,
                                                         //Price = p.total_price,
                                                         //CategoryID = p.t_product.t_category.category_id,

                                                         //ServiceID = p.service_id,
                                                         //ServiceName = p.t_service.service_name,
                                                         //ProductID = p.product_id,
                                                         //ProductName = p.t_product.product_name,
                                                         // UnitPrice = (a.service_id != null && a.product_id != null) ? Context.t_service_price.FirstOrDefault(p => p.service_id == a.service_id && p.product_id == a.product_id).price : null,
                                                         IsExpressDelivery = a.is_express_delivery,

                                                         //Quantity = p.quantity,
                                                         //DeliveredQuantity = p.delivered_quantity,
                                                         //RemainingQuantity = p.remaining_quantity,
                                                         //Unit = p.unit,
                                                         //ExtraPercent = p.extra_charge_percentage,
                                                         //ExtraPrice = p.extra_charge_price,
                                                         Invoice_No = a.Invoice_No,

                                                         IsPromocodeApplied = a.is_promocode_applied,
                                                         Promocode = a.promocode,
                                                         Promocode_Discount = a.promocode_discount,
                                                         Promocode_Amount = a.promocode_amount,
                                                         Delivery_charge = a.delivery_charge,
                                                         pickup_address_type = a.pickup_address_type,
                                                         delivery_address_type = a.delivery_address_type,
                                                     }).OrderByDescending(x => x.OrderID).ToList();
                    foreach (PurchaseOrder_Model ord in Res)
                    {
                        //int ordercount = Context.t_order_details.Where(c => c.order_id == ord.OrderID).Count();
                        //if (ordercount > 0)
                        //{
                        ord.PurchaseOrderDetail = (from x in Context.t_order_details
                                                   where x.order_id == ord.OrderID
                                                   select new PurchaseOrderDetail
                                                   {
                                                       OrderDetailsID = x.order_details_id,
                                                       OrderID = x.order_id,
                                                       CategoryID = x.t_product.t_category.category_id,
                                                       CategoryName = x.t_product.t_category.category_name,
                                                       ProductID = x.product_id,
                                                       ProductName = x.t_product.product_name,
                                                       //UnitPrice = Context.t_service_price.FirstOrDefault(p => p.service_id == x.service_id && p.product_id == x.product_id).price : null,
                                                       UnitPrice = (x.total_price - x.extra_charge_price) / x.quantity,
                                                       Price = x.total_price,
                                                       Quantity = x.quantity,
                                                       DeliveredQuantity = x.delivered_quantity,
                                                       RemainingQuantity = x.remaining_quantity,
                                                       ExtraPercent = x.extra_charge_percentage,
                                                       ExtraPrice = x.extra_charge_price,
                                                       ServiceID = x.service_id,
                                                       ServiceName = x.t_service.service_name,

                                                   }).ToList();

                        // }
                    }




                    ObjRequest = Res;
                    return ObjRequest;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long AcceptCancelOrder(PurchaseOrder_Model Objpo)
        {
            long IRes = 0;
            try
            {
                if (Objpo.OrderID > 0 && Objpo.PickedbyID > 0 && Objpo.Action.ToLower() == "acceptpickup")
                {
                    IRes = acceptpickuporder(Objpo);
                }
                else if (Objpo.OrderID > 0 && Objpo.PickedbyID > 0 && Objpo.Action.ToLower() == "cancelpickup")
                {
                    IRes = cancelpickuporder(Objpo);
                }
                else if (Objpo.OrderID > 0 && Objpo.DeliveredbyID > 0 && Objpo.Action.ToLower() == "acceptdelivery")
                {
                    IRes = acceptdeliveryuporder(Objpo);
                }
                else if (Objpo.OrderID > 0 && Objpo.DeliveredbyID > 0 && Objpo.Action.ToLower() == "canceldelivery")
                {
                    IRes = canceldeliveryuporder(Objpo);
                }
                return IRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private long acceptpickuporder(PurchaseOrder_Model Objpo)
        {
            long IRes = 0;
            try
            {
                using (API.DataAccess.maxcleanEntities context = new API.DataAccess.maxcleanEntities())
                {

                    var ord = context.t_orders.FirstOrDefault(x => x.order_id == Objpo.OrderID);
                    if (ord.pickedup_by == null)
                    {
                        var DeliveryboyExist = context.t_delivery_boy.FirstOrDefault(x => x.id == Objpo.PickedbyID);
                        if (ord == null || DeliveryboyExist == null)
                        {
                            IRes = -4;
                        }
                        else
                        {

                            ord.pickedup_by = Objpo.PickedbyID;
                            ord.order_status = 3;
                            context.SaveChanges();
                            IRes = ord.order_id;
                        }
                    }
                    else
                    {
                        IRes = -6;
                    }
                }
                return IRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private long acceptdeliveryuporder(PurchaseOrder_Model Objpo)
        {
            long IRes = 0;
            try
            {
                using (API.DataAccess.maxcleanEntities context = new API.DataAccess.maxcleanEntities())
                {
                    var ord = context.t_orders.FirstOrDefault(x => x.order_id == Objpo.OrderID);
                    if (ord.delivered_by == null)
                    {
                        var DeliveryboyExist = context.t_delivery_boy.FirstOrDefault(x => x.id == Objpo.DeliveredbyID);
                        if (ord == null || DeliveryboyExist == null)
                        {
                            IRes = -4;
                        }
                        else
                        {
                            ord.delivered_by = Objpo.DeliveredbyID;
                            ord.order_status = 9;
                            context.SaveChanges();
                            IRes = ord.order_id;
                        }
                    }
                    else
                    {
                        IRes = -6;
                    }
                }
                return IRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private long cancelpickuporder(PurchaseOrder_Model Objpo)
        {
            long IRes = 0;
            try
            {
                using (API.DataAccess.maxcleanEntities context = new API.DataAccess.maxcleanEntities())
                {
                    var ord = context.t_orders.FirstOrDefault(x => x.order_id == Objpo.OrderID);

                    var DeliveryboyExist = context.t_delivery_boy.FirstOrDefault(x => x.id == Objpo.PickedbyID);
                    if (ord == null || DeliveryboyExist == null)
                    {
                        IRes = -4;
                    }
                    else
                    {
                        ord.pickedup_by = null;
                        ord.order_status = 2;
                        context.SaveChanges();
                        IRes = ord.order_id;
                    }

                }
                return IRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private long canceldeliveryuporder(PurchaseOrder_Model Objpo)
        {
            long IRes = 0;
            try
            {
                using (API.DataAccess.maxcleanEntities context = new API.DataAccess.maxcleanEntities())
                {
                    var ord = context.t_orders.FirstOrDefault(x => x.order_id == Objpo.OrderID);
                    var DeliveryboyExist = context.t_delivery_boy.FirstOrDefault(x => x.id == Objpo.DeliveredbyID);
                    if (ord == null || DeliveryboyExist == null)
                    {
                        IRes = -4;
                    }
                    else
                    {
                        ord.delivered_by = null;
                        ord.order_status = 8;
                        context.SaveChanges();
                        IRes = ord.order_id;
                    }

                }
                return IRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PurchaseOrder_Model> GetPickupAcceptedOrders(int DeliveryBoyID)
        {
            List<PurchaseOrder_Model> ObjRequest = new List<PurchaseOrder_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())//3, pic = dbid
                {
                    var Res = (from a in Context.t_orders
                               where a.order_status == 3 && a.pickedup_by == DeliveryBoyID
                               select new PurchaseOrder_Model
                               {


                                   OrderID = a.order_id,
                                   CustomerID = a.customer_id,
                                   CustomerName = a.t_customer.customer_name,
                                   CustomerMobile = a.t_customer.mobile_no,
                                   CustomerAddress = a.t_customer.address,
                                   StoreID = a.store_id,
                                   StoreName = a.t_store.store_name,
                                   StoreMobileNo = a.t_store.mobile_no,
                                   Store_code = a.t_store.store_code,
                                   StoreAddress = a.t_store.address,
                                   ConfirmedBy = a.confirmed_by,
                                   ConfirmedUser = a.t_user.user_name,
                                   PickedbyID = a.pickedup_by,
                                   Pickedby = a.t_delivery_boy.name,
                                   DeliveredbyID = a.delivered_by,
                                   ExpressChargeApplied = a.express_delivery_price,
                                   OldBalanceApplied = a.old_balance,
                                   RewardPointsApplied = a.rewardsapplied,
                                   PickupDate = a.pickup_date,

                                   DeliveryboyName = (a.delivered_by != null) ? Context.t_delivery_boy.FirstOrDefault(p => p.id == a.delivered_by).name : null,
                                   OrderDate = a.order_date,
                                   ExpectingDeliveryDate = a.expected_delivery_date,
                                   ActualDeliveryDate = a.delivery_date,
                                   DeliveryAddress = a.delivery_address,
                                   PickupTime = a.pickup_time,
                                   DeliveryTime = a.delivery_time,
                                   PaidAmount = a.paid_amount,
                                   Advance_Amount = a.advance_amount,
                                   RemainingAmount = a.balance_amount,
                                   GrandTotal = a.grandtotal_price,
                                   PickupAddress = a.pickup_address,
                                   orderstatusID = a.order_status,
                                   OrderStatus = a.t_order_cylce.order_cycle_stage,
                                   Remark = a.remark,
                                   PaymentStatus = a.payment_Status,
                                   PaymentType = a.payment_type,
                                   //OrderDetailsID = p.order_details_id,
                                   //CategoryName = p.t_product.t_category.category_name,
                                   //Price = p.total_price,
                                   //CategoryID = p.t_product.t_category.category_id,

                                   //ServiceID = p.service_id,
                                   //ServiceName = p.t_service.service_name,
                                   //ProductID = p.product_id,
                                   //ProductName = p.t_product.product_name,
                                   // UnitPrice = (a.service_id != null && a.product_id != null) ? Context.t_service_price.FirstOrDefault(p => p.service_id == a.service_id && p.product_id == a.product_id).price : null,
                                   IsExpressDelivery = a.is_express_delivery,

                                   //Quantity = p.quantity,
                                   //DeliveredQuantity = p.delivered_quantity,
                                   //RemainingQuantity = p.remaining_quantity,
                                   //Unit = p.unit,
                                   //ExtraPercent = p.extra_charge_percentage,
                                   //ExtraPrice = p.extra_charge_price,
                                   Invoice_No = a.Invoice_No,

                                   IsPromocodeApplied = a.is_promocode_applied,
                                   Promocode = a.promocode,
                                   Promocode_Discount = a.promocode_discount,
                                   Promocode_Amount = a.promocode_amount,
                                   Delivery_charge = a.delivery_charge,
                                   pickup_address_type = a.pickup_address_type,
                                   delivery_address_type = a.delivery_address_type,

                               }).OrderByDescending(x => x.OrderID).ToList();
                    foreach (PurchaseOrder_Model ord in Res)
                    {
                        //int ordercount = Context.t_order_details.Where(c => c.order_id == ord.OrderID).Count();
                        //if (ordercount > 0)
                        //{
                        ord.PurchaseOrderDetail = (from x in Context.t_order_details
                                                   where x.order_id == ord.OrderID
                                                   select new PurchaseOrderDetail
                                                   {
                                                       OrderDetailsID = x.order_details_id,
                                                       OrderID = x.order_id,
                                                       CategoryID = x.t_product.t_category.category_id,
                                                       CategoryName = x.t_product.t_category.category_name,
                                                       ProductID = x.product_id,
                                                       ProductName = x.t_product.product_name,
                                                       //UnitPrice = Context.t_service_price.FirstOrDefault(p => p.service_id == x.service_id && p.product_id == x.product_id).price : null,
                                                       UnitPrice = (x.total_price - x.extra_charge_price) / x.quantity,
                                                       Price = x.total_price,
                                                       Quantity = x.quantity,
                                                       DeliveredQuantity = x.delivered_quantity,
                                                       RemainingQuantity = x.remaining_quantity,
                                                       ExtraPercent = x.extra_charge_percentage,
                                                       ExtraPrice = x.extra_charge_price,
                                                       ServiceID = x.service_id,
                                                       ServiceName = x.t_service.service_name,

                                                   }).ToList();

                        //}
                    }

                    ObjRequest = Res;
                    return ObjRequest;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PurchaseOrder_Model> GetDeliveryAcceptedOrders(int DeliveryBoyID)//9 , delby==dbid
        {
            List<PurchaseOrder_Model> ObjRequest = new List<PurchaseOrder_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Res = (from a in Context.t_orders
                               where a.order_status == 9 && a.delivered_by == DeliveryBoyID
                               select new PurchaseOrder_Model
                               {

                                   OrderID = a.order_id,
                                   CustomerID = a.customer_id,
                                   CustomerName = a.t_customer.customer_name,
                                   CustomerMobile = a.t_customer.mobile_no,
                                   CustomerAddress = a.t_customer.address,
                                   StoreID = a.store_id,
                                   StoreName = a.t_store.store_name,
                                   StoreMobileNo = a.t_store.mobile_no,
                                   StoreAddress = a.t_store.address,
                                   Store_code = a.t_store.store_code,
                                   ConfirmedBy = a.confirmed_by,
                                   ConfirmedUser = a.t_user.user_name,
                                   PickedbyID = a.pickedup_by,
                                   Pickedby = a.t_delivery_boy.name,
                                   DeliveredbyID = a.delivered_by,
                                   ExpressChargeApplied = a.express_delivery_price,
                                   OldBalanceApplied = a.old_balance,
                                   RewardPointsApplied = a.rewardsapplied,
                                   PickupDate = a.pickup_date,

                                   DeliveryboyName = (a.delivered_by != null) ? Context.t_delivery_boy.FirstOrDefault(p => p.id == a.delivered_by).name : null,
                                   OrderDate = a.order_date,
                                   ExpectingDeliveryDate = a.expected_delivery_date,
                                   ActualDeliveryDate = a.delivery_date,
                                   DeliveryAddress = a.delivery_address,
                                   PickupTime = a.pickup_time,
                                   DeliveryTime = a.delivery_time,
                                   PaidAmount = a.paid_amount,
                                   Advance_Amount = a.advance_amount,
                                   RemainingAmount = a.balance_amount,
                                   GrandTotal = a.grandtotal_price,
                                   PickupAddress = a.pickup_address,
                                   orderstatusID = a.order_status,
                                   OrderStatus = a.t_order_cylce.order_cycle_stage,
                                   Remark = a.remark,
                                   PaymentStatus = a.payment_Status,
                                   PaymentType = a.payment_type,
                                   //OrderDetailsID = p.order_details_id,
                                   //CategoryName = p.t_product.t_category.category_name,
                                   //Price = p.total_price,
                                   //CategoryID = p.t_product.t_category.category_id,

                                   //ServiceID = p.service_id,
                                   //ServiceName = p.t_service.service_name,
                                   //ProductID = p.product_id,
                                   //ProductName = p.t_product.product_name,
                                   // UnitPrice = (a.service_id != null && a.product_id != null) ? Context.t_service_price.FirstOrDefault(p => p.service_id == a.service_id && p.product_id == a.product_id).price : null,
                                   IsExpressDelivery = a.is_express_delivery,

                                   //Quantity = p.quantity,
                                   //DeliveredQuantity = p.delivered_quantity,
                                   //RemainingQuantity = p.remaining_quantity,
                                   //Unit = p.unit,
                                   //ExtraPercent = p.extra_charge_percentage,
                                   //ExtraPrice = p.extra_charge_price,
                                   Invoice_No = a.Invoice_No,

                                   IsPromocodeApplied = a.is_promocode_applied,
                                   Promocode = a.promocode,
                                   Promocode_Discount = a.promocode_discount,
                                   Promocode_Amount = a.promocode_amount,
                                   Delivery_charge = a.delivery_charge,
                                   pickup_address_type = a.pickup_address_type,
                                   delivery_address_type = a.delivery_address_type,

                               }).OrderByDescending(x => x.OrderID).ToList();
                    // var Order = Res.GroupBy(x => x.OrderID).Select(x => x.FirstOrDefault());
                    foreach (PurchaseOrder_Model ord in Res)
                    {
                        //int ordercount = Context.t_order_details.Where(c => c.order_id == ord.OrderID).Count();
                        //if (ordercount > 0)
                        //{
                        ord.PurchaseOrderDetail = (from x in Context.t_order_details
                                                   where x.order_id == ord.OrderID
                                                   select new PurchaseOrderDetail
                                                   {
                                                       OrderDetailsID = x.order_details_id,
                                                       OrderID = x.order_id,
                                                       CategoryID = x.t_product.t_category.category_id,
                                                       CategoryName = x.t_product.t_category.category_name,
                                                       ProductID = x.product_id,
                                                       ProductName = x.t_product.product_name,
                                                       //UnitPrice = Context.t_service_price.FirstOrDefault(p => p.service_id == x.service_id && p.product_id == x.product_id).price : null,
                                                       UnitPrice = (x.total_price - x.extra_charge_price) / x.quantity,
                                                       Price = x.total_price,
                                                       Quantity = x.quantity,
                                                       DeliveredQuantity = x.delivered_quantity,
                                                       RemainingQuantity = x.remaining_quantity,
                                                       ExtraPercent = x.extra_charge_percentage,
                                                       ExtraPrice = x.extra_charge_price,
                                                       ServiceID = x.service_id,
                                                       ServiceName = x.t_service.service_name,

                                                   }).ToList();

                        // }
                    }

                    ObjRequest = Res;
                    return ObjRequest;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PurchaseOrder_Model> GetAllDeliveryReadyOrders()
        {
            List<PurchaseOrder_Model> ObjRequest = new List<PurchaseOrder_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Res = (from a in Context.t_orders
                               where a.order_status == 8
                               select new PurchaseOrder_Model
                               {


                                   OrderID = a.order_id,
                                   CustomerID = a.customer_id,
                                   CustomerName = a.t_customer.customer_name,
                                   CustomerMobile = a.t_customer.mobile_no,
                                   CustomerAddress = a.t_customer.address,
                                   StoreID = a.store_id,
                                   StoreName = a.t_store.store_name,
                                   StoreMobileNo = a.t_store.mobile_no,
                                   Store_code = a.t_store.store_code,
                                   StoreAddress = a.t_store.address,
                                   ConfirmedBy = a.confirmed_by,
                                   ConfirmedUser = a.t_user.user_name,
                                   PickedbyID = a.pickedup_by,
                                   Pickedby = a.t_delivery_boy.name,
                                   DeliveredbyID = a.delivered_by,
                                   ExpressChargeApplied = a.express_delivery_price,
                                   OldBalanceApplied = a.old_balance,
                                   RewardPointsApplied = a.rewardsapplied,
                                   PickupDate = a.pickup_date,

                                   DeliveryboyName = (a.delivered_by != null) ? Context.t_delivery_boy.FirstOrDefault(p => p.id == a.delivered_by).name : null,
                                   OrderDate = a.order_date,
                                   ExpectingDeliveryDate = a.expected_delivery_date,
                                   ActualDeliveryDate = a.delivery_date,
                                   DeliveryAddress = a.delivery_address,
                                   PickupTime = a.pickup_time,
                                   DeliveryTime = a.delivery_time,
                                   PaidAmount = a.paid_amount,
                                   Advance_Amount = a.advance_amount,
                                   RemainingAmount = a.balance_amount,
                                   GrandTotal = a.grandtotal_price,
                                   PickupAddress = a.pickup_address,
                                   orderstatusID = a.order_status,
                                   OrderStatus = a.t_order_cylce.order_cycle_stage,
                                   Remark = a.remark,
                                   PaymentStatus = a.payment_Status,
                                   PaymentType = a.payment_type,
                                   //OrderDetailsID = p.order_details_id,
                                   //CategoryName = p.t_product.t_category.category_name,
                                   //Price = p.total_price,
                                   //CategoryID = p.t_product.t_category.category_id,

                                   //ServiceID = p.service_id,
                                   //ServiceName = p.t_service.service_name,
                                   //ProductID = p.product_id,
                                   //ProductName = p.t_product.product_name,
                                   // UnitPrice = (a.service_id != null && a.product_id != null) ? Context.t_service_price.FirstOrDefault(p => p.service_id == a.service_id && p.product_id == a.product_id).price : null,
                                   IsExpressDelivery = a.is_express_delivery,

                                   //Quantity = p.quantity,
                                   //DeliveredQuantity = p.delivered_quantity,
                                   //RemainingQuantity = p.remaining_quantity,
                                   //Unit = p.unit,
                                   //ExtraPercent = p.extra_charge_percentage,
                                   //ExtraPrice = p.extra_charge_price,
                                   Invoice_No = a.Invoice_No,

                                   IsPromocodeApplied = a.is_promocode_applied,
                                   Promocode = a.promocode,
                                   Promocode_Discount = a.promocode_discount,
                                   Promocode_Amount = a.promocode_amount,
                                   Delivery_charge = a.delivery_charge,
                                   pickup_address_type = a.pickup_address_type,
                                   delivery_address_type = a.delivery_address_type,



                               }).OrderByDescending(x => x.OrderID).ToList();
                    //var Order = Res.GroupBy(x => x.OrderID).Select(x => x.FirstOrDefault());
                    foreach (PurchaseOrder_Model ord in Res)
                    {
                        //int ordercount = Context.t_order_details.Where(c => c.order_id == ord.OrderID).Count();
                        //if (ordercount > 0)
                        //{
                        ord.PurchaseOrderDetail = (from x in Context.t_order_details
                                                   where x.order_id == ord.OrderID
                                                   select new PurchaseOrderDetail
                                                   {
                                                       OrderDetailsID = x.order_details_id,
                                                       OrderID = x.order_id,
                                                       CategoryID = x.t_product.t_category.category_id,
                                                       CategoryName = x.t_product.t_category.category_name,
                                                       ProductID = x.product_id,
                                                       ProductName = x.t_product.product_name,
                                                       //UnitPrice = Context.t_service_price.FirstOrDefault(p => p.service_id == x.service_id && p.product_id == x.product_id).price : null,
                                                       UnitPrice = (x.total_price - x.extra_charge_price) / x.quantity,
                                                       Price = x.total_price,
                                                       Quantity = x.quantity,
                                                       DeliveredQuantity = x.delivered_quantity,
                                                       RemainingQuantity = x.remaining_quantity,
                                                       ExtraPercent = x.extra_charge_percentage,
                                                       ExtraPrice = x.extra_charge_price,
                                                       ServiceID = x.service_id,
                                                       ServiceName = x.t_service.service_name,

                                                   }).ToList();

                        // }
                    }

                    ObjRequest = Res;
                    return ObjRequest;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PurchaseOrder_Model> GetAllPickedUpOrders(int DeliveryBoyID)
        {
            List<PurchaseOrder_Model> obj = new List<PurchaseOrder_Model>();

            try
            {
                using (maxcleanEntities context = new maxcleanEntities())
                {
                    using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                    {
                        var Res = (from a in Context.t_orders
                                   where a.order_status == 4 && a.pickedup_by == DeliveryBoyID
                                   select new PurchaseOrder_Model
                                   {

                                       OrderID = a.order_id,
                                       CustomerID = a.customer_id,
                                       CustomerName = a.t_customer.customer_name,
                                       CustomerMobile = a.t_customer.mobile_no,
                                       CustomerAddress = a.t_customer.address,
                                       StoreID = a.store_id,
                                       StoreName = a.t_store.store_name,
                                       StoreMobileNo = a.t_store.mobile_no,
                                       StoreAddress = a.t_store.address,
                                       Store_code = a.t_store.store_code,
                                       ConfirmedBy = a.confirmed_by,
                                       ConfirmedUser = a.t_user.user_name,
                                       PickedbyID = a.pickedup_by,
                                       Pickedby = a.t_delivery_boy.name,
                                       DeliveredbyID = a.delivered_by,
                                       ExpressChargeApplied = a.express_delivery_price,
                                       OldBalanceApplied = a.old_balance,
                                       RewardPointsApplied = a.rewardsapplied,
                                       PickupDate = a.pickup_date,

                                       DeliveryboyName = (a.delivered_by != null) ? Context.t_delivery_boy.FirstOrDefault(p => p.id == a.delivered_by).name : null,
                                       OrderDate = a.order_date,
                                       ExpectingDeliveryDate = a.expected_delivery_date,
                                       ActualDeliveryDate = a.delivery_date,
                                       DeliveryAddress = a.delivery_address,
                                       PickupTime = a.pickup_time,
                                       DeliveryTime = a.delivery_time,
                                       PaidAmount = a.paid_amount,
                                       Advance_Amount = a.advance_amount,
                                       RemainingAmount = a.balance_amount,
                                       GrandTotal = a.grandtotal_price,
                                       PickupAddress = a.pickup_address,
                                       orderstatusID = a.order_status,
                                       OrderStatus = a.t_order_cylce.order_cycle_stage,
                                       Remark = a.remark,
                                       PaymentStatus = a.payment_Status,
                                       PaymentType = a.payment_type,
                                       //OrderDetailsID = p.order_details_id,
                                       //CategoryName = p.t_product.t_category.category_name,
                                       //Price = p.total_price,
                                       //CategoryID = p.t_product.t_category.category_id,

                                       //ServiceID = p.service_id,
                                       //ServiceName = p.t_service.service_name,
                                       //ProductID = p.product_id,
                                       //ProductName = p.t_product.product_name,
                                       // UnitPrice = (a.service_id != null && a.product_id != null) ? Context.t_service_price.FirstOrDefault(p => p.service_id == a.service_id && p.product_id == a.product_id).price : null,
                                       IsExpressDelivery = a.is_express_delivery,

                                       //Quantity = p.quantity,
                                       //DeliveredQuantity = p.delivered_quantity,
                                       //RemainingQuantity = p.remaining_quantity,
                                       //Unit = p.unit,
                                       //ExtraPercent = p.extra_charge_percentage,
                                       //ExtraPrice = p.extra_charge_price,
                                       Invoice_No = a.Invoice_No,

                                       IsPromocodeApplied = a.is_promocode_applied,
                                       Promocode = a.promocode,
                                       Promocode_Discount = a.promocode_discount,
                                       Promocode_Amount = a.promocode_amount,
                                       Delivery_charge = a.delivery_charge,
                                       pickup_address_type = a.pickup_address_type,
                                       delivery_address_type = a.delivery_address_type,



                                   }).OrderByDescending(x => x.OrderID).ToList();

                        foreach (PurchaseOrder_Model ord in Res)
                        {
                            //int ordercount = Context.t_order_details.Where(c => c.order_id == ord.OrderID).Count();
                            //if (ordercount > 0)
                            //{
                            ord.PurchaseOrderDetail = (from x in Context.t_order_details
                                                       where x.order_id == ord.OrderID
                                                       select new PurchaseOrderDetail
                                                       {
                                                           OrderDetailsID = x.order_details_id,
                                                           OrderID = x.order_id,
                                                           CategoryID = x.t_product.t_category.category_id,
                                                           CategoryName = x.t_product.t_category.category_name,
                                                           ProductID = x.product_id,
                                                           ProductName = x.t_product.product_name,
                                                           //UnitPrice = Context.t_service_price.FirstOrDefault(p => p.service_id == x.service_id && p.product_id == x.product_id).price : null,
                                                           UnitPrice = (x.total_price - x.extra_charge_price) / x.quantity,
                                                           Price = x.total_price,
                                                           Quantity = x.quantity,
                                                           DeliveredQuantity = x.delivered_quantity,
                                                           RemainingQuantity = x.remaining_quantity,
                                                           ExtraPercent = x.extra_charge_percentage,
                                                           ExtraPrice = x.extra_charge_price,
                                                           ServiceID = x.service_id,
                                                           ServiceName = x.t_service.service_name,

                                                       }).ToList();

                            // }
                        }

                        // ObjRequest = Res;

                        obj = Res;

                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long SubmitPickedOrders(OrderList_Model obj)
        {
            long IRes = 0;
            try
            {

                using (maxcleanEntities context = new maxcleanEntities())
                {
                    foreach (var ord in obj.Orders)
                    {
                        var orderExist = context.t_orders.FirstOrDefault(x => x.order_id == ord.OrderID);
                        if (orderExist == null)
                        {
                            IRes = -4;
                        }
                        else
                        {

                            orderExist.pickedup_by = obj.PickedbyID;
                            orderExist.order_status = 5;


                            orderExist.is_tagged = true;
                            var tag_exist = context.t_taggs.FirstOrDefault(x => x.order_id == ord.OrderID);
                            int? total_quant = orderExist.t_order_details.Where(x => x.order_id == ord.OrderID).Sum(c => c.quantity);
                            if (tag_exist == null)
                            {
                                t_taggs tag = new t_taggs();
                                tag.order_id = ord.OrderID;
                                tag.tag_number = ord.OrderID + "/" + total_quant;
                                tag.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                                context.t_taggs.Add(tag);
                            }


                            orderExist.Received_at_store_by = obj.ReceivedOrder_AtStore_by;
                            if (obj.Admin_Sign_At_pickup != null)
                                orderExist.admin_sign_at_pickup = API.CommonClasses.SaveBase64File.SaveBase64Image(ord.OrderID, "Admin_Sign_at_Pickup", "Admin", obj.Admin_Sign_At_pickup, ".jpg");

                            orderExist.store_id = obj.StoreID;

                            var incent = context.t_deliveryboy_incentives.FirstOrDefault(x => x.delivery_boy_id == obj.PickedbyID);
                            if (incent != null)
                            {
                                incent.incentives = incent.incentives + StaticData.Incentive;
                                incent.balance = incent.balance + StaticData.Incentive;
                            }
                            context.SaveChanges();


                            IRes = ord.OrderID;
                        }
                    }
                }
                return IRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public long SubmitAtdelivery(PurchaseOrder_Model obj)
        {
            long IRes = 0;
            int? Remainingquant = 0;
            try
            {
                using (maxcleanEntities context = new maxcleanEntities())
                {
                    var IDExist = context.t_orders.FirstOrDefault(x => x.order_id == obj.OrderID);

                    if (IDExist == null)
                    {
                        IRes = -4;
                    }
                    else
                    {
                        IDExist.delivered_by = obj.DeliveredbyID;
                        IDExist.cust_sign_at_delivery = API.CommonClasses.SaveBase64File.SaveBase64Image(obj.OrderID, "Cust_Sign_at_Delivery", "Customer", obj.Custm_Sign_At_Delivery, ".jpg");

                        IDExist.paid_amount = IDExist.paid_amount + obj.PaidAmount;
                        IDExist.balance_amount = IDExist.balance_amount - obj.PaidAmount;

                        if (IDExist.paid_amount == 0)
                        {
                            IDExist.payment_Status = "pending";
                        }
                        else if (IDExist.paid_amount == IDExist.grandtotal_price)
                        {
                            IDExist.payment_Status = "complete";
                        }
                        else
                        {
                            IDExist.payment_Status = "partial";
                        }
                        /////*******incent*********////
                        var incent = context.t_deliveryboy_incentives.FirstOrDefault(x => x.delivery_boy_id == obj.DeliveredbyID);

                        if (incent != null)
                        {
                            incent.incentives = incent.incentives + StaticData.Incentive;
                            incent.balance = incent.balance + StaticData.Incentive;
                        }

                        /////////*********order_details*********///////
                        foreach (var ord in obj.PurchaseOrderDetail)
                        {
                            var orderdetails = context.t_order_details.FirstOrDefault(x => x.service_id == ord.ServiceID && x.product_id == ord.ProductID);
                            orderdetails.delivered_quantity = ord.DeliveredQuantity;
                            orderdetails.remaining_quantity = orderdetails.quantity - ord.DeliveredQuantity;
                            Remainingquant += orderdetails.quantity - ord.DeliveredQuantity;
                        }

                        ////////*************order_status*********//////

                        IDExist.order_status = 11;
                        IDExist.delivery_date = CommonClasses.StaticData.GetCurrentDataTime;
                        //////***************old balance************////
                        var custexist = context.t_customer.FirstOrDefault(x => x.customer_id == IDExist.customer_id);
                        if (custexist != null)
                        {
                            custexist.old_balance += IDExist.balance_amount;
                        }

                        //////********Rewards*******//////
                        updateRewards(IDExist.customer_id, IDExist.paid_amount);
                        context.SaveChanges();
                        IRes = obj.OrderID;

                        var customerdetails = context.t_customer.SingleOrDefault(x => x.customer_id == IDExist.customer_id);
                        var storedetails = context.t_store.SingleOrDefault(x => x.store_id == IDExist.store_id);

                        var Orderdetails = context.t_orders.SingleOrDefault(x => x.order_id == obj.OrderID);
                        var SMSbody = "Dear " + customerdetails.customer_name + "," + Environment.NewLine + "Order No.: " + Orderdetails.order_id + Environment.NewLine + "Quantity: " + Orderdetails.t_order_details.Sum(x => x.quantity) + Environment.NewLine + "Amount: " + Orderdetails.grandtotal_price + ".00 INR" + Environment.NewLine + "Status: Ready For Delivery" + Environment.NewLine + "For any queries, please reach out to 9900318000." + Environment.NewLine + "Order online from www.maxcleanlaundry.in" + Environment.NewLine + "Outlet: " + storedetails.store_name + Environment.NewLine + "Open Hours: 09:00 AM to 09:00 PM";
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
                }
                return IRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long SubmitDeliveryAccepted(OrderList_Model obj)
        {
            long IRes = 0;
            try
            {

                using (maxcleanEntities context = new maxcleanEntities())
                {
                    foreach (var ord in obj.Orders)
                    {
                        var orderExist = context.t_orders.FirstOrDefault(x => x.order_id == ord.OrderID);
                        if (orderExist == null)
                        {
                            IRes = -4;
                        }
                        else
                        {

                            orderExist.delivered_by = obj.DeliveredbyID;
                            orderExist.order_status = 10;
                            orderExist.sent_from_store_by = obj.DispatchedOrder_Atstore_by;
                            if (obj.Admin_Sign_At_Delivery != null)
                                orderExist.admin_sign_at_delivery = API.CommonClasses.SaveBase64File.SaveBase64Image(ord.OrderID, "Admin_Sign_at_Pickup", "Admin", obj.Admin_Sign_At_Delivery, ".jpg");

                            //orderExist.store_id = ord.StoreID;
                            context.SaveChanges();
                            IRes = ord.OrderID;
                        }
                    }
                }
                return IRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<PurchaseOrder_Model> GetDeliveryOrdersRecived(int DeliveryBoyID)//9 , delby==dbid
        {
            List<PurchaseOrder_Model> ObjRequest = new List<PurchaseOrder_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Res = (from a in Context.t_orders
                               where a.order_status == 10 && a.delivered_by == DeliveryBoyID
                               select new PurchaseOrder_Model
                               {

                                   OrderID = a.order_id,
                                   CustomerID = a.customer_id,
                                   CustomerName = a.t_customer.customer_name,
                                   CustomerMobile = a.t_customer.mobile_no,
                                   CustomerAddress = a.t_customer.address,
                                   StoreID = a.store_id,
                                   StoreName = a.t_store.store_name,
                                   StoreMobileNo = a.t_store.mobile_no,
                                   StoreAddress = a.t_store.address,
                                   Store_code = a.t_store.store_code,
                                   ConfirmedBy = a.confirmed_by,
                                   ConfirmedUser = a.t_user.user_name,
                                   PickedbyID = a.pickedup_by,
                                   Pickedby = a.t_delivery_boy.name,
                                   DeliveredbyID = a.delivered_by,
                                   ExpressChargeApplied = a.express_delivery_price,
                                   OldBalanceApplied = a.old_balance,
                                   RewardPointsApplied = a.rewardsapplied,
                                   PickupDate = a.pickup_date,

                                   DeliveryboyName = (a.delivered_by != null) ? Context.t_delivery_boy.FirstOrDefault(p => p.id == a.delivered_by).name : null,
                                   OrderDate = a.order_date,
                                   ExpectingDeliveryDate = a.expected_delivery_date,
                                   ActualDeliveryDate = a.delivery_date,
                                   DeliveryAddress = a.delivery_address,
                                   PickupTime = a.pickup_time,
                                   DeliveryTime = a.delivery_time,
                                   PaidAmount = a.paid_amount,
                                   Advance_Amount = a.advance_amount,
                                   RemainingAmount = a.balance_amount,
                                   GrandTotal = a.grandtotal_price,
                                   PickupAddress = a.pickup_address,
                                   orderstatusID = a.order_status,
                                   OrderStatus = a.t_order_cylce.order_cycle_stage,
                                   Remark = a.remark,
                                   PaymentStatus = a.payment_Status,
                                   PaymentType = a.payment_type,
                                   //OrderDetailsID = p.order_details_id,
                                   //CategoryName = p.t_product.t_category.category_name,
                                   //Price = p.total_price,
                                   //CategoryID = p.t_product.t_category.category_id,

                                   //ServiceID = p.service_id,
                                   //ServiceName = p.t_service.service_name,
                                   //ProductID = p.product_id,
                                   //ProductName = p.t_product.product_name,
                                   // UnitPrice = (a.service_id != null && a.product_id != null) ? Context.t_service_price.FirstOrDefault(p => p.service_id == a.service_id && p.product_id == a.product_id).price : null,
                                   IsExpressDelivery = a.is_express_delivery,

                                   //Quantity = p.quantity,
                                   //DeliveredQuantity = p.delivered_quantity,
                                   //RemainingQuantity = p.remaining_quantity,
                                   //Unit = p.unit,
                                   //ExtraPercent = p.extra_charge_percentage,
                                   //ExtraPrice = p.extra_charge_price,
                                   Invoice_No = a.Invoice_No,

                                   IsPromocodeApplied = a.is_promocode_applied,
                                   Promocode = a.promocode,
                                   Promocode_Discount = a.promocode_discount,
                                   Promocode_Amount = a.promocode_amount,
                                   Delivery_charge = a.delivery_charge,
                                   pickup_address_type = a.pickup_address_type,
                                   delivery_address_type = a.delivery_address_type,



                               }).OrderByDescending(x => x.OrderID).ToList();
                    // var Order = Res.GroupBy(x => x.OrderID).Select(x => x.FirstOrDefault());
                    foreach (PurchaseOrder_Model ord in Res)
                    {
                        //int ordercount = Context.t_order_details.Where(c => c.order_id == ord.OrderID).Count();
                        //if (ordercount > 0)
                        //{
                        ord.PurchaseOrderDetail = (from x in Context.t_order_details
                                                   where x.order_id == ord.OrderID
                                                   select new PurchaseOrderDetail
                                                   {
                                                       OrderDetailsID = x.order_details_id,
                                                       OrderID = x.order_id,
                                                       CategoryID = x.t_product.t_category.category_id,
                                                       CategoryName = x.t_product.t_category.category_name,
                                                       ProductID = x.product_id,
                                                       ProductName = x.t_product.product_name,
                                                       //UnitPrice = Context.t_service_price.FirstOrDefault(p => p.service_id == x.service_id && p.product_id == x.product_id).price : null,
                                                       UnitPrice = (x.total_price - x.extra_charge_price) / x.quantity,
                                                       Price = x.total_price,
                                                       Quantity = x.quantity,
                                                       DeliveredQuantity = x.delivered_quantity,
                                                       RemainingQuantity = x.remaining_quantity,
                                                       ExtraPercent = x.extra_charge_percentage,
                                                       ExtraPrice = x.extra_charge_price,
                                                       ServiceID = x.service_id,
                                                       ServiceName = x.t_service.service_name,

                                                   }).ToList();

                        //}
                    }

                    ObjRequest = Res;
                    return ObjRequest;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetAvailableTimeSlots(DateTime givendate)
        {
            String[] s1 = new String[10];
            try
            {
                List<TimeSlab_Model> timeslots = new List<TimeSlab_Model>();
                List<TimeSlab_Model> temp_timeslots = new List<TimeSlab_Model>();

                using (maxcleanEntities context = new maxcleanEntities())
                {
                    var orderlist = context.t_orders.Where(x => x.pickup_date == givendate.Date).ToList();
                    s1[0] = "orderlist executed";
                    if (StaticData.GetCurrentDataTime.Date == givendate.Date)
                    {

                        s1[1] = "if executed";
                        TimeSpan time = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo));
                        s1[2] = "time executed";
                        temp_timeslots = (from a in context.t_pickuptimeslabs
                                          where a.isActive == true && a.timevalue >= time
                                          select new TimeSlab_Model()
                                          {
                                              timeslab_id = a.timeslab_id,
                                              timeslab = a.timeslab,
                                              timevalue = a.timevalue
                                          }).ToList();
                        s1[3] = "time_slots executed";
                    }
                    else
                    {
                        // TimeSpan time = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo));
                        s1[4] = "else executed";
                        temp_timeslots = (from a in context.t_pickuptimeslabs
                                          where a.isActive == true
                                          select new TimeSlab_Model()
                                          {
                                              timeslab_id = a.timeslab_id,
                                              timeslab = a.timeslab,
                                              timevalue = a.timevalue
                                          }).ToList();
                        s1[5] = "time_slots executed";
                    }
                    foreach (var timeslot in temp_timeslots)
                    {
                        int i = 0;
                        s1[6] = s1[6] + " time_slots for loop executed " + i + " ;";
                        int count = orderlist.Where(x => x.timeslab_id == timeslot.timeslab_id).ToList().Count();
                        s1[7] = s1[7] + " count recevied for index " + i + " ;";
                        if (count < StaticData.NoOfOrderPerTimeSlab)
                        {
                            timeslots.Add(timeslot);

                        }

                    }

                    s1[8] = " Finished ";

                }
                return timeslots;
            }
            catch (Exception ex)
            {
                return s1;
            }
        }

    }
}