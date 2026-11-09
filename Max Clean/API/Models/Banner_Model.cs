using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Banner_Model:Base_Model
    {
        
        public int BannerID { get; set; }
        public string Banner_Name { get; set; }
        public string Banner_Image { get; set; }

    }

    public class Marque_Model : Base_Model
    {

        public int MarqueID { get; set; }
        public string Marque_Name { get; set; }
        public string Marque_Image { get; set; }

    }

}