using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using API.DataAccess;

namespace API.CommonClasses
{
    public class Send_Exception_Mail
    {
        ////email Cradential

        public static string FromEmail = "info@myprelaunch.in";
        public static string Password = "MMT@2018";
        public static string MailUserName = "testcompanys";
        public static int Port = 25;
        public static string server = "198.204.247.109";
        public static bool EnableSsl = false;
        public static string ToEmail = "nmrddy111@gmail.com";
        public static string CC = "";
        public static string BCC = "";

        public static bool SendExceptionMail(Exception Ex)
        {
            try
            {
                using (DataAccess.maxcleanEntities Context = new DataAccess.maxcleanEntities())
                {
                    t_exception Objexp = new t_exception();
                    Objexp.exception_type = Convert.ToString(Ex.GetType());
                    Objexp.exception_Error = Convert.ToString(Ex.Message);
                    Objexp.exception_Page = Convert.ToString(Ex.Source);
                    Objexp.inner_exception = Convert.ToString(Ex.InnerException);
                    Objexp.date = System.TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(server);
                    mail.From = new MailAddress(FromEmail, MailUserName);
                    mail.To.Add(ToEmail);
                    mail.Subject = "Hits an Error";
                    mail.Body = JsonConvert.SerializeObject(Objexp);
                    mail.IsBodyHtml = false;
                    if (!string.IsNullOrEmpty(BCC))
                        mail.Bcc.Add(BCC);
                    if (!string.IsNullOrEmpty(CC))
                        mail.CC.Add(CC);
                    SmtpServer.Port = Port;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(FromEmail, Password);
                    SmtpServer.EnableSsl = EnableSsl;
                    SmtpServer.Send(mail);

                    Context.t_exception.Add(Objexp);
                    Context.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
