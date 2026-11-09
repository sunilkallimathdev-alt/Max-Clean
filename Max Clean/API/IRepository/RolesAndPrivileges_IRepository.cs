using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface RolesAndPrivileges_IRepository
    {
        List<RolesAndPrivileges_Model.Category> GetPages();
        List<RolesAndPrivileges_Model.Category> GetPrivilegesByRoleID(int RoleID);
    }
}
