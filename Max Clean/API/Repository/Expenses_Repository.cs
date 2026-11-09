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
    public class Expenses_Repository : Expenses_IRepository
    {
        // GET: Expenses_Repository
        public int Add(Expenses_Model ObjExpen)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities context = new DataAccess.maxcleanEntities())
                {
                    t_expenses ObjExpencash = new t_expenses();
                    ObjExpencash.User_Id = ObjExpen.User_Id;
                    ObjExpencash.Store_Id = ObjExpen.Store_Id;
                    ObjExpencash.Amount = ObjExpen.Amount;
                    ObjExpencash.Reason = ObjExpen.Reason;
                    ObjExpencash.Date = ObjExpen.Date;
                    ObjExpencash.Is_Active = true;
                   
                    ObjExpencash.Created_Date = CommonClasses.StaticData.GetCurrentDataTime;
                    context.t_expenses.Add(ObjExpencash);
                    context.SaveChanges();
                    iRes = ObjExpencash.Expenses_Id;
                    if (iRes > 0)
                    {
                        // save to t_petty_cash_utilization table
                        t_petty_cash_utilization objPCU = new t_petty_cash_utilization();
                        decimal? currentBalance = GetCurrentBalanceByStore(ObjExpen.Store_Id);

                        objPCU.Store_Id = ObjExpencash.Store_Id;
                        objPCU.Debited_Amount = ObjExpencash.Amount;
                        objPCU.Reason = ObjExpencash.Reason;
                        objPCU.Balance_Amount = currentBalance - ObjExpencash.Amount;
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

        public List<Expenses_Model> GetExpensesList(int Store_Id)
        {
            List<Expenses_Model> ObjExp = new List<Expenses_Model>();
            try
            {
                using (maxcleanEntities context = new maxcleanEntities())
                {
                    var res = (from a in context.t_expenses
                               where a.Store_Id == Store_Id
                               select new Expenses_Model
                               {
                                   Store_Name=a.t_store.store_name,
                                   User_Name=a.t_user.user_name,
                                   Amount = a.Amount,
                                   Reason = a.Reason,
                                   Date = a.Date,
                               }).OrderByDescending(o => o.Date).ToList();
                    return ObjExp = res.ToList().Select(q => new Expenses_Model
                    {
                        Store_Name = q.Store_Name,
                        User_Name = q.User_Name,
                        Amount = q.Amount,
                        Reason = q.Reason,
                        date = (q.Date == null) ? "" : q.Date.Value.ToString("dd-MM-yyyy"),

                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public List<Expenses_Model> GetExpensesList()
        //{
        //    List<Expenses_Model> ObjExp = new List<Expenses_Model>();
        //    try
        //    {
        //        using (maxcleanEntities context = new maxcleanEntities())
        //        {
        //            var res = (from a in context.t_expenses
        //                           //  where a.Store_Id == Store_Id
        //                       select new Expenses_Model
        //                       {

        //                           Amount = a.Amount,
        //                           Reason = a.Reason,
        //                           Date = a.Date,
        //                       }).OrderByDescending(o => o.Date).ToList();
        //            return ObjExp = res.ToList().Select(q => new Expenses_Model
        //            {

        //                Amount = q.Amount,
        //                Reason = q.Reason,
        //                date = (q.Date == null) ? "" : q.Date.Value.ToString("dd-MM-yyyy"),

        //            }).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
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