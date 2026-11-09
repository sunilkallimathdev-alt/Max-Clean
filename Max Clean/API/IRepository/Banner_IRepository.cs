using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Banner_IRepository
    {

        int Add(Banner_Model ObjBanner);
        int Update(Banner_Model ObjBanner);
        int Delete(int BannerID);
        Banner_Model GetBannerByID(int BannerID);
        List<Banner_Model> GetActiveBanner();
        List<Banner_Model> GetAllBanner();

    }
}
