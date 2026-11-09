using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace API.CommonClasses
{
    public class ErrorResponce
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public dynamic ReceivedParameters { get; set; }
    }
}