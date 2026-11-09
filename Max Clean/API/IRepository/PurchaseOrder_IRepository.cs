using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface PurchaseOrder_IRepository
    {
        long Add(PurchaseOrder_Model ObjPO);
        long Update(PurchaseOrder_Model ObjPO);
        //long Delete(PurchaseOrder_Model ObjPO);
        //List<PurchaseOrder_Model> GetInActiveRequest();
        long UpdateDetails(PurchaseOrder_Model ObjPO);
         List<PurchaseOrder_Model> GetAllOutletOrders();
        List<PurchaseOrder_Model> GetAllOrdersList();
        PurchaseOrder_Model GetOrderByID(long OrderID);
        List<PurchaseOrder_Model> GetAllOrders(PurchaseOrder_Model dates);

        List<PurchaseOrder_Model> GetOrdersByStoreId(int StoreID);

        List<PurchaseOrder_Model> GetAllOnlineOutletOrders(bool? IsOutlet);
        List<PurchaseOrder_Model> GetAllPickupReadyOrders();
        long AcceptCancelOrder(PurchaseOrder_Model Objpo);
        List<PurchaseOrder_Model> GetPickupAcceptedOrders(int DeliveryBoyID);
        List<PurchaseOrder_Model> GetAllDeliveryReadyOrders();
        List<PurchaseOrder_Model> GetDeliveryAcceptedOrders(int DeliveryBoyID);

        Base_Model GetOrdersList_BystoreID(string length, string start, string sortColumn, string sortColumnDir, string draw, string search, long id, string FromDate, string ToDate);

        long UpdateStatus(PurchaseOrder_Model obj);

        long payBalance(PurchaseOrder_Model ObjPO);
        List<PurchaseOrder_Model> GetAllPickedUpOrders(int DeliveryBoyID);

        long SubmitPickedOrders(OrderList_Model obj);
        long SubmitAtdelivery(PurchaseOrder_Model obj);
        long SubmitDeliveryAccepted(OrderList_Model obj);
        List<PurchaseOrder_Model> GetDeliveryOrdersRecived(int DeliveryBoyID);
        dynamic GetAvailableTimeSlots(DateTime givendate);
    }
}
