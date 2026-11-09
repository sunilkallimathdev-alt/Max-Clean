using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.IRepository
{
    interface CancelOrder_IRepositiry
    {
        int PUT([FromBody]PurchaseOrder_Model value);
        List<OrderCancel_Model> GetAllOrderCancelRequest(int id);
        Base_Model GetOrdersCancelList_BystoreID(string length, string start, string sortColumn, string sortColumnDir, string draw, string search, long id, string FromDate, string ToDate);
    }

}