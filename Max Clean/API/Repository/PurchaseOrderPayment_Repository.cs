using API.CommonClasses;
using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace API.Repository
{
    public class PurchaseOrderPayment_Repository : PurchaseOrderPayment_IRepository
    {
        public long AddPaymentToVendorProductOrder(PurchaseOrderPayment_Model Objvpot)
        {
            long iRes = 0;
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    var VPO = Context.t_vpo.SingleOrDefault(x => x.vpo_id == Objvpot.VendorPaymentOrderID);
                    if (VPO == null)
                    {
                        iRes = -4;
                    }
                    else
                    {
                        if (VPO.remaining_balance >= Objvpot.PaidAmountTran && Objvpot.PaidAmountTran > 0)
                        {
                            VPO.paid_amount = VPO.paid_amount + Objvpot.PaidAmountTran;
                            VPO.remaining_balance = VPO.remaining_balance - Objvpot.PaidAmountTran;
                            t_vpo_trans tvpot = new t_vpo_trans();
                            tvpot.vpo_id = VPO.vpo_id;
                            tvpot.paid_amount = Objvpot.PaidAmountTran;
                            tvpot.payment_type = Objvpot.PaymentType;
                            tvpot.note = Objvpot.Note;
                            tvpot.trans_date = API.CommonClasses.StaticData.GetCurrentDataTime;
                            tvpot.created_by = Objvpot.CreatedBy;
                            tvpot.created_date = API.CommonClasses.StaticData.GetCurrentDataTime;
                            Context.t_vpo_trans.Add(tvpot);
                            Context.SaveChanges();
                            iRes = VPO.vpo_id;
                            PurchaseOrderPayment_Repository ObjVendorProductOrderPayment_Repository = new PurchaseOrderPayment_Repository();
                            ObjVendorProductOrderPayment_Repository.SendPaymentMail(VPO.vpo_id);
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

        public List<VendorProductOrderPayment_ModelResult> GetAllTransactionByVendorID(long VendorID)
        {
            List<VendorProductOrderPayment_ModelResult> objvpot = new List<VendorProductOrderPayment_ModelResult>();
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    var objvpotlist = (from a in Context.t_vpo_trans
                                       where a.t_vpo.vendor_id == VendorID
                                       select new
                                       {
                                           VendorPaymentOrderPaymentID = a.vpo_trans_id,
                                           PaidAmountTran = a.paid_amount,
                                           PaymentType = a.payment_type,
                                           Note = a.note,
                                           TransactionDate = a.trans_date,

                                           VendorPaymentOrderID = a.vpo_id,
                                           VendorID = a.t_vpo.vendor_id,
                                           VendorName = a.t_vpo.t_vendor.vendor_name,
                                           VendorMobileNO = a.t_vpo.t_vendor.mobile_no,
                                           StoreID = a.t_vpo.store_id,
                                           StoreName = a.t_vpo.t_store.store_name,
                                           StoreMobileNo = a.t_vpo.t_store.mobile_no,
                                           PaidAmount = a.t_vpo.paid_amount,
                                           RemainingAmount = a.t_vpo.remaining_balance,
                                           GrandTotal = a.t_vpo.grand_total,
                                           ExpectingDeliveryDate = a.t_vpo.expecting_delivery_date,
                                           ActualDeliveryDate = a.t_vpo.actual_delivery_date,
                                           Remark = a.t_vpo.remark,
                                           IsActive = a.t_vpo.is_active,
                                           CreatedBy = a.t_vpo.created_by,
                                           CreatedDate = a.t_vpo.created_date,
                                           UpdateBy = a.t_vpo.updated_by,
                                           UpdateDate = a.t_vpo.updated_date
                                       }).ToList();
                    //var RequestOrderList = objvpotlist.GroupBy(x => x.VendorPaymentOrderID).Select(x => x.FirstOrDefault()).ToList();
                    //var FilterData = RequestOrderList.Select(x => new VendorProductOrderPayment_ModelResult { VendorPaymentOrderID = x.VendorPaymentOrderID, VendorID = x.VendorID, VendorName = x.VendorName, VendorMobileNO = x.VendorMobileNO, StoreID = x.StoreID, StoreName = x.StoreName, StoreMobileNo = x.StoreMobileNo, VendorOrderID = x.VendorOrderID, PaidAmount = x.PaidAmount, RemainingAmount = x.RemainingAmount, GrandTotal = x.GrandTotal, PricePerKG = x.PricePerKG, RequestedQuantity = x.RequestedQuantity, ReceivedQuantity = x.ReceivedQuantity, ExpectingDeliveryDate = x.ExpectingDeliveryDate, ActualDeliveryDate = x.ActualDeliveryDate, Remark = x.Remark, IsActive = x.IsActive, CreatedBy = x.CreatedBy, CreatedDate = x.CreatedDate, UpdateBy = x.UpdateBy, UpdateDate = x.UpdateDate }).ToList();
                    //foreach (var ROID in FilterData)
                    //{
                    //    var PaymentTran = objvpotlist.Where(x => x.VendorPaymentOrderID == ROID.VendorPaymentOrderID).Select(x => new VendorProductOrderPayment_Model { VendorPaymentOrderPaymentID = x.VendorPaymentOrderPaymentID, PaidAmountTran = x.PaidAmountTran, PaymentType = x.PaymentType, Note = x.Note, TransactionDate = x.TransactionDate }).ToList();
                    //    ROID.TransactionList = PaymentTran.OrderByDescending(x => x.VendorPaymentOrderPaymentID).ToList();
                    //}
                    //FilterData = FilterData.OrderByDescending(x => x.VendorPaymentOrderID).ToList();
                    return objvpot;
                    //return FilterData;
                }
            }
            catch (Exception ex) { throw ex; }

        }

        public VendorProductOrderPayment_ModelResult GetAllTransactionByVendorProductOrderID(long VPOID)
        {
            VendorProductOrderPayment_ModelResult objvpot = new VendorProductOrderPayment_ModelResult();
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    var objvpotlist = (from a in Context.t_vpo_trans
                                       where a.vpo_id == VPOID
                                       select new
                                       {
                                           VendorPaymentOrderPaymentID = a.vpo_trans_id,
                                           PaidAmountTran = a.paid_amount,
                                           PaymentType = a.payment_type,
                                           Note = a.note,
                                           TransactionDate = a.trans_date,

                                           VendorPaymentOrderID = a.vpo_id,
                                           VendorID = a.t_vpo.vendor_id,
                                           VendorName = a.t_vpo.t_vendor.vendor_name,
                                           VendorMobileNO = a.t_vpo.t_vendor.mobile_no,
                                           StoreID = a.t_vpo.store_id,
                                           StoreName = a.t_vpo.t_store.store_name,
                                           StoreMobileNo = a.t_vpo.t_store.mobile_no,
                                           PaidAmount = a.t_vpo.paid_amount,
                                           RemainingAmount = a.t_vpo.remaining_balance,
                                           GrandTotal = a.t_vpo.grand_total,
                                           ExpectingDeliveryDate = a.t_vpo.expecting_delivery_date,
                                           ActualDeliveryDate = a.t_vpo.actual_delivery_date,
                                           Remark = a.t_vpo.remark,
                                           IsActive = a.t_vpo.is_active,
                                           CreatedBy = a.t_vpo.created_by,
                                           CreatedDate = a.t_vpo.created_date,
                                           UpdateBy = a.t_vpo.updated_by,
                                           UpdateDate = a.t_vpo.updated_date
                                       }).ToList();
                    //objvpot = objvpotlist.Select(x => new VendorProductOrderPayment_ModelResult { VendorPaymentOrderID = x.VendorPaymentOrderID, VendorID = x.VendorID, VendorName = x.VendorName, VendorMobileNO = x.VendorMobileNO, StoreID = x.StoreID, StoreName = x.StoreName, StoreMobileNo = x.StoreMobileNo, VendorOrderID = x.VendorOrderID, PaidAmount = x.PaidAmount, RemainingAmount = x.RemainingAmount, GrandTotal = x.GrandTotal, PricePerKG = x.PricePerKG, RequestedQuantity = x.RequestedQuantity, ReceivedQuantity = x.ReceivedQuantity, ExpectingDeliveryDate = x.ExpectingDeliveryDate, ActualDeliveryDate = x.ActualDeliveryDate, Remark = x.Remark, IsActive = x.IsActive, CreatedBy = x.CreatedBy, CreatedDate = x.CreatedDate, UpdateBy = x.UpdateBy, UpdateDate = x.UpdateDate }).FirstOrDefault();
                    //var PaymentTran = objvpotlist.Select(x => new VendorProductOrderPayment_Model { VendorPaymentOrderPaymentID = x.VendorPaymentOrderPaymentID, PaidAmountTran = x.PaidAmountTran, PaymentType = x.PaymentType, Note = x.Note, TransactionDate = x.TransactionDate }).ToList();
                    //objvpot.TransactionList = PaymentTran.OrderByDescending(x => x.VendorPaymentOrderPaymentID).ToList();
                    return objvpot;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SendPaymentMail(long RequestedOrderID)
        {
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    var Res = GetAllTransactionByVendorProductOrderID(RequestedOrderID);

                    var vendordetails = Context.t_vendor.SingleOrDefault(x => x.vendor_id == Res.VendorID);

                    string HTML = new WebClient().DownloadString("http://api.brewlifecoffee.in/EmailTemplates/VendorPaymentCopy.html");
                    HTML = HTML.Replace(@"$VENDORNAME$", vendordetails.vendor_name);
                    var DateTime = new DateTime();
                    DateTime = Convert.ToDateTime(Res.ActualDeliveryDate);
                    HTML = HTML.Replace(@"$VENDORMOBILE$", Res.VendorMobileNO);
                    HTML = HTML.Replace(@"$STORENAME$", Res.StoreName);
                    HTML = HTML.Replace(@"$STOREMOBILE$", Res.StoreMobileNo);
                    HTML = HTML.Replace(@"$REQUESTEDORDERID$", "VD" + Res.VendorPaymentOrderID.ToString());
                    HTML = HTML.Replace(@"$REQUESTEDQTY$", Res.RequestedQuantity.ToString());
                    HTML = HTML.Replace(@"$PRICEPERKG$", Res.PricePerKG.ToString());
                    HTML = HTML.Replace(@"$DELIVEREDON$", DateTime.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    HTML = HTML.Replace(@"$RECEIVEDQTY$", Res.ReceivedQuantity.ToString());
                    HTML = HTML.Replace(@"$BALANCEAMOUNT$", Convert.ToString(Res.RemainingAmount));
                    HTML = HTML.Replace(@"$GRANDTOTAL$", Res.GrandTotal.ToString());
                    StringBuilder pt = new StringBuilder();
                    foreach (var tran in Res.TransactionList)
                    {
                        pt.Append("<tr>");
                        pt.Append("<td valign='middle' style='border-collapse:collapse;padding:8px 0;border-bottom:1px solid #eaeaed;font-size:12px'>" + tran.VendorPaymentOrderPaymentID + "</td>");
                        pt.Append("<td valign='middle' style='border-collapse:collapse;padding:8px 0;border-bottom:1px solid #eaeaed;font-size:12px'>Cash</td>");
                        pt.Append("<td valign='middle' style='border-collapse:collapse;padding:8px 0;border-bottom:1px solid #eaeaed;font-size:12px'>" + tran.PaidAmountTran + "</td>");
                        pt.Append("<td valign='middle' style='border-collapse:collapse;padding:8px 0;border-bottom:1px solid #eaeaed;font-size:12px'>" + tran.TransactionDate.ToString("dd-MM-yyyy hh:mm:ss tt") + "</td>");
                        pt.Append("<td valign='middle' style='border-collapse:collapse;padding:8px 0;border-bottom:1px solid #eaeaed;font-size:12px'>" + tran.Note + "</td>");
                        pt.Append("</tr>");
                    }
                    HTML = HTML.Replace(@"$TRANSACTIONLIST$", pt.ToString());
                    // MailTo,Subject,MailBody,CC,BCC
                    return MailSender.SendMail(vendordetails.email_id, "Payment made for Invoice No.: VD" + RequestedOrderID + "", HTML, "nmreddy111@gmail.com", "");
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}