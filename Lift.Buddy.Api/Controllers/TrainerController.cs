using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lift.Buddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController: ControllerBase
    {
        private readonly ITrainerService _trainersService;

        public TrainerController (ITrainerService trainersService) 
        {
            _trainersService = trainersService;
        }

        [HttpGet("athletes")]
        public async Task<IActionResult> Get()
        {
            var trainerGuidString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (trainerGuidString == null)
            {
                return NotFound();
            }

            var response = await _trainersService.GetAthletes(new Guid(trainerGuidString));
            return Ok(response);
        }

        [HttpDelete("athletes")]
        public async Task<IActionResult> RemoveSubscriber([FromBody] User user)
        {
            var trainerGuidString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (trainerGuidString == null)
            {
                return NotFound();
            }

            await _trainersService.RemoveFollower(new Guid(trainerGuidString), user);
            return NoContent();
        }
    }
}
