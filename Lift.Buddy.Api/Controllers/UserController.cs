using Lift.Buddy.API.Interfaces;
using Lift.Buddy.API.Services;
using Lift.Buddy.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lift.Buddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usersService;

        public UserController(IUserService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUsername([FromRoute] string username)
        {
            var response = await _usersService.GetUsersByUsername(username);
            return Ok(response);
        }

        #region User Data
        [HttpGet]
        public async Task<IActionResult> GetUserData()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return NotFound();

            var response = await _usersService.GetUserData(Guid.Parse(userId));
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserData([FromBody] UserDTO userData)
        {
            await _usersService.UpdateUserData(userData);
            return NoContent();
        }
        #endregion
    }
}
