using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lift.Buddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FrontpageController : ControllerBase
    {
        private readonly IFrontpageService _frontpageService;

        public FrontpageController(IFrontpageService frontpageService)
        {
            _frontpageService = frontpageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFrontpage()
        {
            var trainerGuidString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (trainerGuidString == null)
            {
                return StatusCode(500);
            }

            var response = await _frontpageService.GetFrontpage(Guid.Parse(trainerGuidString));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddFrontpage([FromBody] FrontpageDTO frontpage)
        {
            var trainerGuidString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (trainerGuidString == null)
            {
                return StatusCode(500);
            }

            var response = await _frontpageService.AddFrontpage(Guid.Parse(trainerGuidString), frontpage);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFrontpage([FromBody] FrontpageDTO frontpage)
        {
            var trainerGuidString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (trainerGuidString == null)
            {
                return StatusCode(500);
            }

            var response = await _frontpageService.UpdateFrontpage(Guid.Parse(trainerGuidString), frontpage);
            return Ok(response);
        }
    }
}
