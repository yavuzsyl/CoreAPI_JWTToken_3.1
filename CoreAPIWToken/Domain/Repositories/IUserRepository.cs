using CoreAPIWToken.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Domain.Repositories
{
    public interface IUserRepository
    {
        Task RegisterUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<bool> SaveRefreshTokenAsync(User user, string refreshToken);
        Task<User> GetUserWithRefreshTokenAsync(string refreshToken);
        Task RevokeRefreshTokenAsync(User user);
    }
}
