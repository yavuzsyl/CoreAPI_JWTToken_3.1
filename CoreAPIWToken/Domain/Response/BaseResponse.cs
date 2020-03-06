using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Domain.Response
{
    public class BaseResponse<T> where T : class
    {
        public BaseResponse(int statusCode, bool success, string message)
        {
            this.StatusCode = statusCode;
            this.Success = success;
            this.Message = message;
        }
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
