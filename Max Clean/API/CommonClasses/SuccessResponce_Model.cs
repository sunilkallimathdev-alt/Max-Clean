using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace API.CommonClasses
{
    public class SuccessResponce_Model
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = "Success";
        public dynamic Result { get; set; } = 0;
        public dynamic CustomerID { get; set; } = 0;
        public dynamic ObjectResult { get; set; }
        public dynamic ReceivedParameters { get; set; }
    }
}