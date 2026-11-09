using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Category_IRepository
    {
        int Add(Category_Model ObjCategory);
        int Update(Category_Model ObjCategory);
        int Delete(int CategoryId);
        Category_Model GetCategoryByID(int CategoryId);
        List<Category_Model> GetActiveCategory();
        List<Category_Model> GetAllCategory();
        List<Shop_Model> GetCategoryByServiceID(int ServiceID);
        List<mCategory_Model> GetCategoriesByServiceID(int ServiceID);
        Base_Model GetCategoryList(string length, string start, string sortColumn, string sortColumnDir, string draw,string search);
    }
}
