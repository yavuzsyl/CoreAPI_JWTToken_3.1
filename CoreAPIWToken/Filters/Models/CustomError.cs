using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CoreAPIWToken.Filters.Models
{
    public class CustomError
    {
        public CustomError(HttpStatusCode statusCode, string customStatusMessage)
        {
            this.statusCode = statusCode;
            this.statusMessage = String.IsNullOrEmpty(customStatusMessage) ? statusCode.ToString() : customStatusMessage;
        }

        public HttpStatusCode statusCode { get; }
        public string statusMessage { get; }
        public string result { get; set; }
        public string errors { get; set; }
    }
}
