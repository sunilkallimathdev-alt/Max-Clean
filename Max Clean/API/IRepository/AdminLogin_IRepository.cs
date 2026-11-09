using System;
using API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface AdminLogin_IRepository
    {
       API.Repository.AdminLogin_Repository.Login_Model  login(AdminUser_Model ObjAdmin);
        bool ForgotPassword(AdminUser_Model obj);
        bool SendMail(string sEmailTo, string sEmailSubject, string sEmailFrom, string sPassword, string sEmailContent);

    }
}
