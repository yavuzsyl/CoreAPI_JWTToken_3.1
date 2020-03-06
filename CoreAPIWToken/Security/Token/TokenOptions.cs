using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Security.Token
{
    public class TokenOptions
    {
        public string Audience { get; set; }//tokenı dinleyecek olanlar
        public string Issuer { get; set; }//tokenı oluşturup-dağıtacak olan
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string SecurityKey { get; set; }//token oluştururken kullanılacak olan verification signature

    }
}
