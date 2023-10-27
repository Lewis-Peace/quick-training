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
        private readonly IWorkoutPlanService _workoutPlanService;

        public UserController(IUserService usersService, IWorkoutPlanService workoutPlanService)
        {
            _usersService = usersService;
            _workoutPlanService = workoutPlanService;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUsername([FromRoute] string username)
        {
            var response = await _usersService.GetUsersByUsername(username);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserData()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return StatusCode(500);

            var response = await _usersService.GetUserData(Guid.Parse(userId));
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserData([FromBody] UserDTO userData)
        {
            await _usersService.UpdateUserData(userData);
            return NoContent();
        }

        [HttpGet("workouts")]
        public async Task<IActionResult> GetWorkoutsAssignedToUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return StatusCode(500);

            var response = await _workoutPlanService.GetAssignedWorkoutPlans(Guid.Parse(userId));
            return Ok(response);
        }

        #region Subscription

        [HttpGet("subscription")]
        public IActionResult GetSubscriptions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return StatusCode(500);

            var response = _usersService.GetSubscriptionsRelatedToUser(Guid.Parse(userId));
            return Ok(response);
        }

        [HttpPost("subscription")]
        public async Task<IActionResult> SubscribeToTrainer([FromBody] SubscriptionDTO subscription)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return StatusCode(500);

            await _usersService.SubscribeToTrainer(Guid.Parse(userId), subscription);
            return NoContent();
        }

        [HttpDelete("subscription")]
        public async Task<IActionResult> UnsubscribeToTrainer([FromBody] SubscriptionDTO subscription)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return StatusCode(500);

            await _usersService.UnsubscribeToTrainer(Guid.Parse(userId), subscription);
            return NoContent();
        }
        #endregion
    }
}
