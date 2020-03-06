using CoreAPIWToken.Domain.Models;
using CoreAPIWToken.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Domain.Services
{
    public interface IUserService
    {
        Task<Response<User>> RegisterUserAsync(User user);
        Task<Response<User>> GetUserByIdAsync(int userId);
        Task<Response<User>> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<Response<User>> SaveRefreshTokenAsync(int userId, string refreshToken);
        Task<Response<User>> GetUserWithRefreshTokenAsync(string refreshToken);
        Task<Response<User>> RevokeRefreshTokenAsync(User user);
    }
}
