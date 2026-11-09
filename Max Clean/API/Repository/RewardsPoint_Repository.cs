using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class RewardsPoint_Repository: Rewardspoints_IRepository
    {
        public Reward_Model GetCustomerRewardsByID(int CustomeID)
        {
            Reward_Model ObjCustomers = new Reward_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var ObjCustomer = (from a in Context.t_reward
                                       where a.customer_id == CustomeID
                                       select new Reward_Model
                                       {
                                           CustomerID = a.customer_id,
                                          CustomerRewardPoints = a.customer_rewardpoints,
                                          CustomerUsedRewardPoints= a.customer_usedrewards,
                                           CreatedBy = a.created_by,
                                           CreatedDate = a.created_date,
                                           UpdateBy = a.update_by,
                                           UpdateDate = a.update_date
                                       }).SingleOrDefault();
                 
                    return ObjCustomers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}