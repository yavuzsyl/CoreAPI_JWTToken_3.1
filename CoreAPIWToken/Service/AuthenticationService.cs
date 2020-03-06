using CoreAPIWToken.Domain.Response;
using CoreAPIWToken.Domain.Services;
using CoreAPIWToken.Domain.UnitOfWork;
using CoreAPIWToken.Security.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService userService;
        private readonly ITokenHandler tokenHandler;
        private readonly IUnitOfWork unitOfWork;
        public AuthenticationService(IUserService userService, ITokenHandler tokenHandler, IUnitOfWork unitOfWork)
        {
            this.userService = userService;
            this.tokenHandler = tokenHandler;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response<AccessToken>> CreateAccessToken(string email, string password)
        {
            var user = await userService.GetUserByEmailAndPasswordAsync(email, password);
            if (!user.Success)
                return new Response<AccessToken>(user.Message);

            var accessToken = tokenHandler.CreateAccessToken(user.Result);
            if (accessToken == null)
                return new Response<AccessToken>("Token couldnt created");

            var refResult = await userService.SaveRefreshTokenAsync(user.Result.Id, accessToken.RefreshToken);

            return new Response<AccessToken>(accessToken);
        }

        public async Task<Response<AccessToken>> CreateAccessTokenWithRefreshToken(string refreshToken)
        {
            var user = await userService.GetUserWithRefreshTokenAsync(refreshToken);
            if (!user.Success)
                return new Response<AccessToken>(user.Message);

            if (user.Result.RefreshTokenEndDate > DateTime.Now)
                return new Response<AccessToken>("RefreshTokenın süresü dolmuş");

            var accessToken = tokenHandler.CreateAccessToken(user.Result);
            if (accessToken == null)
                return new Response<AccessToken>("Token couldnt created");

            var refResult = await userService.SaveRefreshTokenAsync(user.Result.Id, accessToken.RefreshToken);

            return new Response<AccessToken>(accessToken);
        }

        public async Task<Response<AccessToken>> RevokeRefreshToken(string refreshToken)
        {
            var user = await userService.GetUserWithRefreshTokenAsync(refreshToken);
            if (!user.Success)
                return new Response<AccessToken>(user.Message);
            var refRevokeResult = await userService.RevokeRefreshTokenAsync(user.Result);
            if (!refRevokeResult.Success)
                return new Response<AccessToken>(refRevokeResult.Message);
            return new Response<AccessToken>(new AccessToken());
        }
    }
}
