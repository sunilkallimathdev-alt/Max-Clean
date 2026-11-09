using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Repository
{
    public class PettyCash_Repository : PettyCash_IRepository
    {
        // GET: PettyCash_Repository
        public int Add(PettyCash_Model ObjPCash)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities context = new DataAccess.maxcleanEntities())
                {
                    t_petty_cash Objpcash = new t_petty_cash();
                    Objpcash.Store_Id = ObjPCash.Store_Id;
                    Objpcash.Amount = ObjPCash.Amount;
                    Objpcash.Reason = ObjPCash.Reason;
                    Objpcash.Date = ObjPCash.Date;
                    Objpcash.Is_Active = true;
                    Objpcash.Created_Date = CommonClasses.StaticData.GetCurrentDataTime;
                    context.t_petty_cash.Add(Objpcash);
                    context.SaveChanges();
                    iRes = Objpcash.Petty_Cash_Id;

                    if (iRes > 0)
                    {
                        // save to t_petty_cash_utilization table
                        t_petty_cash_utilization objPCU = new t_petty_cash_utilization();
                        decimal? currentBalance = GetCurrentBalanceByStore(ObjPCash.Store_Id);

                        objPCU.Store_Id = ObjPCash.Store_Id;
                        objPCU.Credited_Amount = ObjPCash.Amount;
                        objPCU.Reason = ObjPCash.Reason;
                        objPCU.Balance_Amount = currentBalance + ObjPCash.Amount;
                        objPCU.Created_Date = DateTime.Now;
                        //objPCU.CreditDate = DateTime.Now;
                        objPCU.Is_Active = true;
                        //objPCU.Created_By = "admin";

                        context.t_petty_cash_utilization.Add(objPCU);
                        context.SaveChanges();
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PettyCash_Model> GetPettyCashList(int Store_Id)
        {
            List<PettyCash_Model> objpcash = new List<PettyCash_Model>();
            try
            {
                using (maxcleanEntities context = new maxcleanEntities())
                {

                    var res = (from a in context.t_petty_cash
                               where a.Store_Id == Store_Id
                               select new PettyCash_Model
                               {
                                   Store_Name = a.t_store.store_name,
                                   Amount = a.Amount,
                                   Reason = a.Reason,
                                   Date = a.Date,
                               }).OrderByDescending(o => o.Date).ToList();

                    return objpcash = res.ToList().Select(q => new PettyCash_Model
                    {

                        Store_Name = q.Store_Name,
                        Amount = q.Amount,
                        Reason = q.Reason,
                        Pdate = (q.Date == null) ? "" : q.Date.Value.ToString("dd-MM-yyyy"),

                    }).ToList();


                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private decimal? GetCurrentBalanceByStore(int? storeID)
        {

            decimal? balance = 0.0M;

            using (maxcleanEntities context = new maxcleanEntities())
            {
                var lastBalance = (from p in context.t_petty_cash_utilization
                                   where p.Store_Id == storeID
                                   select p).OrderByDescending(o => o.Petty_Cash_Utilization_id).Take(1).FirstOrDefault();
                if (lastBalance != null)
                {
                    balance = lastBalance.Balance_Amount;
                }
            }

            return balance;
        }



    }
}