using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.IRepository
{
    
    interface PCashUtilization_IRepository
    {
      //  dynamic GetPCashUtilizationList(int CustomerID);
       // object add(PCashUtilzation_Model value);
        List<PCashUtilzation_Model> GetPCashUtlizationListBySID(PCashUtilzation_Model Store_Id);
    }
}