using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.IRepository
{
    interface PettyCash_IRepository
    {
        int Add(PettyCash_Model ObjPCash);
        
        List<PettyCash_Model> GetPettyCashList(int Store_Id);
        //Base_Model GetPettyCashList(string length, string start, string sortColumn, string sortColumnDir, string draw, string search);
    }
}