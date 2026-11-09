using API.CommonClasses;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace API.Repository
{
    public class AdminLogin_Repository : AdminLogin_IRepository
    {
        public class Login_Model
        {
            public AdminUser_Model objadmin { get; set; }
            public List<RolesAndPrivileges_Model.Category> objroles { get; set; }
        }

        public Login_Model login(AdminUser_Model ObjAdmin)
        {
            Login_Model objmast = new Login_Model();
            AdminUser_Model objadmin = new AdminUser_Model();
           List<RolesAndPrivileges_Model.Category> objroles = new List<RolesAndPrivileges_Model.Category>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                     objadmin = (from a in Context.t_user
                               where ( a.user_name == ObjAdmin.UserName && a.password == ObjAdmin.Password) && a.is_active == true
                               select new AdminUser_Model
                               {
                                   UserID = a.user_id,
                                   RoleID = a.role_id,
                                   RoleName = a.t_role.role_name,
                                   StoreID = a.store_id,
                                   StoreName = a.t_store.store_name,
                                   UserName = a.user_name,
                                   MobileNumber = a.mobile_no,
                                   EmailID = a.email_id,
                                   Password = a.password,
                                   Address=a.address
                                  
                               }).SingleOrDefault();


                    if (objadmin.RoleID != null)
                    {
                        var pagelist = (from a in Context.tbl_roles_privileges
                                        where a.role_id == objadmin.RoleID
                                        select new
                                        {
                                            CategoryID = a.tbl_admin_pages.tbl_admin_category.id,
                                            CategoryName = a.tbl_admin_pages.tbl_admin_category.category,
                                            FontAwsome = a.tbl_admin_pages.tbl_admin_category.font_awsome,
                                            CatDisplayOrder = a.tbl_admin_pages.tbl_admin_category.display_order,

                                            PageID = a.id,
                                            PageCategoryID = a.tbl_admin_pages.category_id,
                                            PageName = a.tbl_admin_pages.page_name,
                                            //Description = a.tbl_admin_pages.description,
                                            PageURL = a.tbl_admin_pages.url,
                                            DisplayOrder = a.tbl_admin_pages.display_order,
                                            Is_Active = a.tbl_admin_pages.is_active,

                                            RolePageID = a.id,
                                            RoleID = a.role_id,
                                            Is_view = a.is_view_enabled,
                                            Is_Add_Edit = a.is_add_edit_enabled,
                                            Is_Delete = a.is_delete_enabled,
                                        }).ToList();
                        var catlist = pagelist.GroupBy(x => x.CategoryID).Select(x => x.FirstOrDefault()).ToList();
                        var _objpages = catlist.Select(x => new RolesAndPrivileges_Model.Category { CategoryID = x.CategoryID, CategoryName = x.CategoryName, FontAwsome = x.FontAwsome, DisplayOrder = x.CatDisplayOrder }).ToList();
                        foreach (var cat in _objpages)
                        {
                            cat.PageList = pagelist.Where(x => x.CategoryID == cat.CategoryID).Select(x => new RolesAndPrivileges_Model.Page { PageID = x.PageID, PageCategoryID = x.PageCategoryID, PageName = x.PageName, PageURL = x.PageURL, DisplayOrder = x.DisplayOrder, Is_Active = x.Is_Active, RolePageID = x.RolePageID, RoleID = x.RoleID, Is_Delete = x.Is_Delete, Is_Add_Edit = x.Is_Add_Edit, Is_view = x.Is_view }).ToList();
                        }

                        objroles = _objpages;
                        objmast.objroles = objroles;
                    }
                    objmast.objadmin = objadmin;
                  


                    return objmast;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ForgotPassword(AdminUser_Model obj)
        {
            bool passwordrequest = false;
            AdminUser_Model ObjUser = new AdminUser_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Res = Context.t_user.FirstOrDefault(c => c.user_name == obj.UserName);
                    if (Res != null)
                    {
                        if (Res.email_id != null)
                        {

                            ObjUser.UserName = Res.user_name;
                            ObjUser.EmailID = Res.email_id;
                            ObjUser.Password = Res.password;
                            var emailFrom = API.CommonClasses.EmailSMSCredentials.FromEmail;
                            var password = API.CommonClasses.EmailSMSCredentials.Password;
                            AdminLogin_Repository fr = new AdminLogin_Repository();
                            if (ObjUser.EmailID != null)
                            {
                                fr.SendMail(
                                    ObjUser.EmailID,
                                    "Company Admin Password Recovery",
                                    emailFrom,
                                    password,
                                    "Dear " + ObjUser.UserName + ","
                                    + Environment.NewLine + ""
                                    + Environment.NewLine + "Welcome to Company Admin."
                                    + Environment.NewLine + ""
                                    + Environment.NewLine + "Use the following values when prompted to log in:"
                                    + Environment.NewLine + "UserName  : " + ObjUser.UserName
                                    + Environment.NewLine + "Password : " + ObjUser.Password
                                    + Environment.NewLine + "Click here to login: http://maxcleanadmin.myprelaunch.in"
                                    + Environment.NewLine + ""
                                    + Environment.NewLine + ""
                                    + Environment.NewLine + "Thanks and Regards"
                                    + Environment.NewLine + "Team testcompanys Technologies.");
                            }
                            passwordrequest = true;
                        }
                    }
                    else
                    {
                        passwordrequest = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return passwordrequest;
        }
        public bool SendMail(string sEmailTo, string sEmailSubject, string sEmailFrom, string sPassword, string sEmailContent)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(EmailSMSCredentials.server);
                mail.From = new MailAddress(EmailSMSCredentials.FromEmail);
                mail.To.Add(sEmailTo);
                mail.IsBodyHtml = false;
                mail.Subject = sEmailSubject;
                mail.Body = sEmailContent;
                SmtpServer.Port = EmailSMSCredentials.Port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(EmailSMSCredentials.FromEmail, EmailSMSCredentials.Password);
                SmtpServer.EnableSsl = EmailSMSCredentials.EnableSsl;
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}