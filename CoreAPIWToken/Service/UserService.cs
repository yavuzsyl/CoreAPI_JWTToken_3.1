using CoreAPIWToken.Domain.Models;
using CoreAPIWToken.Domain.Repositories;
using CoreAPIWToken.Domain.Response;
using CoreAPIWToken.Domain.Services;
using CoreAPIWToken.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepo;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this.unitOfWork = unitOfWork;
            this.userRepo = userRepository;
        }
        public async Task<Response<User>> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return new Response<User>("email ve password girilmeilidir");

            try
            {
                var user = await userRepo.GetUserByEmailAndPasswordAsync(email, password);
                if (user != null)
                    return new Response<User>(user);
                else
                    return new Response<User>("böyle bir kullanıcı bulunamadı");

            }
            catch (Exception ex)
            {

                return new Response<User>(ex.Message);
            }
        }

        public async Task<Response<User>> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await userRepo.GetUserByIdAsync(userId);
                if (user != null)
                    return new Response<User>(user);
                else
                    return new Response<User>("böyle bir kullanıcı bulunamadı");
            }
            catch (Exception ex)
            {

                return new Response<User>(ex.Message);
            }
        }

        public async Task<Response<User>> GetUserWithRefreshTokenAsync(string refreshToken)
        {
            try
            {
                if (string.IsNullOrEmpty(refreshToken))
                    return new Response<User>("RefreshToken empty");
                var user = await userRepo.GetUserWithRefreshTokenAsync(refreshToken);
                if (user != null)
                    return new Response<User>(user);
                else
                    return new Response<User>("böyle bir kullanıcı bulunamadı");

            }
            catch (Exception ex)
            {

                return new Response<User>(ex.Message);
            }
        }

        public async Task<Response<User>> RegisterUserAsync(User user)
        {
            try
            {
                if (user == null)
                    return new Response<User>("null");
                await userRepo.RegisterUserAsync(user);
                var result = await unitOfWork.CompleteAsync();
                return result ? new Response<User>(user) : new Response<User>("fail");
            }
            catch (Exception ex)
            {

                return new Response<User>(ex.Message);
            }
        }

        public async Task<Response<User>> RevokeRefreshTokenAsync(User user)
        {
            try
            {
                if (user == null)
                    return new Response<User>("user null");
                await userRepo.RevokeRefreshTokenAsync(user);
                var result = await unitOfWork.CompleteAsync();
                return result ? new Response<User>(user) : new Response<User>("fail");
            }
            catch (Exception ex)
            {

                return new Response<User>(ex.Message);
            }
        }

        public async Task<Response<User>> SaveRefreshTokenAsync(int userId, string refreshToken)
        {
            try
            {
                var user = userRepo.GetUserByIdAsync(userId);
                if (user == null)
                    return new Response<User>("There is no user with this id");
                var isSaved = await userRepo.SaveRefreshTokenAsync(user.Result, refreshToken);
                var result = await unitOfWork.CompleteAsync();
                return result ? new Response<User>(user.Result) : new Response<User>("fail");
            }
            catch (Exception ex)
            {

                return new Response<User>(ex.Message);
            }
        }
    }
}
