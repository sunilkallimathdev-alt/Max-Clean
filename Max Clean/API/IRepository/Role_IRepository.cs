using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Role_IRepository
    {

        int Add(Role_Model ObjRole);
        int Update(Role_Model ObjRole);
        int Delete(int RoleID);
        Role_Model GetRoleByID(int RoleID);
        List<Role_Model> GetActiveRole();
        List<Role_Model> GetAllRole();
    }
}
