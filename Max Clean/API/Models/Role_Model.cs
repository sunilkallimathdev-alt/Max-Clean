using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Role_Model:Base_Model
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}