using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class HomeBanner_Model:Base_Model
    {
        public int BannerID { get; set; }
        public string BannerTitle { get; set; }
        public string BannerUrl { get; set; }
        public int? DisplayOrder { get; set; }
        public string BannerText1 { get; set; }
        public string BannerText2 { get; set; }
        public string BannnerText3 { get; set; }
    }
}