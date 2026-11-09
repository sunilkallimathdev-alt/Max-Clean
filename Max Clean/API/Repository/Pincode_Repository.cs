using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Repository
{
    public class Pincode_Repository : Pincode_IRepository
    {
        public int Add(Pincode_Model ObjPincode)
        {
            throw new NotImplementedException();
        }

        public int Delete(int PincodeID)
        {
            throw new NotImplementedException();
        }

        public int GetStoreByPincode(int Pincode)
        {
            try
            {
                int res = 0;
                using(maxcleanEntities context = new maxcleanEntities())
                {
                    var objpin = context.t_pincodes.FirstOrDefault(x => x.pincode == Pincode);
                    res = (objpin != null) ? Convert.ToInt32(objpin.store_id) : 0;
                }
                return res;
            }catch(Exception e)
            {
                throw e;
            }
        }
        public List<Pincode_Model> GetActivePincode()
        {
            List<Pincode_Model> ObjPincode = new List<Pincode_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjPincode = (from a in Context.t_pincodes
                                        where a.is_active == true
                                        select new Pincode_Model
                                        {
                                            PincodeID = a.pincode_id,
                                            Pincode = a.pincode,
                                            IsActive = a.is_active,
                                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Pincode_Model> GetAllPincode()
        {
            throw new NotImplementedException();
        }

        public Pincode_Model GetPincodeByID(int PincodeID)
        {
            throw new NotImplementedException();
        }

        public int Update(Pincode_Model ObjPincode)
        {
            throw new NotImplementedException();
        }
    }
}