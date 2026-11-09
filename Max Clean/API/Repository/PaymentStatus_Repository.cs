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
    public class PaymentStatus_Repository : IPaymentStatus_Repository
    {
        public long Add(PaymentStatus_Model ObjPO)
        {
            DateTime today = DateTime.Now; //For orderdate assignment
            long iRes = 0;
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    #region t_customer
                    t_paymentstatus ps = new t_paymentstatus();
                    ps.order_id = ObjPO.order_id;
                    ps.transaction_id = ObjPO.transaction_id;
                    ps.payment_method = ObjPO.payment_method;
                    ps.payment_status = ObjPO.payment_status;
                    ps.bank_name = ObjPO.bank_name;
                    ps.txn_date = ObjPO.txn_date;
                    ps.mode_of_payment = ObjPO.mode_of_payment;

                    Context.t_paymentstatus.Add(ps);
                    Context.SaveChanges();
                    ObjPO.paymentStatus_id = ps.paymentStatus_id;
                    iRes = ps.paymentStatus_id;
                    #endregion
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