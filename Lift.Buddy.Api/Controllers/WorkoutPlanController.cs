﻿using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Pdf.Content.Objects;
using System.Security.Claims;

namespace Lift.Buddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                return StatusCode(500);

            var res = await _workoutScheduleService.GetUserWorkoutPlans(Guid.Parse(userId));
            return Ok(res);
        }

        [HttpGet("created-by/{userId}")]
        public async Task<IActionResult> GetWorkoutsCreatedBy(Guid userId)
        {
            var response = await _workoutScheduleService.GetWorkoutPlanCreatedByUser(userId);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _workoutScheduleService.GetWorkoutPlanById(id);
            return Ok(response);
        }

        [HttpGet("pdf/{id}")]
        public async Task<IActionResult> GetWorkplanPdf(Guid id)
        {
            var response = await _workoutScheduleService.GetWorkoutPlanPdf(id);
            return Ok(response);
        }


        [HttpGet("subscribers/{id}")]
        public async Task<IActionResult> GetWorkoutPlanSubscribers(Guid id)
        {
            var response = await _workoutScheduleService.GetWorkoutPlanSubscribers(id);
            return Ok(response);
        }

        [HttpPost("subscribers/{id}")]
        public async Task<IActionResult> SetAssignments([FromRoute] Guid id, [FromBody] UserDTO[] assignments)
        {
            var response = await _workoutScheduleService.SetWorkoutPlanAssignment(id, assignments);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WorkoutPlanDTO workoutSchedule)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                return StatusCode(500);

            workoutSchedule.CreatorId = Guid.Parse(userId);

            var response = await _workoutScheduleService.AddWorkoutPlan(workoutSchedule);
            if (!response.Result)
            {
                return Ok(response);
            }

            return NoContent();
        }

        [HttpPut("review")]
        public async Task<IActionResult> ReviewWorkouPlan([FromBody] WorkoutPlanDTO workoutPlan)
        {
            var response = await _workoutScheduleService.ReviewWorkoutPlan(workoutPlan);
            if (!response.Result)
            {
                return Ok(response);
            }

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] WorkoutPlanDTO workoutSchedule)
        {
            var response = await _workoutScheduleService.UpdateWorkoutPlan(workoutSchedule);
            if (!response.Result)
            {
                return Ok(response);
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] WorkoutIdModel workoutId)
        {
            var response = await _workoutScheduleService.DeleteWorkoutPlan(workoutId.WorkoutId);
            if (!response.Result)
            {
                return Ok(response);
            }

            return NoContent();
        }

        public class WorkoutIdModel
        {
            public Guid WorkoutId { get; set; }
        };
    }
}
