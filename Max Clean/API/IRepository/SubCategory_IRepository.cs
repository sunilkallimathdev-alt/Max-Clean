using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface SubCategory_IRepository
    {
        int Add(SubCategory_Model ObjSubCategory);
        int Update(SubCategory_Model ObjSubCategory);
        int Delete(int SubCategoryID);
        SubCategory_Model GetSubCategoryByID(int SubCategoryID);
        List<SubCategory_Model> GetSubCategoryByCategoryID(int CategoryID);
        List<SubCategory_Model> GetActiveSubCategory();
        List<SubCategory_Model> GetAllSubCategory();

    }
}
