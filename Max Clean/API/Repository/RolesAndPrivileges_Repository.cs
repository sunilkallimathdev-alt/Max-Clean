using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class RolesAndPrivileges_Repository : RolesAndPrivileges_IRepository
    {
        public List<RolesAndPrivileges_Model.Category> GetPages()
        {
            List<RolesAndPrivileges_Model.Category> _objcat = new List<RolesAndPrivileges_Model.Category>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Res = (from a in Context.tbl_admin_pages
                               where a.is_active == true
                               select new
                               {
                                   CategoryID = a.tbl_admin_category.id,
                                   CategoryName = a.tbl_admin_category.category,
                                   FontAwsome = a.tbl_admin_category.font_awsome,
                                   CatDisplayOrder = a.tbl_admin_category.display_order,

                                   PageID = a.id,
                                   PageCategoryID = a.category_id,
                                   PageName = a.page_name,
                                  // Description = a.description,
                                   PageURL = a.url,
                                   DisplayOrder = a.display_order,
                                   Is_Active = a.is_active,
                               }).ToList();
                    var catlist = Res.GroupBy(x => x.CategoryID).Select(x => x.FirstOrDefault()).ToList();
                    _objcat = catlist.Select(x => new RolesAndPrivileges_Model.Category { CategoryID = x.CategoryID, CategoryName = x.CategoryName, FontAwsome = x.FontAwsome, DisplayOrder = x.CatDisplayOrder }).ToList();
                    foreach (var cat in _objcat)
                    {
                        cat.PageList = Res.Where(x => x.CategoryID == cat.CategoryID).Select(x => new RolesAndPrivileges_Model.Page { PageID = x.PageID, PageCategoryID = x.PageCategoryID, PageName = x.PageName, PageURL = x.PageURL, DisplayOrder = x.DisplayOrder, Is_Active = x.Is_Active }).ToList();
                    }
                }
                return _objcat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RolesAndPrivileges_Model.Category> GetPrivilegesByRoleID(int RoleID)
        {
            List<RolesAndPrivileges_Model.Category> _objpages = new List<RolesAndPrivileges_Model.Category>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Res = (from a in Context.tbl_roles_privileges
                               where a.role_id == RoleID
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
                    var catlist = Res.GroupBy(x => x.CategoryID).Select(x => x.FirstOrDefault()).OrderBy(c=>c.CatDisplayOrder).ToList();
                    _objpages = catlist.Select(x => new RolesAndPrivileges_Model.Category { CategoryID = x.CategoryID, CategoryName = x.CategoryName, FontAwsome = x.FontAwsome, DisplayOrder = x.CatDisplayOrder }).ToList();
                    foreach (var cat in _objpages)
                    {
                        cat.PageList = Res.Where(x => x.CategoryID == cat.CategoryID).Select(x => new RolesAndPrivileges_Model.Page { PageID = x.PageID, PageCategoryID = x.PageCategoryID, PageName = x.PageName, PageURL = x.PageURL, DisplayOrder = x.DisplayOrder, Is_Active = x.Is_Active, RolePageID = x.RolePageID, RoleID = x.RoleID,Is_Delete=x.Is_Delete,Is_Add_Edit=x.Is_Add_Edit,Is_view=x.Is_view }).ToList();
                    }

                    return _objpages;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}