using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Promocode_IRepository
    {
        int Add(Promocode_Model ObjPromocode);
        int Update(Promocode_Model ObjPromocode);
        int Delete(int PromocodeId);
        Promocode_Model GetPromocodeByID(int PromocodeId);
        List<Promocode_Model> GetActivePromocode();
        Base_Model GetPromocodeList(string length, string start, string sortColumn, string sortColumnDir, string draw, string search);

    }
}
