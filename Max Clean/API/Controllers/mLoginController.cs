using API.CommonClasses;
using API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace API.Controllers
{
    public class mLoginController : ApiController
    {

        API.IRepository.Customer_IRepository ObjStores = new API.Repository.Customer_Repository();
        SuccessResponce_Model ObjSuccessResponce_Model = new SuccessResponce_Model();
        ErrorResponce ObjErrorResponce = new ErrorResponce();

        [HttpPost]
        public dynamic Post(Customer_Model value)
        {
            try
            {
                int Res = 0;
                using (DataAccess.maxcleanEntities context = new DataAccess.maxcleanEntities())
                {

                    var isExist = context.t_customer.FirstOrDefault(x => x.mobile_no == value.MobileNumber);
                    if (isExist != null)
                    {
                        Res = SendOTP(value.MobileNumber);
                    }
                    
                    if (Res != 0)
                    {
                        ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                        ObjSuccessResponce_Model.Message = "Success";
                        ObjSuccessResponce_Model.CustomerID = isExist.customer_id;
                    }
                    else
                    {
                        ObjSuccessResponce_Model.StatusCode = HttpStatusCode.BadRequest;
                        ObjSuccessResponce_Model.Message = "Not Registered";
                    }
                    ObjSuccessResponce_Model.ObjectResult = Res;
                   
                }
               
                
                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                //Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                ObjErrorResponce.ReceivedParameters = value;
                return ObjErrorResponce;
            }
        }

        public dynamic Put(Customer_Model objAdmin)
        {
            try
            {
                ObjSuccessResponce_Model.StatusCode = HttpStatusCode.OK;
                ObjSuccessResponce_Model.ObjectResult = ObjStores.ForgotPassword(objAdmin);
                ObjSuccessResponce_Model.ReceivedParameters = objAdmin;

                return ObjSuccessResponce_Model;
            }
            catch (Exception ex)
            {
                Send_Exception_Mail.SendExceptionMail(ex);
                ObjErrorResponce.StatusCode = HttpStatusCode.InternalServerError;
                ObjErrorResponce.Message = "Failure : " + ex.Message.ToString();
                ObjErrorResponce.ReceivedParameters = objAdmin;

                return ObjErrorResponce;
            }
        }

        public int SendOTP(string mobile)
        {
            // if (checkNumberExits(mobile) > 0)
            // {
            Random rand = new Random();
            var otp = rand.Next(999, 10000); //Generate a random 4 digit number.

            var SMSbody = "Dear Customer" + "," + Environment.NewLine + " Your OTP for Login is: " + Environment.NewLine + otp + Environment.NewLine + "For any queries, please reach out to 9900318000." + Environment.NewLine + "Order online from www.maxcleanlaundry.in " + Environment.NewLine + "Open Hours: 09:00 AM to 09:00 PM";
            //var SMSbody = "Dear " + customerdetails.customer_name + ",Company Order No : " + orderdetails.order_id + " Qty.: " + ObjPO.PurchaseOrderDetail.Sum(x => x.Quantity) + " Amt.   : " + orderdetails.grandtotal_price + ".00 INR Status : Order Confirmed ";


            //Send the randomnumber to the customer.
            StringBuilder baseurl = new StringBuilder();
            baseurl.Append(EmailSMSCredentials.SMS_URL);
            baseurl.Append("sender=" + EmailSMSCredentials.SMS_SENDER_NAME);
            baseurl.Append("&route=" + EmailSMSCredentials.Route);
            baseurl.Append("&mobiles=" + mobile);
            baseurl.Append("&authkey=" + EmailSMSCredentials.SMS_OauthKey);
            baseurl.Append("&country=" + EmailSMSCredentials.SMS_Country);
            baseurl.Append("&message=" + SMSbody);

            WebClient client = new WebClient();
            Stream data = client.OpenRead(baseurl.ToString());
            StreamReader reader = new StreamReader(data);
            var Status = reader.ReadToEnd();
            data.Close();
            reader.Close();

            //Return to the function.
            return otp;


        }


    }
}
