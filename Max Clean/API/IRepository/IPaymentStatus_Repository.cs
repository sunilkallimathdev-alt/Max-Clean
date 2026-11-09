using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface IPaymentStatus_Repository
    {
        long Add(PaymentStatus_Model ObjPS);
       
    }
}
