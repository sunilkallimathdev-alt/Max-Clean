using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface PurchaseOrderPayment_IRepository
    {
        long AddPaymentToVendorProductOrder(PurchaseOrderPayment_Model Objvpot);
        bool SendPaymentMail(long RequestedOrderID);
        VendorProductOrderPayment_ModelResult GetAllTransactionByVendorProductOrderID(long VPOID);
        List<VendorProductOrderPayment_ModelResult> GetAllTransactionByVendorID(long VendorID);
    }
}
