using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class DeliveryBoyIncentives_Repository : DeliveryBoyIncentives_IRepository
    {
     
        public long AddDeliveryBoyIncentives(DeliveryBoyInsentives_Model Obj)
        {
            long iRes = 0;
            lock (Obj)
            {
                try
                {
                    using (maxcleanEntities Context = new maxcleanEntities())
                    {
                        Context.Configuration.ValidateOnSaveEnabled = false;

                        var db = Context.t_deliveryboy_incentives.SingleOrDefault(x => x.delivery_boy_id == Obj.DeliveryBoyID);
                        if (db == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (db.balance >= Obj.PaidAmount && Obj.PaidAmount > 0)
                            {
                                db.amount_paid = db.amount_paid + Obj.PaidAmount;
                                db.balance = db.incentives - db.amount_paid;
                                t_incentives_transaction it = new t_incentives_transaction();
                                it.deliveryboy_incentive_id = db.db_incentive_id;
                                it.amount_paid = Obj.PaidAmount;
                                it.payment_type = Obj.PaymentType;
                                it.note = Obj.Note;
                                it.transaction_date = API.CommonClasses.StaticData.GetCurrentDataTime;
                               
                                Context.t_incentives_transaction.Add(it);
                                Context.SaveChanges();
                                iRes = db.db_incentive_id;
                               // DeliveryBoyIncentives_Repository ObjVendorProductOrderPayment_Repository = new DeliveryBoyIncentives_Repository();
                                //ObjVendorProductOrderPayment_Repository.(Objvpot.VendorPaymentOrderID);
                            }
                            else
                            {
                                iRes = -5;
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
        }

        public DeliveryBoyInsentives_Model GetAllTransactionByDeliveryBoyID(long DBID)
        {
            DeliveryBoyInsentives_Model obj = new DeliveryBoyInsentives_Model();
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    var objlist = (from a in Context.t_incentives_transaction
                                       where a.t_deliveryboy_incentives.delivery_boy_id == DBID
                                       select new
                                       {
                                           IncentiveTransactionID = a.incentive_transaction_id,
                                           PaidAmountTrs = a.amount_paid,
                                           PaymentTypeTrs = a.payment_type,
                                           Note = a.note,
                                           TransactionDate = a.transaction_date,

                                           DeliveryBoyIncentiveID = a.deliveryboy_incentive_id,
                                           DeliveryBoyID = a.t_deliveryboy_incentives.delivery_boy_id,
                                           DeliveryBoyName = a.t_deliveryboy_incentives.t_delivery_boy.name,

                                           GrandTotal=a.t_deliveryboy_incentives.incentives,
                                           PaidAmount = a.t_deliveryboy_incentives.amount_paid,
                                           RemainingAmount = a.t_deliveryboy_incentives.balance,
                                          
                                       }).ToList();
                   
                        var RequestOrderList = objlist.GroupBy(x => x.DeliveryBoyIncentiveID).Select(x => x.FirstOrDefault()).ToList();
                        var FilterData = RequestOrderList.Select(x => new DeliveryBoyInsentives_Model { DeliveryBoyIncentiveID = x.DeliveryBoyIncentiveID, DeliveryBoyID = x.DeliveryBoyID, DeliveryBoyName = x.DeliveryBoyName, PaidAmount = x.PaidAmount, RemainingAmount = x.RemainingAmount, GrandTotal = x.GrandTotal }).SingleOrDefault();
                        FilterData.TransactionList = objlist.Select(x => new PurchaseOrderPayment_Model { IncentiveTransactionID = x.IncentiveTransactionID, DeliveryBoyIncentiveID = x.DeliveryBoyIncentiveID, PaidAmountTrs = x.PaidAmountTrs, PaymentTypeTrs = x.PaymentTypeTrs, Note = x.Note, TransactionDate = x.TransactionDate }).ToList();
                        FilterData.TransactionList = FilterData.TransactionList.OrderByDescending(x => x.IncentiveTransactionID).ToList();
                        return FilterData;
                
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public DeliveryBoyInsentives_Model GetAllDeliveryBoyIncentivesByDeliveryBoyID(long DeliveryBoyID)
        //{
        //    DeliveryBoyInsentives_Model ObjDeliveryBoyIncentives = new DeliveryBoyInsentives_Model();
        //    try
        //    {
        //        using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
        //        {
        //            return ObjDeliveryBoyIncentives = (from a in Context.t_deliveryboy_incentives
        //                                    where a.delivery_boy_id == DeliveryBoyID
        //                                    select new DeliveryBoyInsentives_Model
        //                                    {
        //                                        DeliveryBoyIncentiveID = a.db_incentive_id,
        //                                        DeliveryBoyID = a.delivery_boy_id,
        //                                        PaidAmount = a.amount_paid,
        //                                        GrandTotal = a.incentives,
        //                                        RemainingAmount = a.balance,
                                                
        //                                    }).SingleOrDefault();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool SendInvoice(long DeliveryBoyID)
        //{
        //    try
        //    {
        //        using (maxcleanEntities Context = new maxcleanEntities())
        //        {
        //            var Res = GetAllTransactionByDeliveryBoyID(DeliveryBoyID);

        //            var deliverydetails = Context.t_delivery_boy.SingleOrDefault(x => x.id == Res.DeliveryBoyID);

        //            string HTML = new WebClient().DownloadString("http://api.brewlifecoffee.in/EmailTemplates/VendorPaymentCopy.html");
        //            HTML = HTML.Replace(@"$VENDORNAME$", deliverydetails.name);
        //            var DateTime = new DateTime();
        //            DateTime = Convert.ToDateTime(Res.ActualDeliveryDate);
        //            HTML = HTML.Replace(@"$VENDORMOBILE$", Res.VendorMobileNO);
        //            HTML = HTML.Replace(@"$STORENAME$", Res.StoreName);
        //            HTML = HTML.Replace(@"$STOREMOBILE$", Res.StoreMobileNo);
        //            HTML = HTML.Replace(@"$REQUESTEDORDERID$", "VD" + Res.VendorPaymentOrderID.ToString());
        //            HTML = HTML.Replace(@"$REQUESTEDQTY$", Res.RequestedQuantity.ToString());
        //            HTML = HTML.Replace(@"$PRICEPERKG$", Res.PricePerKG.ToString());
        //            HTML = HTML.Replace(@"$DELIVEREDON$", DateTime.ToString("dd-MM-yyyy hh:mm:ss tt"));
        //            HTML = HTML.Replace(@"$RECEIVEDQTY$", Res.ReceivedQuantity.ToString());
        //            HTML = HTML.Replace(@"$BALANCEAMOUNT$", Convert.ToString(Res.RemainingAmount));
        //            HTML = HTML.Replace(@"$GRANDTOTAL$", Res.GrandTotal.ToString());
        //            StringBuilder pt = new StringBuilder();
        //            foreach (var tran in Res.TransactionList)
        //            {
        //                pt.Append("<tr>");
        //                pt.Append("<td valign='middle' style='border-collapse:collapse;padding:8px 0;border-bottom:1px solid #eaeaed;font-size:12px'>" + tran.VendorPaymentOrderPaymentID + "</td>");
        //                pt.Append("<td valign='middle' style='border-collapse:collapse;padding:8px 0;border-bottom:1px solid #eaeaed;font-size:12px'>Cash</td>");
        //                pt.Append("<td valign='middle' style='border-collapse:collapse;padding:8px 0;border-bottom:1px solid #eaeaed;font-size:12px'>" + tran.PaidAmountTran + "</td>");
        //                pt.Append("<td valign='middle' style='border-collapse:collapse;padding:8px 0;border-bottom:1px solid #eaeaed;font-size:12px'>" + tran.TransactionDate.ToString("dd-MM-yyyy hh:mm:ss tt") + "</td>");
        //                pt.Append("<td valign='middle' style='border-collapse:collapse;padding:8px 0;border-bottom:1px solid #eaeaed;font-size:12px'>" + tran.Note + "</td>");
        //                pt.Append("</tr>");
        //            }
        //            HTML = HTML.Replace(@"$TRANSACTIONLIST$", pt.ToString());
        //            // MailTo,Subject,MailBody,CC,BCC
        //            return MailSender.SendMail(vendordetails.email_id, "Payment made for Invoice No.: VD" + RequestedOrderID + "", HTML, "nmreddy111@gmail.com", "");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
    }
}