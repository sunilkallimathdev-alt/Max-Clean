using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CommonClasses
{
    public class EmailSMSCredentials
    {
        ////email Cradential
        //public static string FromEmail = "info@myprelaunch.in";
        //public static string Password = "MMT@2018";
        public static string FromEmail = "info@maxcleanlaundry.in";
        public static string Password = "Max@12345";
        public static string MailUserName = "MaxClean Laundry";
        public static int Port = 25;
        public static string server = "(localdb)\MSSQLLocalDB";
        public static bool EnableSsl = false;


        //SMS Cradential
        public static string SMS_URL = "http://api.msg91.com/api/sendhttp.php?";
        public static string SMS_OauthKey = "197528A41vxSxu5a7e816b";
        public static int SMS_Country = 91;
        public static string SMS_SENDER_NAME = "MSGIND";
        public static int Route = 4;
    }
}
