using Newtonsoft.Json;
using System.Net;

namespace MaxCleanWeb.Models
{
    public class Static_Data
     {

#if DEBUG
        static string _name = "http://localhost:49170/api/";
#else       //static string _name = "http://test.in/api/";
        static string _name = "http://maxcleanapilocal.myprelaunch.in/api/";
#endif
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