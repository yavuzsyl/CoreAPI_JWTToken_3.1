using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CoreAPIWToken.Filters
{
    public class AuthorizeFilter : Attribute, IAuthorizationFilter
    {
        public string ClientApps { get; set; }
        public string Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
                context.Result = new UnauthorizedResult();
            else
            {
                string[] clientApps = default(string[]);
                string[] roles = default(string[]);

                if (!string.IsNullOrEmpty(ClientApps) && !string.IsNullOrWhiteSpace(ClientApps))
                {
                    clientApps = ClientApps.Trim().Split().ToArray();
                    string clientAppClaim = context.HttpContext.User.Claims.Where(c => c.Type.Equals("clientApp")).FirstOrDefault().Value;

                    if (!clientApps.Contains(clientAppClaim))
                        context.Result = new CustomHttpErrorResult(HttpStatusCode.Forbidden);
                }
            }
        }
    }
}
