using API.CommonClasses;
using API.DataAccess;
using API.IRepository;
using API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace API.Repository
{
    public class Customer_Repository : Customer_IRepository
    {
        public int Add(Customer_Model ObjCustomer)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_customer.FirstOrDefault(x => x.mobile_no == ObjCustomer.MobileNumber);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        t_customer ObjCoun = new t_customer();
                        ObjCoun.customer_name = ObjCustomer.CustomerName;
                        ObjCoun.email_id = ObjCustomer.EmailID;
                        ObjCoun.mobile_no = ObjCustomer.MobileNumber;
                        ObjCoun.alt_mobile_no = ObjCustomer.AltMobileNumber;
                        ObjCoun.password = CommonClasses.Base64Encode_Decode.base64Encode(ObjCustomer.Password);
                        ObjCoun.address = ObjCustomer.Address;
                        ObjCoun.is_active = ObjCustomer.IsActive;
                        ObjCoun.created_by = ObjCustomer.CreatedBy;
                        ObjCoun.pincode = ObjCustomer.Pincode;
                        ObjCoun.old_balance = 0;
                        ObjCoun.IsMobileVerified = true;


                        ObjCoun.city = ObjCustomer.city;
                        ObjCoun.pickup_address_type = ObjCustomer.pickup_address_type;
                        ObjCoun.delivery_address_type = ObjCustomer.delivery_address_type;

                        ObjCoun.created_date = CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_customer.Add(ObjCoun);
                        Context.SaveChanges();
                        iRes = ObjCoun.customer_id;
                      
                        t_reward objrwd = new t_reward();
                        objrwd.customer_id = iRes;
                        objrwd.customer_rewardpoints = 300;
                        objrwd.is_active = true;
                        objrwd.created_date= CommonClasses.StaticData.GetCurrentDataTime;
                        Context.t_reward.Add(objrwd);
                        Context.SaveChanges();
                    }
                }
                    return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int CustomeID)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_customer customer = Context.t_customer.SingleOrDefault(x => x.customer_id == CustomeID);
                    Context.t_customer.Remove(customer);
                    Context.SaveChanges();
                }
                return iRes = CustomeID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Customer_Model ObjCustomer)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Exist = Context.t_customer.FirstOrDefault(x => x.mobile_no == ObjCustomer.MobileNumber && x.customer_id != ObjCustomer.CustomerID);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        var IDExist = Context.t_customer.SingleOrDefault(x => x.customer_id == ObjCustomer.CustomerID);
                        if (IDExist == null)
                        {
                            iRes = -4;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ObjCustomer.CustomerName))
                                IDExist.customer_name = ObjCustomer.CustomerName;
                            if (!string.IsNullOrEmpty(ObjCustomer.EmailID))
                                IDExist.email_id = ObjCustomer.EmailID;
                            if (!string.IsNullOrEmpty(ObjCustomer.MobileNumber))
                                IDExist.mobile_no = ObjCustomer.MobileNumber;
                            if (!string.IsNullOrEmpty(ObjCustomer.AltMobileNumber))
                                IDExist.alt_mobile_no = ObjCustomer.AltMobileNumber;
                            if (ObjCustomer.Password != null)
                                IDExist.password = CommonClasses.Base64Encode_Decode.base64Encode(ObjCustomer.Password);


                            if (ObjCustomer.Address != null)
                                IDExist.address = ObjCustomer.Address;
                            if (ObjCustomer.IsActive != null)
                                IDExist.is_active = ObjCustomer.IsActive;
                            if (ObjCustomer.IsActive != null)
                                IDExist.IsMobileVerified = ObjCustomer.isMobileVerified;


                            if (ObjCustomer.city != null)
                                IDExist.city = ObjCustomer.city;
                            if (ObjCustomer.pickup_address_type != 0)
                                IDExist.pickup_address_type = ObjCustomer.pickup_address_type;
                            if (ObjCustomer.delivery_address_type != 0)
                                IDExist.delivery_address_type = ObjCustomer.delivery_address_type;


                            IDExist.updated_by = ObjCustomer.UpdateBy;
                            IDExist.updated_date = CommonClasses.StaticData.GetCurrentDataTime;
                            Context.SaveChanges();
                            iRes = ObjCustomer.CustomerID;
                        }
                    }
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Customer_Model> GetActiveCustomers()
        {
            List<Customer_Model> ObjCustomers = new List<Customer_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCustomers = (from a in Context.t_customer
                                           where a.is_active == true
                                           select new Customer_Model
                                           {
                                               CustomerID = a.customer_id,
                                               CustomerName = a.customer_name,

                                               RewardID = a.t_reward.Where(p => p.customer_id == a.customer_id).Select(n => n.reward_id).FirstOrDefault(),
                                               CustomerRewardPoints = a.t_reward.Where(p => p.customer_id == a.customer_id).Select(n => n.customer_rewardpoints).FirstOrDefault(),

                                               EmailID = a.email_id,
                                               MobileNumber = a.mobile_no,
                                               AltMobileNumber = a.alt_mobile_no,
                                               OldBalance =a.old_balance,
                                               Address = a.address,
                                               Pincode = a.pincode,
                                               Password = a.password,
                                               IsActive = a.is_active,
                                               CreatedBy = a.created_by,
                                               CreatedDate = a.created_date,
                                               UpdateBy = a.updated_by,
                                               UpdateDate = a.updated_date,

                                               city=a.city,
                                               pickup_address_type=a.pickup_address_type,
                                               delivery_address_type=a.delivery_address_type
                                           }).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Customer_Model> GetAllCustomers()
        {
            List<Customer_Model> ObjCustomers = new List<Customer_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    return ObjCustomers = (from a in Context.t_customer
                                           select new Customer_Model
                                           {
                                               CustomerID = a.customer_id,
                                               CustomerName = a.customer_name,

                                               RewardID = a.t_reward.Where(p => p.customer_id == a.customer_id).Select(n => n.reward_id).FirstOrDefault(),
                                               CustomerRewardPoints = a.t_reward.Where(p => p.customer_id == a.customer_id).Select(n => n.customer_rewardpoints).FirstOrDefault(),

                                               Total_Cust_Orders = a.t_orders.Where(c => c.customer_id == a.customer_id).Select(c => c.order_id).Count(),
                                               Open_Cust_Orders = a.t_orders.Where(c => c.customer_id == a.customer_id && c.order_status < 11).Select(c => c.order_id).Count(),
                                               Closed_Cust_Orders = a.t_orders.Where(c => c.customer_id == a.customer_id && c.order_status == 11).Select(c => c.order_id).Count(),
                                               EmailID = a.email_id,
                                               OldBalance=a.old_balance,
                                               MobileNumber = a.mobile_no,
                                               AltMobileNumber = a.alt_mobile_no,
                                               Pincode = a.pincode,
                                               Address = a.address,
                                               Password = a.password,
                                               IsActive = a.is_active,
                                               CreatedBy = a.created_by,
                                               CreatedDate = a.created_date,
                                               UpdateBy = a.updated_by,
                                               UpdateDate = a.updated_date,

                                               city = a.city,
                                               pickup_address_type = a.pickup_address_type,
                                               delivery_address_type = a.delivery_address_type
                                           }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Customer_Model GetCustomerByID(int CustomeID)
        {
            Customer_Model ObjCustomers = new Customer_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var ObjCustomer = (from a in Context.t_customer
                                       where a.customer_id == CustomeID
                                       select new Customer_Model
                                       {
                                           CustomerID = a.customer_id,
                                           CustomerName = a.customer_name,

                                           RewardID = a.t_reward.Where(p => p.customer_id == a.customer_id).Select(n => n.reward_id).FirstOrDefault(),
                                           CustomerRewardPoints = a.t_reward.Where(p => p.customer_id == a.customer_id).Select(n => n.customer_rewardpoints).FirstOrDefault(),
                                           CustomerUsedRewardPoints = a.t_reward.Where(p => p.customer_id == a.customer_id).Select(n => n.customer_usedrewards).FirstOrDefault(),
                                           OldBalance = a.old_balance,
                                           EmailID = a.email_id,
                                           MobileNumber = a.mobile_no,
                                           AltMobileNumber = a.alt_mobile_no,
                                           Address = a.address,
                                           PickUPAddress = a.address,
                                           DeliveryAddress = a.deliveryAddress,
                                           Password = a.password,
                                           Pincode = a.pincode,
                                           IsActive = a.is_active,
                                           CreatedBy = a.created_by,
                                           CreatedDate = a.created_date,
                                           UpdateBy = a.updated_by,
                                           UpdateDate = a.updated_date,

                                           city = a.city,
                                           pickup_address_type = a.pickup_address_type,
                                           delivery_address_type = a.delivery_address_type
                                       }).SingleOrDefault();
                    ObjCustomer.Password = CommonClasses.Base64Encode_Decode.base64Decode2(ObjCustomer.Password);
                    ObjCustomers = ObjCustomer;
                    return ObjCustomers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Customer_Model GetCustomerID(Customer_Model Customer)
        {
            Customer_Model ObjCustomers = new Customer_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var ObjCustomer = (from a in Context.t_customer

                                       select new Customer_Model
                                       {
                                           CustomerID = a.customer_id,
                                           CustomerName = a.customer_name,

                                       });
                    if (Customer.CustomerName != null)
                    {
                        ObjCustomer = ObjCustomer.Where(a => a.CustomerName == Customer.CustomerName).AsQueryable();
                    }
                    if (Customer.MobileNumber != null)
                    {
                        ObjCustomer = ObjCustomer.Where(a => a.MobileNumber == Customer.MobileNumber).AsQueryable();
                    }
                    ObjCustomers = ObjCustomer.SingleOrDefault();
                    return ObjCustomers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Forgot PasswordModule
        public bool ForgotPassword(Customer_Model obj)
        {
            bool passwordrequest = false;
            Customer_Model ObjUser = new Customer_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Res = Context.t_customer.FirstOrDefault(c => c.email_id == obj.EmailID);
                    if (Res != null)
                    {
                        if (Res.email_id != null)
                        {
                            ObjUser.CustomerName = Res.customer_name;
                            ObjUser.CustomerID = Res.customer_id;
                            ObjUser.EmailID = Res.email_id;
                            ObjUser.Password = Res.password;
                            var emailFrom = API.CommonClasses.EmailSMSCredentials.FromEmail;
                            var password = API.CommonClasses.EmailSMSCredentials.Password;
                            AdminLogin_Repository fr = new AdminLogin_Repository();
                            if (ObjUser.EmailID != null)
                            {
                                fr.SendMail(
                                    ObjUser.EmailID,
                                    "Company Password Recovery",
                                    emailFrom,
                                    password,
                                    "Dear Customer,"
                                    + Environment.NewLine + ""
                                    + Environment.NewLine + "Welcome to Company."
                                    + Environment.NewLine + ""
                                    + Environment.NewLine + "Use the following values when prompted to log in:"
                                    + Environment.NewLine + "Password : " + CommonClasses.Base64Encode_Decode.base64Decode2(ObjUser.Password)
                                    + Environment.NewLine + "Click here to login: https://maxcleanlaundry.in"
                                    + Environment.NewLine + ""
                                    + Environment.NewLine + ""
                                    + Environment.NewLine + "Thanks and Regards"
                                    + Environment.NewLine + "Team testcompanys Technologies.");
                            }
                            passwordrequest = true;
                        }
                    }
                    else
                    {
                        passwordrequest = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return passwordrequest;
        }
        public bool SendMail(string sEmailTo, string sEmailSubject, string sEmailFrom, string sPassword, string sEmailContent)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(EmailSMSCredentials.server);
                mail.From = new MailAddress(EmailSMSCredentials.FromEmail);
                mail.To.Add(sEmailTo);
                mail.IsBodyHtml = false;
                mail.Subject = sEmailSubject;
                mail.Body = sEmailContent;
                SmtpServer.Port = EmailSMSCredentials.Port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(EmailSMSCredentials.FromEmail, EmailSMSCredentials.Password);
                SmtpServer.EnableSsl = EmailSMSCredentials.EnableSsl;
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Forgot password module ends


        public dynamic GetNonPerformingCustomer()
        {
            List<Customer_Model> obj = new List<Customer_Model>();
            List<int> IDs = new List<int>();
            try
            {
                using (maxcleanEntities Context = new maxcleanEntities())
                {

                    var Customers = Context.t_customer.Select(x => x.customer_id).ToList();
                    
                    var previousmonth = CommonClasses.StaticData.GetCurrentDataTime.AddMonths(-1);
                    var ordersCustomerIDs = Context.t_orders.Where(x => x.order_date >= previousmonth).Select(x => x.customer_id).ToList();

                    foreach (var ID in Customers)
                    {
                        if (!ordersCustomerIDs.Contains(ID))
                        {
                            obj.Add((from a in Context.t_customer
                                     where a.customer_id == ID
                                     select new Customer_Model
                                     {
                                         CustomerID = a.customer_id,
                                         CustomerName = a.customer_name,

                                         RewardID = a.t_reward.Where(p => p.customer_id == a.customer_id).Select(n => n.reward_id).FirstOrDefault(),
                                         CustomerRewardPoints = a.t_reward.Where(p => p.customer_id == a.customer_id).Select(n => n.customer_rewardpoints).FirstOrDefault(),

                                         OldBalance=a.old_balance,
                                         EmailID = a.email_id,
                                         MobileNumber = a.mobile_no,
                                         AltMobileNumber = a.alt_mobile_no,
                                         Address = a.address,
                                         Password = a.password,
                                         Pincode = a.pincode,
                                         IsActive = a.is_active,
                                         CreatedBy = a.created_by,
                                         CreatedDate = a.created_date,
                                         UpdateBy = a.updated_by,
                                         UpdateDate = a.updated_date,
                                         city = a.city,
                                         pickup_address_type = a.pickup_address_type,
                                         delivery_address_type = a.delivery_address_type
                                     }).SingleOrDefault());
                        }
                    }
                    

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public dynamic GetOrders_of_Customer(int CustomerID)
        {
            List<PurchaseOrder_Model> ordlist = new List<PurchaseOrder_Model>();
            try {
                using(maxcleanEntities context = new maxcleanEntities())
                {
                    return ordlist = (from a in context.t_orders
                                where a.customer_id== CustomerID
                                select new PurchaseOrder_Model
                                {
                                    OrderID =a.order_id,
                                    OrderDate = a.order_date,
                                    GrandTotal = a.grandtotal_price,
                                    CustomerName=a.t_customer.customer_name,
                                    CustomerMobile=a.t_customer.mobile_no,
                                    CustomerEmail=a.t_customer.email_id,
                                    Pincode = a.pincode,
                                    PaidAmount =a.paid_amount,
                                    RemainingAmount =a.balance_amount,
                                    orderstatusID = a.order_status,
                                    OrderStatus = a.t_order_cylce.order_cycle_stage
                                }).ToList();


                }
            } catch(Exception ex) {
                throw ex;
            }
        }
        //used for mobile app
        public List<ActiveOrders_Model> GetOrders_of_Customer_mobile(int CustomerID)
        {
            List<ActiveOrders_Model> ordlist = new List<ActiveOrders_Model>();
            try
            {
                using (maxcleanEntities context = new maxcleanEntities())
                {
                    return ordlist = (from a in context.t_orders
                                      where a.customer_id == CustomerID
                                      select new ActiveOrders_Model
                                      {
                                          OrderID = a.order_id,
                                          OrderDate = a.order_date,
                                          GrandTotal = a.grandtotal_price,
                                          PaidAmount = a.paid_amount,
                                          orderstatusID = a.order_status,
                                          OrderStatus = a.t_order_cylce.order_cycle_stage
                                      }).ToList();


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetActiveOrders_of_Customer(int Customer_ID)
        {
            List<ActiveOrders_Model> ordlist = new List<ActiveOrders_Model>();
            try
            {
                using (maxcleanEntities context = new maxcleanEntities())
                {
                    return ordlist = (from a in context.t_orders
                                      where a.customer_id == Customer_ID && a.order_status <11
                                      select new ActiveOrders_Model
                                      {
                                          OrderID = a.order_id,
                                          OrderDate = a.order_date,
                                          GrandTotal = a.grandtotal_price,
                                          PaidAmount = a.paid_amount,
                                          orderstatusID = a.order_status,
                                          OrderStatus = a.t_order_cylce.order_cycle_stage
                                      }).ToList();


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public dynamic login(Customer_Model ObjCus)
        {
            Customer_Model ObjCustomers = new Customer_Model();
            try
            {
                var encPwd = CommonClasses.Base64Encode_Decode.base64Encode(ObjCus.Password);
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var ObjCustomer = (from a in Context.t_customer
                                       where a.mobile_no == ObjCus.MobileNumber && a.password == encPwd 
                                       select new Customer_Model
                                       {
                                           CustomerID = a.customer_id,
                                           CustomerName = a.customer_name,
                                           isMobileVerified = a.IsMobileVerified,
                                           RewardID = a.t_reward.Where(p => p.customer_id == a.customer_id).Select(n => n.reward_id).FirstOrDefault(),
                                           CustomerRewardPoints = a.t_reward.Where(p => p.customer_id == a.customer_id).Select(n => n.customer_rewardpoints).FirstOrDefault(),
                                           CustomerUsedRewardPoints = a.t_reward.Where(p => p.customer_id == a.customer_id).Select(n => n.customer_usedrewards).FirstOrDefault(),
                                           OldBalance =a.old_balance,
                                           EmailID = a.email_id,
                                           MobileNumber = a.mobile_no,
                                           AltMobileNumber = a.alt_mobile_no,
                                           Pincode = a.pincode,
                                           Address = a.address,
                                           Password = a.password,
                                           IsActive = a.is_active,
                                           CreatedBy = a.created_by,
                                           CreatedDate = a.created_date,
                                           UpdateBy = a.updated_by,
                                           UpdateDate = a.updated_date,
                                           city = a.city,
                                           pickup_address_type = a.pickup_address_type,
                                           delivery_address_type = a.delivery_address_type
                                       }).SingleOrDefault();
                    if(ObjCustomer!=null)
                    ObjCustomer.Password = CommonClasses.Base64Encode_Decode.base64Decode2(ObjCustomer.Password);
                    ObjCustomers = ObjCustomer;
                    return ObjCustomers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SendOTP(string mobile)
        {
           // if (checkNumberExits(mobile) > 0)
           // {
                Random rand = new Random();
            var otp = rand.Next(999, 10000); //Generate a random 4 digit number.

            var SMSbody = "Dear Customer" + "," + Environment.NewLine + " Your OTP for registration is: " + Environment.NewLine + otp + Environment.NewLine + "For any queries, please reach out to 9900318000." + Environment.NewLine + "Order online from www.maxcleanlaundry.in " + Environment.NewLine + "Open Hours: 09:00 AM to 09:00 PM";
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

           // }
           // else
            //{
           //     return -1;
          //  }

        }

        public dynamic OTPVerify(int Cus_ID)
        {
            var iRes = 0;
            using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
            {
                var IDExist = Context.t_customer.SingleOrDefault(x => x.customer_id == Cus_ID);
                IDExist.IsMobileVerified = true;
                Context.SaveChanges();
                iRes = IDExist.customer_id;
            }
            return iRes;
        }

        public int checkNumberExits(string mobile)
        {
            Customer_Model ObjCustomers = new Customer_Model();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var ObjCustomer = (from a in Context.t_customer
                                       where a.mobile_no == mobile
                                       select new Customer_Model
                                       {
                                           CustomerID = a.customer_id
                                          
                                       }).SingleOrDefault();
                    ObjCustomers = ObjCustomer;
                    if (ObjCustomers != null)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Customer_Model> GetCustomers_ByDates(Customer_Model dates)
        {
            List<Customer_Model> ObjCustomers = new List<Customer_Model>();
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    var Obj = (from a in Context.t_customer
                               select new Customer_Model
                               {
                                   CustomerID = a.customer_id,
                                   CustomerName = a.customer_name,

                                   RewardID = a.t_reward.Where(p => p.customer_id == a.customer_id).Select(n => n.reward_id).FirstOrDefault(),
                                   CustomerRewardPoints = a.t_reward.Where(p => p.customer_id == a.customer_id).Select(n => n.customer_rewardpoints).FirstOrDefault(),

                                   Pincode = a.pincode,
                                   EmailID = a.email_id,
                                   OldBalance = a.old_balance,
                                   MobileNumber = a.mobile_no,
                                   AltMobileNumber = a.alt_mobile_no,
                                   Address = a.address,
                                   Password = a.password,
                                   IsActive = a.is_active,
                                   CreatedBy = a.created_by,
                                   CreatedDate = a.created_date,
                                   UpdateBy = a.updated_by,
                                   UpdateDate = a.updated_date
                               });
                    if (dates.FromDate != null)
                    {
                        Obj = Obj.Where(a => a.CreatedDate >= dates.FromDate).AsQueryable();
                    }
                    if (dates.ToDate != null)
                    {
                        Obj = Obj.Where(a => a.CreatedDate <= dates.ToDate).AsQueryable();
                    }
                  return  ObjCustomers = Obj.OrderByDescending(a => a.CreatedDate).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic Promocode(Promocode_Model ObjCus)
        {
            Promocode_Model ObjCustomers = new Promocode_Model();
            try
            {               
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                     ObjCustomers = (from a in Context.t_promocode
                                       where a.promocode_name == ObjCus.Promocode_Name && a.is_active==true
                                     
                                       select new Promocode_Model
                                       {
                                          Promocode_ID = a.promocode_id,
                                          Promocode_Name = a.promocode_name,
                                          Discount= a.discount,
                                       }).SingleOrDefault();
                   
                    return ObjCustomers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddCustomerRating(CustomerRating_Model ObjCustomer)
        {
            int iRes = 0;
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {


                    t_customer_rating objrwd = new t_customer_rating();
                    objrwd.customer_id = ObjCustomer.CustomerID;
                    objrwd.order_id = ObjCustomer.OrderID;
                    objrwd.rating = ObjCustomer.Rating;
                    objrwd.comments = ObjCustomer.Comments;
                    Context.t_customer_rating.Add(objrwd);
                    Context.SaveChanges();
                    iRes = objrwd.customer_rating_id;
                }
                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}