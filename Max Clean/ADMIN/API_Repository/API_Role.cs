using API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using API.DataAccess;

namespace ADMIN.API_Repository
{
    public class API_Role
    {


        public dynamic GetRoleList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Role"));
                return res["ObjectResult"].ToObject<List<Role_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetActiveRoleList()
        {
            try
            {
                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Role/0/0"));
                return res["ObjectResult"].ToObject<List<Role_Model>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddRole(string Objrole)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Role", "POST", Objrole));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic UpdateRole(string Objrole)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Role", "PUT", Objrole));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteRole(int RoleID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Role/" + RoleID, "DELETE", ""));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetTicketSubject(int RoleID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Role/" + RoleID));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public class PagesList
        {
            public int? CategoryID { get; set; }
            public string CategoryName { get; set; }
            public int? PageID { get; set; }
            public string PageName { get; set; }
            public string Page_URL { get; set; }
            public int? RoleID { get; set; }
            public bool isChecked { get; set; }
            public bool? Is_view { get; set; }
            public bool? Is_Add_Edit { get; set; }
            public bool? Is_Delete { get; set; }
        }

        public dynamic Getpages()
        {

            List<PagesList> _PagesList = new List<PagesList>();
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    var PagesList = (from p in Context.tbl_admin_pages
                                     join c in Context.tbl_admin_category on p.category_id equals c.id
                                     select new PagesList
                                     {
                                         CategoryID = c.id,
                                         CategoryName = c.category,
                                         PageID = p.id,
                                         PageName = p.page_name,
                                         Page_URL = p.url
                                     }).ToList();

                    _PagesList = PagesList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _PagesList;
        }

        public dynamic GetRolePages(int RoleID)
        {
            List<PagesList> _PagesList = new List<PagesList>();
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    var PagesList = (from rp in Context.tbl_roles_privileges
                                     join p in Context.tbl_admin_pages on rp.admin_page_id equals p.id
                                     join c in Context.tbl_admin_category on p.category_id equals c.id
                                     where rp.role_id == RoleID
                                     select new PagesList
                                     {

                                         CategoryID = c.id,
                                         CategoryName = c.category,
                                         PageID = p.id,
                                         PageName = p.page_name,
                                         Page_URL = p.url,
                                         Is_Add_Edit = rp.is_add_edit_enabled,
                                         Is_Delete = rp.is_delete_enabled,
                                         Is_view = rp.is_view_enabled,

                                     }).ToList();

                    _PagesList = PagesList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _PagesList;
        }

