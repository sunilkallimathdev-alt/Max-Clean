using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Customer_IRepository
    {
        int Add(Customer_Model ObjCustomer);
        int AddCustomerRating(CustomerRating_Model ObjCustomer);
        int Update(Customer_Model ObjCustomer);
        int Delete(int CustomeID);
        Customer_Model GetCustomerByID(int CustomeID);
        Customer_Model GetCustomerID(Customer_Model CustomeID);
        List<Customer_Model> GetActiveCustomers();
        List<Customer_Model> GetCustomers_ByDates(Customer_Model dates);
        List<Customer_Model> GetAllCustomers();
        dynamic GetNonPerformingCustomer();
        dynamic GetOrders_of_Customer(int CustomerID);
        dynamic login(Customer_Model ObjAdmin);
        int SendOTP(string mobile);
        bool ForgotPassword(Customer_Model obj);
        dynamic OTPVerify(int Cus_ID);
        dynamic Promocode(Promocode_Model ObjAdmin);
        dynamic GetActiveOrders_of_Customer(int Customer_ID);
        List<ActiveOrders_Model> GetOrders_of_Customer_mobile(int CustomerID);
    }
}
