using API.DataAccess;
using API.IRepository;
using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class PCashUtilization_Repository: PCashUtilization_IRepository
    {
        //petty cash Utilization List
        public List<PCashUtilzation_Model> GetPCashUtlizationListBySID(PCashUtilzation_Model Store_Id)
        {
            List<PCashUtilzation_Model> PCUtl = new List<PCashUtilzation_Model>();
            try
            {
                using (maxcleanEntities context = new maxcleanEntities())
                {
                    PettyCash_Model Pcash = new PettyCash_Model();
                    //var storid=

                    if (Store_Id.Store_Id != 0)
                    {
                        var res = (from a in context.t_petty_cash_utilization
                                   where a.Store_Id == Store_Id.Store_Id
                                   select new PCashUtilzation_Model
                                   {  
                                       PCashUtil_id=a.Petty_Cash_Utilization_id,
                                       Reason = a.Reason,
                                       CreditedAmount = a.Credited_Amount, 
                                       DebitedAmount = a.Debited_Amount ,
                                       BalanceAmount = a.Balance_Amount,
                                       Date = a.Date,
                                       //Date=a.Date.Value.Date,
                                   }).OrderByDescending(o => o.Date).ToList();

                        return PCUtl = res.ToList().Select(q => new PCashUtilzation_Model
                        {
                            PCashUtil_id = q.PCashUtil_id,
                            Reason = q.Reason,
                            CreditedAmount = q.CreditedAmount,
                            DebitedAmount = q.DebitedAmount,
                            BalanceAmount = q.BalanceAmount,
                            Pdate = (q.Date == null) ? "" : q.Date.Value.ToString("dd-MM-yyyy"),


                        }).ToList();
                    }
                    else
                    {

                        var res = (from a in context.t_petty_cash_utilization
                                   where a.Store_Id==Store_Id.Store_Id
                                   select new PCashUtilzation_Model
                                   {
                                       Reason = a.Reason,
                                       CreditedAmount = a.Credited_Amount,
                                       DebitedAmount = a.Debited_Amount,
                                       BalanceAmount = a.Balance_Amount,
                                       Date = a.Date,
                                       //Date=a.Date.Value.Date,
                                   }).ToList();
                        return PCUtl = res.ToList().Select(q => new PCashUtilzation_Model
                        {
                            Reason = q.Reason,
                            CreditedAmount = q.CreditedAmount,
                            DebitedAmount = q.DebitedAmount,
                            BalanceAmount = q.BalanceAmount,
                            Pdate = (q.Date == null) ? "" : q.Date.Value.ToString("dd-MM-yyyy"),


                        }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}