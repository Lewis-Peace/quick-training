using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.DB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lift.Buddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PRController : ControllerBase
    {
        private readonly IPRService _prService;
        public PRController(IPRService prSservice) 
        {
            _prService = prSservice;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var response = await _prService.GetByUser(username);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserPR userPR)
        {
            var response = await _prService.AddPR(userPR);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserPR userPR)
        {
            var response = await _prService.UpdatePR(userPR);
            return Ok(response);
        }
    }
}
