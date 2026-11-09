using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface HomeBanner_IRepository
    {
           
        int Add(HomeBanner_Model ObjHome);
        int Update(HomeBanner_Model ObjHome);
        int Delete(int BannerID);
        HomeBanner_Model GetHomeBannerByID(int BannerID);
        List<HomeBanner_Model> GetActiveHomeBanner();
        List<HomeBanner_Model> GetAllHomeBanner();
    }
}
