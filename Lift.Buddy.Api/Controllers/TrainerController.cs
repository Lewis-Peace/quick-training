﻿using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.Database.Entities;
using Lift.Buddy.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lift.Buddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
                return StatusCode(500);
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
                return StatusCode(500);
            }

            await _trainersService.RemoveFollower(Guid.Parse(trainerGuidString), user);
            return NoContent();
        }
    }
}
