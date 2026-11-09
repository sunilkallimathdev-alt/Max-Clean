using ADMIN.API_Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class UnitController : Controller
    {
        // GET: Unit
        public ActionResult Index()
        {
            return View();
        }
        public dynamic GetUnitList()
        {
            try
            {
                API_Unit Objunit = new API_Unit();
                return JsonConvert.SerializeObject(Objunit.GetUnitList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddUnit(string ObjUnit)
        {
            try
            {
                API_Unit Objunit = new API_Unit();
                return JsonConvert.SerializeObject(Objunit.AddUnit(ObjUnit));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic UpdateUnit(string ObjUnit)
        {
            try
            {
                API_Unit Objunit = new API_Unit();
                return JsonConvert.SerializeObject(Objunit.UpdateUnit(ObjUnit));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetUnit(int UnitID)
        {
            try
            {
                API_Unit Objunit = new API_Unit();
                return JsonConvert.SerializeObject(Objunit.GetUnit(UnitID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeleteUnit(int UnitID)
        {
            try
            {
                API_Unit Objunit = new API_Unit();
                return JsonConvert.SerializeObject(Objunit.DeleteUnit(UnitID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}