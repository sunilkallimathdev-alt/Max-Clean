using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.IRepository
{
    interface Expenses_IRepository
    {
        int Add(Expenses_Model ObjExpen);

        List<Expenses_Model> GetExpensesList(int Store_Id);
        //List<Expenses_Model> GetExpensesList();

    }
}