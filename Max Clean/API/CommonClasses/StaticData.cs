using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.CommonClasses
{
    public class StaticData
    {
        static DateTime _name = System.TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        public static DateTime GetCurrentDataTime { get { return _name; } }
       
        //static string _server = @"C:\Inetpub\vhosts\myprelaunch.in\";
       // static string _server = @"C:\Inetpub\vhosts\maxcleanlaundry.in\";
        static string _server =System.Configuration.ConfigurationManager.AppSettings["_server"];
        public static string ServerDirectory { get { return _server; } }


        //public static double Incentive = 10;
        static double _Incentive = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["_Incentive"]);
        public static double Incentive { get { return _Incentive; } }

        static double _Rewards = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["_Rewards"]);
        public static double Rewards { get { return _Rewards; } }

        static double _NoOfOrderPerTimeSlab = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["_NoOfOrderPerTimeSlab"]);
        public static double NoOfOrderPerTimeSlab { get { return _NoOfOrderPerTimeSlab; } }

    }
}