        public void SaveRoleList(List<API_Role.PagesList> PagesList)
        {

            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {
                    foreach (var Priv in PagesList)
                    {
                        var Found = Context.tbl_roles_privileges.FirstOrDefault(t => t.role_id == Priv.RoleID && t.admin_page_id == Priv.PageID);
                        if (Found != null)
                        {
                            if (Priv.isChecked == false)
                            {
                                Context.tbl_roles_privileges.Remove(Found);
                                Context.SaveChanges();
                            }
                           else
                            {
                               
                                Found.is_view_enabled = Priv.Is_view;
                                Found.is_add_edit_enabled = Priv.Is_Add_Edit;
                                Found.is_delete_enabled = Priv.Is_Delete;
                                Context.SaveChanges();
                            }
                        }
                        else
                        {
                            if (Priv.isChecked == true)
                            {
                                tbl_roles_privileges ObjRole = new tbl_roles_privileges();
                                ObjRole.admin_page_id = Priv.PageID;
                                ObjRole.role_id = Priv.RoleID;
                                ObjRole.is_view_enabled = Priv.Is_view;
                                ObjRole.is_add_edit_enabled = Priv.Is_Add_Edit;
                                ObjRole.is_delete_enabled = Priv.Is_Delete;
                                Context.tbl_roles_privileges.Add(ObjRole);
                                Context.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public class Admin
        //{
        //    public int? UserID { get; set; }
        //    public int? RoleID { get; set; }
        //    public string RoleName { get; set; }
        //    public string Name { get; set; }
        //    public string UserName { get; set; }
        //    public string MobileNo { get; set; }
        //    public string Password { get; set; }
        //    public string EmailId { get; set; }
        //}

        //public dynamic login(Admin ObjAdmin)
        //{
        //    try
        //    {
        //        using (maxcleanEntities Context = new maxcleanEntities())
        //        {
        //            var Res = (from a in Context.t_user
        //                       join r in Context.t_role on a.role_id equals r.role_id
        //                       where (a.user_name == ObjAdmin.UserName && a.password == ObjAdmin.Password)
        //                       select new Admin
        //                       {
        //                           UserID = a.user_id,
        //                           RoleID = r.role_id,
        //                           RoleName = r.role_name,
        //                           Name = a.user_name,
        //                           Password = a.password,
        //                           UserName = a.user_name,
        //                           MobileNo = a.mobile_no,
        //                           EmailId = a.email_id
        //                       }).SingleOrDefault();
        //            return Res;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public class CategoryList
        //{
        //    public int? CategoryID { get; set; }
        //    public string CategoryName { get; set; }
        //    public string CatLiID { get; set; }
        //    public int? PageID { get; set; }
        //    public string PageName { get; set; }
        //    public string Page_URL { get; set; }
        //    public string LiID { get; set; }
        //}

        //public List<CategoryList> GetRolesPagesList(int RoleID)
        //{
        //    List<CategoryList> _CategoryList = new List<CategoryList>();
        //    try
        //    {
        //        using (maxcleanEntities Context = new maxcleanEntities())
        //        {
        //            var CategoryList = (from rp in Context.tbl_roles_privileges

        //                                where rp.role_id == RoleID
        //                                orderby rp.tbl_admin_pages.display_order
        //                                select new CategoryList
        //                                {
        //                                    CategoryID = rp.tbl_admin_pages.tbl_admin_category.id,
        //                                    CategoryName = rp.tbl_admin_pages.tbl_admin_category.category,
        //                                    CatLiID = rp.tbl_admin_pages.tbl_admin_category.font_awsome,
        //                                    PageID = rp.tbl_admin_pages.id,
        //                                    PageName = rp.tbl_admin_pages.page_name,
        //                                    Page_URL = rp.tbl_admin_pages.url,
        //                                    LiID = rp.tbl_admin_pages.li_id

        //                                }).ToList();
        //            _CategoryList = CategoryList;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return _CategoryList;
        //}

        //public dynamic Getmenu()
        //{
        //    try
        //    {
        //        List<CategoryList> CategoryList = new List<CategoryList>();
        //        List<int> pageid = new List<int>();
        //        int RoleID = Convert.ToInt16(Request.Cookies["RoleID"].Value);
        //        CategoryList = GetRolesPagesList(RoleID);
        //        StringBuilder LeftMenu = new StringBuilder();
        //        int? CatID = 0;
        //        foreach (CategoryList ObjCat in CategoryList)
        //        {
        //            if (CatID != ObjCat.CategoryID)
        //            {
        //                if (CatID > 0)
        //                {
        //                    LeftMenu.Append("</ul></li>");
        //                }
        //                LeftMenu.Append("<li class='submenu' id='" + ObjCat.CatLiID + "'><a href='#'><i class='icon icon-th-list'></i><span>" + ObjCat.CategoryName + "</span><span class='label label-important'>" + CategoryList.Where(x => x.CategoryID == ObjCat.CategoryID).Count() + "</span></a><ul>");
        //                LeftMenu.Append("<li><a href='" + ObjCat.Page_URL + "' id='" + ObjCat.LiID + "'>" + ObjCat.PageName + "</a></li>");
        //                CatID = ObjCat.CategoryID;
        //            }
        //            else
        //            {
        //                LeftMenu.Append("<li><a href='" + ObjCat.Page_URL + "' id='" + ObjCat.LiID + "'>" + ObjCat.PageName + "</a></li>");
        //            }
        //        }
        //        LeftMenu.Append("</ul></li>");
        //        leftmenu.Text = LeftMenu.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Redirect("AdminLogin.aspx");
        //    }
        //}
    }
}