using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Unit_IRepository
    {
        int Add(Unit_Model ObjUnit);
        int Update(Unit_Model ObjUnit);
        int Delete(int UnitID);
        Unit_Model GetUnitByID(int UnitID);
        List<Unit_Model> GetActiveUnit();
        List<Unit_Model> GetAllUnit();
    }
}
