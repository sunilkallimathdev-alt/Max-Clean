using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class RolesAndPrivileges_Model
    {
        public class Category : Base_Model
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
            public string FontAwsome { get; set; }
            public int? DisplayOrder { get; set; }
            public List<Page> PageList { get; set; }
        }
        public class Page
        {
            public int? PageID { get; set; }
            public int? PageCategoryID { get; set; }
            public string PageCategoryName { get; set; }
            public string PageName { get; set; }
            public string Description { get; set; }
            public string PageURL { get; set; }
            public int? DisplayOrder { get; set; }
            public bool? Is_Active { get; set; }

            public int RolePageID { get; set; }
            public int? RoleID { get; set; }
            public bool? Is_view { get; set; }
            public bool? Is_Add_Edit { get; set; }
            public bool? Is_Delete { get; set; }
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

        }
    }
}