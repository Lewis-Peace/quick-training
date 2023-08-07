using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lift.Buddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginService _loginService;

        public LoginController(IConfiguration configuration, ILoginService loginService)
        {
            _configuration = configuration;
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginCredentials loginCredentials) {
            if (loginCredentials == null || !_loginService.CheckCredentials(loginCredentials))
            {
                return Unauthorized();
            }
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", loginCredentials.Username!));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(5),
                signingCredentials
                );

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);
            var loginResp = new Response
            {
                result = true,
                body = tokenToReturn,
                notes = ""
            };
            return Ok(loginResp);
        }

        [Authorize]
        [HttpPost("changePassword")]
        public IActionResult ChangePassword([FromBody] LoginCredentials loginCredentials)
        {
            var username = loginCredentials.Username;
            var newPassowrd = loginCredentials.Password;
            return Ok();
        }
    }
}
