using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Gallery_IRepository
    {
        int Add(Gallery_Model ObjGallery);
        int Update(Gallery_Model ObjGallery);
        int Delete(int GalleryID);
        Gallery_Model GetGalleryByID(int GalleryID);
        List<Gallery_Model> GetActiveGallery();
        List<Gallery_Model> GetAllGallery();
    }
}
