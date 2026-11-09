using API.Models;
using API.DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static MaxCleanWebV1.Models.StaticData;

namespace MaxCleanWebV1.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult orders()
        {

            return View();
        }

        public ActionResult Neworders()
        {

            return View();
        }
        public ActionResult Address()
        {

            return View();
        }

        public dynamic verify_mobile(string mobile)
        {
            try {

                WebClient client = new WebClient();
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Customer"));
                List<Customer_Model> ctm_list = res["ObjectResult"].ToObject<List<Customer_Model>>();
                var found = false;
                Customer_Model objcustomer = new Customer_Model();
                foreach(var cust in ctm_list)
                {
                    if(mobile == cust.MobileNumber)
                    {
                        objcustomer = cust;
                        break;
                    }
                }
                return JsonConvert.SerializeObject(objcustomer);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ActionResult ordernow()
        {

            ViewBag.Header = 2;
            WebClient client = new WebClient();
            var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Service/0/0"));
            List<Service_Model> serlist = res["ObjectResult"].ToObject<List<Service_Model>>();

            foreach (var sr in serlist)
            {
                var cat_res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Category/" + sr.ServiceID + "/0/0"));
                sr.Categorylist = cat_res["ObjectResult"].ToObject<List<Shop_Model>>();
            }
            ViewBag.services = serlist;
            return View();
        }

        public dynamic Getservices()
        {
            WebClient client = new WebClient();
            var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Service/0/0"));
            List<Service_Model> serlist = res["ObjectResult"].ToObject<List<Service_Model>>();

            foreach (var sr in serlist)
            {
                var cat_res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Category/" + sr.ServiceID + "/0/0"));
                sr.Categorylist = cat_res["ObjectResult"].ToObject<List<Shop_Model>>();
            }
            // ViewBag.services = serlist;
            return JsonConvert.SerializeObject(serlist);
        }



        public dynamic GetCategoryProductsByServiceID(int ServiceID)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "Category/" + ServiceID + "/0/0"));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public ActionResult getMyOrders()
        {
            try
            {
                if (Session["Users"] != null)
                {
                    Customer_Model user = (Customer_Model)Session["Users"];
                    WebClient client = new WebClient();
                    var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "GetOrderByCustomerID/" + user.CustomerID));
                    List<CustmoerOrders_Model> TempData = res["ObjectResult"].ToObject<List<CustmoerOrders_Model>>();
                    Session["UserOrders"] = TempData;
                    ViewBag.ErrorMessage = "";
                    ViewBag.Orders = TempData;
                    return View("MyOrders");
                }
                else
                {
                    return RedirectToAction("Home", "MaxClean");
                }

            }
            catch (Exception)
            {

                return RedirectToAction("Home", "MaxClean");
            }


        }

        public dynamic OrderDetails(int id)
        {
            if (Session["Users"] != null)
            {
                ViewBag.IsOtherResp = true; //Responsive Header change (Not Home Page)
                ViewBag.Header = "2"; //Activate second header. (There are two types of headers depending upon the page being visited.)
                if (id != 0 && Session["UserOrders"] is List<CustmoerOrders_Model>)
                {
                    CustmoerOrders_Model currentOrder = new CustmoerOrders_Model();
                    List<CustmoerOrders_Model> orderlist = new List<CustmoerOrders_Model>();
                    orderlist = (List<CustmoerOrders_Model>)Session["UserOrders"];
                    foreach (CustmoerOrders_Model order in orderlist)
                    {
                        if (order.OrderID == id)
                        {
                            currentOrder = order;
                            foreach (CustmoerOrderDetails_Model tempe in order.orderList)
                            {
                                if (tempe.extraPrice == null)
                                {
                                    tempe.extraPrice = 0;
                                }
                            }
                        }

                    }

                    ViewBag.OrderDetail = currentOrder;
                    return View();
                }
                else
                {
                    return "There was an error must be an Internal server error or your login session is expired. Please try logging in again and access this page.";
                }
            }
            else
            {
                return RedirectToAction("Home", "MaxClean");
            }

        }


        public ActionResult getMyRewardPoints()
        {
            if (Session["Users"] != null)
                return View("getMyRewardPoints");
            else
                return RedirectToAction("Home", "MaxClean");
        }

        public ActionResult OrderConfirmation(int id)
        {
           // if (Session["Users"] != null)
            //{
                ViewBag.InvoiceNumber = id;
                return View("OrderConfirmation");
           // }
           // else
           //     return RedirectToAction("Home", "MaxClean");
            
        }
        

        public ActionResult Cart()
        {
            double? oldbalance = 0;
            double? rewards = 0;
            if (Session["Users"] == null)
                ViewBag.login = false;
            else
            {
                Customer_Model cust = new Customer_Model();
                cust = (Customer_Model)Session["Users"];
                using (maxcleanEntities context = new maxcleanEntities())
                {
                    var custrwd_exists = context.t_reward.FirstOrDefault(c => c.customer_id == cust.CustomerID);
                    var cust_old_balance = context.t_customer.FirstOrDefault(c => c.customer_id == cust.CustomerID);

                    if (cust != null)
                    {
                        if (cust_old_balance.old_balance > 0)
                            oldbalance = cust_old_balance.old_balance;
                        if (custrwd_exists.customer_rewardpoints > 99)
                            rewards = custrwd_exists.customer_rewardpoints;
                    }
                }
            }
            ViewBag.Old_Balance = oldbalance;
            ViewBag.Cust_Rewards = rewards;
            return View();

        }

        public dynamic Place_order(string ObjOrder)
        {

            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Customer_PlaceOrder", "POST", ObjOrder));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public dynamic ValidatePromocode(string ObjPromocode)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "PromocodeValidate", "POST", ObjPromocode));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}