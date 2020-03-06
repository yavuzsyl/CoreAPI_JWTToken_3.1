using System.Net;
using CoreAPIWToken.Filters.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPIWToken.Filters
{
    internal class CustomHttpErrorResult : JsonResult
    {
        private HttpStatusCode forbidden;

        public CustomHttpErrorResult(HttpStatusCode statusCode, string customStatusMessage = null) : base(new CustomError(statusCode, customStatusMessage))
        {
            StatusCode = (int)statusCode;
        }
    }
}