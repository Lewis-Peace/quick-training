using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.DB.Models;
using Lift.Buddy.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lift.Buddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutPlanService _workoutScheduleService;

        public WorkoutPlanController(IWorkoutPlanService workoutScheduleService)
        {
            _workoutScheduleService = workoutScheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? String.Empty;
            Response<WorkoutPlan> response;
            if (String.IsNullOrEmpty(username))
            {
                response = await _workoutScheduleService.GetWorkoutPlan(-1);
            }
            else
            {
                response = await _workoutScheduleService.GetWorkoutPlanAssignedToUsername(username);
            }
            return Ok(response);
        }

        [HttpGet("CreatedBy/{username}")]
        public async Task<IActionResult> GetWorkoutsCreatedBy(string username)
        {
            var response = await _workoutScheduleService.GetWorkoutPlanCreatedByUsername(username);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _workoutScheduleService.GetWorkoutPlan(id);
            return Ok(response);
        }

        [HttpGet("pdf/{id}")]
        public async Task<IActionResult> GetWorkplanPdf(int id)
        {
            var response = await _workoutScheduleService.GetWorkplanPdf(id);
            return Ok(response);
        }


        [HttpGet("subscribers/{id}")]
        public async Task<IActionResult> GetWorkoutPlanSubscribers(int id)
        {
            var response = await _workoutScheduleService.GetWorkoutPlanSubscribersNumber(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WorkoutPlan workoutSchedule)
        {
            var response = await _workoutScheduleService.AddWorkoutPlan(workoutSchedule);
            if (!response.result)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpPut("review")]
        public async Task<IActionResult> ReviewWorkouPlan([FromBody] WorkoutPlan workoutPlan)
        {
            var response = await _workoutScheduleService.ReviewWorkoutPlan(workoutPlan);
            if (!response.result)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] WorkoutPlan workoutSchedule)
        {
            var response = await _workoutScheduleService.UpdateWorkoutPlan(workoutSchedule);
            if (!response.result)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] WorkoutPlan workoutSchedule)
        {
            var response = await _workoutScheduleService.DeleteWorkoutPlan(workoutSchedule);
            if (!response.result)
            {
                return Ok(response);
            }
            return NoContent();
        }
    }
}
