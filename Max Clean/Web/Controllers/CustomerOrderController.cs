using API.CommonClasses;
using API.Models;
using MaxCleanWeb.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MaxCleanWeb.Controllers
{
    public class CustomerOrderController : Controller
    {
        // GET: CustomerOrder
        public ActionResult Order()
        {
            ViewBag.IsOtherResp = true; //Responsive Header change (Not Home Page)
            Order_Model ord = new Order_Model();

            #region Common Start Module that intitiates and sets up the user controls.
            var currentUser = (Customer_Model)Session["CurrentUser"];

            if (Session["RegistrationDone"] == "yes" && currentUser == null)
            {
                ViewBag.RegTtl = "Registration Failed. please try again.";
            }
            else
            {
                ViewBag.RegTtl = "Registration";
            }

            if (currentUser != null)
            {
                ViewBag.UserName = currentUser.CustomerName;
                Session["LoginAttempt"] = "";
            }
            else
            {
                if (Session["LoginAttempt"] == ">0")
                {
                    ViewBag.UserName = "Login Failed. Please try again.";
                }
                else
                {
                    ViewBag.UserName = "Login";
                }
            }
            ViewBag.URL = Static_Data.BaseURL;
            #endregion

            if (Session["CurrentUser"] != null)
            {
                Customer_Model user = (Customer_Model)Session["CurrentUser"];

                List<CustmoerOrders_Model> TempData = ord.GetCustomerOrder(user.CustomerID);
                ViewBag.Orders = TempData;
                Session["UserOrders"] = TempData;
                ViewBag.ErrorMessage = "";
                return View();
            }
            else
            {
                ViewBag.ErrorMessage = "Please login for accessing the Order details";
                return View();
            }


        }

        public dynamic OrderDetails(int id)
        {
            if (Session["CurrentUser"] is Customer_Model)
            {
                ViewBag.IsOtherResp = true; //Responsive Header change (Not Home Page)
                ViewBag.Header = "2"; //Activate second header. (There are two types of headers depending upon the page being visited.)
                if (id != null && Session["UserOrders"] is List<CustmoerOrders_Model>)
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
                    #region Common Start Module that intitiates and sets up the user controls.
                    Customer_Model currentUser = (Customer_Model)Session["CurrentUser"];

                    if (Session["RegistrationDone"] == "yes" && currentUser == null)
                    {
                        ViewBag.RegTtl = "Registration Failed. please try again.";
                    }
                    else
                    {
                        ViewBag.RegTtl = "Registration";
                    }

                    if (currentUser != null)
                    {
                        ViewBag.UserName = currentUser.CustomerName;
                        Session["LoginAttempt"] = "";
                    }
                    else
                    {
                        if (Session["LoginAttempt"] == ">0")
                        {
                            ViewBag.UserName = "Login Failed. Please try again.";
                        }
                        else
                        {
                            ViewBag.UserName = "Login";
                        }
                    }
                    ViewBag.URL = Static_Data.BaseURL;

                    #endregion
                    return View();
                }
                else
                {
                    return "There was an error must be an Internal server error or your login session is expired. Please try logging in again and access this page.";
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Customer_Address()
        {

            if (Session["CurrentUser"] is Customer_Model)
            {
                //  ViewBag.Header = "2";
                ViewBag.IsOtherResp = true; //Responsive Header change (Not Home Page)
                #region Common Start Module that intitiates and sets up the user controls.
                var currentUser = (Customer_Model)Session["CurrentUser"];

                if (Session["RegistrationDone"] == "yes" && currentUser == null)
                {
                    ViewBag.RegTtl = "Registration Failed. please try again.";
                }
                else
                {
                    ViewBag.RegTtl = "Registration";
                }

                if (currentUser != null)
                {
                    ViewBag.UserName = currentUser.CustomerName;
                    Session["LoginAttempt"] = "";
                }
                else
                {
                    if (Session["LoginAttempt"] == ">0")
                    {
                        ViewBag.UserName = "Login Failed. Please try again.";
                    }
                    else
                    {
                        ViewBag.UserName = "Login";
                    }
                }
                ViewBag.URL = Static_Data.BaseURL;
                #endregion
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //public ActionResult Payment()
        //{
        //    return View();
        //}

        public dynamic getNearbyPlaces(GeoLocation_Model data)
        {

            try
            {

                //stores.Latitude = id1;
                //stores.Longitude = id2;
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "NearbyStores", "POST", JsonConvert.SerializeObject(data)));

                return JsonConvert.SerializeObject(res);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic PlaceOrder(PurchaseOrder_Model data)
        {

            try
            {
                Customer_Model user = (Customer_Model)Session["CurrentUser"];
                if (data.ExpectingDeliveryDate != null)
                {
                    DateTime date = new DateTime();
                    data.ExtraPrice = 0;
                    data.PaidAmount = 0;
                    data.orderstatusID = 2;
                    date = data.PickupDate; //?? DateTime.Now;  //if the value of pickupdate is null then add curent date and time
                    data.PickupDate = date.Date;
                    data.PickupTime = date.TimeOfDay;

                    //if the customer's Reward points and the grand total exceeds 100, deduct the reward points from total. Update Session Variable.
                    if (user.CustomerRewardPoints >= 100 && data.GrandTotal > 100)
                    {
                        data.GrandTotal = data.GrandTotal - 100;
                        user.CustomerRewardPoints = user.CustomerRewardPoints - 100;
                        data.IsReward = true;
                        data.RewardPointsApplied = 100;
                        Session["CurrentUser"] = user;
                    }
                    else
                    {
                        data.IsReward = false;
                    }
                    date = data.ExpectingDeliveryDate ?? DateTime.Now;
                    data.DeliveryTime = date.TimeOfDay;
                    WebClient client = new WebClient();
                    client.Headers[HttpRequestHeader.Accept] = "application/json";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var res = JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "Customer_PlaceOrder", "POST", JsonConvert.SerializeObject(data)));

                    return JsonConvert.SerializeObject(res);
                }
                else
                {
                    SuccessResponce_Model response = new SuccessResponce_Model();
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Expecting deliery date.";
                    return JsonConvert.SerializeObject(response);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult OrderStatusDisplay(int? id)
        {
            #region Common Start Module that intitiates and sets up the user controls.
            var currentUser = (Customer_Model)Session["CurrentUser"];

            if (Session["RegistrationDone"] == "yes" && currentUser == null)
            {
                ViewBag.RegTtl = "Registration Failed. please try again.";
            }
            else
            {
                ViewBag.RegTtl = "Registration";
            }

            if (currentUser != null)
            {
                ViewBag.UserName = currentUser.CustomerName;
                Session["LoginAttempt"] = "";
            }
            else
            {
                if (Session["LoginAttempt"] == ">0")
                {
                    ViewBag.UserName = "Login Failed. Please try again.";
                }
                else
                {
                    ViewBag.UserName = "Login";
                }
            }
            ViewBag.URL = Static_Data.BaseURL;
            #endregion
            ViewBag.IsOtherResp = true;
            //ViewBag.Header = "2";
            ViewBag.OrderID = id;
            return View();
        }


    }
}