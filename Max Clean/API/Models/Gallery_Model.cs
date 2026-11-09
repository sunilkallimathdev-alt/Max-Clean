using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Gallery_Model : Base_Model
    {
        public int GalleryID { get; set; }
        public string Title { get; set; }
        public bool? Is_Image { get; set; }
        public string Url { get; set; }

        public bool? IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}