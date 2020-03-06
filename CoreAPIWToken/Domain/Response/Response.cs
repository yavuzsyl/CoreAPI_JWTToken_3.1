using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Domain.Response
{
    public class Response<T> : BaseResponse<T> where T : class
    {
        private Response(int statusCode, bool success, string message, T result) : base(statusCode, success, message) => this.Result = result;
        //başarılı
        public Response(T result) : this(200, true, "OK", result) { }
        //başarısız
        public Response(string message) : this(400, false, message, default) { }


    }
}
