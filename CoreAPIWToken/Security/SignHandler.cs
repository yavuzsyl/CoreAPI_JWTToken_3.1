using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPIWToken.Security
{
    public static class SignHandler
    {
        public static SecurityKey GetSecurityKey(string signinKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey)); 
        }
    }
}
