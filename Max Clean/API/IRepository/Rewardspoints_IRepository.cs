using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Rewardspoints_IRepository
    {
        Reward_Model GetCustomerRewardsByID(int CustomeID);
    }
}
