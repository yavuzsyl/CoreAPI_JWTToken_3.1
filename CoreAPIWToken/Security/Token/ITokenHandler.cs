using CoreAPIWToken.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Security.Token
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(User user);
        void RevokeRefreshToken(User user);//client güvenli çıkış yaparsa refresh token nulla set edilir ve localstoragedan silinir

    }
}
