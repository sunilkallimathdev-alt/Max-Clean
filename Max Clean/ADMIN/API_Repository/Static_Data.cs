using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;

namespace ADMIN.API_Repository
{
    public class Static_Data
    {
        //static string _name = "http://localhost:49170/api/";
        //static string _name = "http://maxcleanapi.myprelaunch.in/api/";
        //static string _name = "http://maxcleanapilocal.myprelaunch.in/api/";
        //static string _name = "http://api.maxcleanlaundry.in/api/";

        static string _name = System.Configuration.ConfigurationManager.AppSettings["BaseURL"];
        public static string BaseURL { get { return _name; } }

        //"whttp://control.msg91.com/api/balance.php?type=4&authkey=197528A41vxSxu5a7e816b";
    }
}
