using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models;

namespace API.IRepository
{
    interface  OrderCancelRequests_IRepository
    {
        List<OrderCancel_Model> GetOrderCancelList(int id);
    }
}