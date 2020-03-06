using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CoreAPIWToken.Domain.Models;
using CoreAPIWToken.Domain.Services;
using CoreAPIWToken.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPIWToken.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IUserService userService;
        public readonly IMapper mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(UserResource model)
        {
            if (model == null)
                return BadRequest("model is null");
            var user = mapper.Map<UserResource, User>(model);
            var userResponse = await userService.RegisterUserAsync(user);
            if (userResponse.Success)
                return Ok(userResponse.Result);
            return BadRequest(userResponse.Message);
        }

        [HttpGet]
        [Authorize]//configure metoduna eklenen authentication middleware ile kullanılır service containerına eklenen JWT ayarlarına bağlı olarak token bekler
        public async Task<IActionResult> GetUser()
        {
            var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            //token payloadundaki claimlerden id çekilir
            if (userId == null)
                return BadRequest("No fucking user info");

            var userResponse = await userService.GetUserByIdAsync(int.Parse(userId));
            if (userResponse.Success)
                return Ok(userResponse.Result);

            return BadRequest(userResponse.Message);


        }
    }
}