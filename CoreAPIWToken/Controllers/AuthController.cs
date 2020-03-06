using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoreAPIWToken.Domain.Services;
using CoreAPIWToken.Extensions;
using CoreAPIWToken.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPIWToken.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService authService;
        public AuthController(IAuthenticationService authenticationService)
        {
            authService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> AccessToken([FromBody]AuthResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var token = await authService.CreateAccessToken(model.Email, model.Password);
            if (!token.Success)
                return BadRequest(token.Message);
            return Ok(token.Result);
        }
        [HttpPost]
        public async Task<IActionResult> GetAccessTokenWithRefreshToken([FromBody]string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Empty");
            var token = await authService.CreateAccessTokenWithRefreshToken(refreshToken);
            if (!token.Success)
                return BadRequest(token.Message);
            return Ok(token.Result);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveRefreshToken([Required][FromBody]string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Empty");
            var result = await authService.RevokeRefreshToken(refreshToken);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Result);
        }
    }
}