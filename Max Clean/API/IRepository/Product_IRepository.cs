using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Product_IRepository
    {
        int Add(Product_Model ObjProduct);
        int Update(Product_Model ObjProduct);
        int Delete(int ProductID);
        Product_Model GetProductByID(int ProductID);
        List<Product_Model> GetProductByCategoryID(int CategoryID);
        List<Product_Model> GetActiveProduct();
        List<Product_Model> GetAllProduct();
        List<Product_Model> GetServicePrice();
        List<Service_Model> GetProductPrices();
        List<mProduct_Model> GetProductByCategoryServiceID(int ServiceID, int CategoryID);
    }
}
