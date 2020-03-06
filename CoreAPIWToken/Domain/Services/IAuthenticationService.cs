using CoreAPIWToken.Domain.Response;
using CoreAPIWToken.Security.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<Response<AccessToken>> CreateAccessToken(string email, string password);
        Task<Response<AccessToken>> CreateAccessTokenWithRefreshToken(string refreshToken);
        Task<Response<AccessToken>> RevokeRefreshToken(string refreshToken);

    }
}
