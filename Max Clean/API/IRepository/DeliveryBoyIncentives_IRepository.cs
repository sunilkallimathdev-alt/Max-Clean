using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface DeliveryBoyIncentives_IRepository
    {
        long AddDeliveryBoyIncentives(DeliveryBoyInsentives_Model Obj);
        //bool SendInvoice(long DeliveryBoyID);
       DeliveryBoyInsentives_Model GetAllTransactionByDeliveryBoyID(long DBID);




        //DeliveryBoyInsentives_Model GetMakePayment(DeliveryBoyInsentives_Model Obj);

        //long AddPaymentToVendorProductOrder(PurchaseOrderPayment_Model Objvpot);
        //bool SendPaymentMail(long RequestedOrderID);
        //VendorProductOrderPayment_ModelResult GetAllTransactionByVendorProductOrderID(long VPOID);
        //List<VendorProductOrderPayment_ModelResult> GetAllTransactionByVendorID(long VendorID);

        //List<DeliveryBoyInsentives_Model> GetActiveCategory();
        //List<DeliveryBoyInsentives_Model> GetAllCategory();
    }
}
