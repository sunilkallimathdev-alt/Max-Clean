using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MaxCleanWebV1.Models
{
    public class StaticData
    {
        public class Static_Data
        {

            //#if DEBUG
            //static string _name = "http://test.in/api/";
            //#else      

            // static string _name = http://http://test.in/api/api/;
            //#endif

            static string _name = System.Configuration.ConfigurationManager.AppSettings["BaseURL"];
            public static string BaseURL { get { return _name; } }
        }

        public class JStatusCode
        {
            [JsonProperty("StatusCode")]
            public HttpStatusCode StatusCode { get; set; }

            [JsonProperty("Message")]
            public string Message { get; set; }

            [JsonProperty("Result")]
            public int? Result { get; set; }

            [JsonProperty("Data")]
            public dynamic Data { get; set; }

            [JsonProperty("Log")] // The converted and validated object extracted from the api body.
            public dynamic Log { get; set; }

            [JsonProperty("Log1")] //Used to send in the object user sent to server (Whisper back).
            public dynamic Log1 { get; set; }

            [JsonProperty("Log2")]    //Optional field used for the same as above.
            public dynamic Log2 { get; set; }
        }

    }
}