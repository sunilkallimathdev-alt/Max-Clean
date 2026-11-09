using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace API.CommonClasses
{
    public class MailSender
    {
        public static bool SendMail(string MailTo, string Subject, string MailBody, string CC, string BCC)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(EmailSMSCredentials.server);
                mail.From = new MailAddress(EmailSMSCredentials.FromEmail, EmailSMSCredentials.MailUserName);
                mail.To.Add(MailTo);
                mail.Subject = Subject;
                mail.Body = MailBody;
                mail.IsBodyHtml = true;
                if (!string.IsNullOrEmpty(BCC))
                    mail.Bcc.Add(BCC);
                if (!string.IsNullOrEmpty(CC))
                    mail.CC.Add(CC);
                SmtpServer.Port = EmailSMSCredentials.Port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(EmailSMSCredentials.FromEmail, EmailSMSCredentials.Password);
                SmtpServer.EnableSsl = EmailSMSCredentials.EnableSsl;
                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}