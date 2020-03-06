using CoreAPIWToken.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CoreAPIWToken.Security.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly TokenOptions tokenOptions;

        public TokenHandler(IOptions<TokenOptions> tokenOptions)
        {
            this.tokenOptions = tokenOptions.Value;
        }
        public AccessToken CreateAccessToken(User user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);//token expire zamanı belirlenir
            var securityKey = SignHandler.GetSecurityKey(tokenOptions.SecurityKey);//tokenı imazalamk için secirtykey oluşturulur
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);//secuirtykey seçilen algoritma ile şifrelenir

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims: GetClaims(user)

                );

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            return new AccessToken()
            {
                Token = token,
                Expiration = accessTokenExpiration,
                RefreshToken = CreateRefreshToken()
            };
        }
        private IEnumerable<Claim> GetClaims(User user)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(ClaimTypes.Name,$"{user.Name} {user.SurName}"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
        }
        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];//32 byte uzunluğunda array 
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(numberByte);//numberByte arrayi dolduruldu
                return Convert.ToBase64String(numberByte);
            }
        }
        public void RevokeRefreshToken(User user)
        {
            user.RefreshToken = null; //güvenli çıkış db ye kayıt service de yapılacak
        }
    }
}
