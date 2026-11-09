using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface AdminUser_IRepository
    {
        int Add(AdminUser_Model ObjAdminUsers);
        int Update(AdminUser_Model ObjAdminUsers);
        int Delete(int UserID);
        AdminUser_Model GetAdminUsersByID(int UserID);
        List<AdminUser_Model> GetActiveAdminUsers();
        List<AdminUser_Model> GetAllAdminUsers();
        
    }
}
