using Lift.Buddy.API.Interfaces;
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
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService) 
        {
            _settingsService = settingsService;
        }

        [HttpGet]
        public IActionResult GetSettings()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                return NotFound();

            var settings = _settingsService.GetSettings(new Guid(userId));
            return Ok(settings);
        }

        [HttpGet("labels-languages")]
        public IActionResult GetLabelsLanguages()
        {
            return Ok();
        }

        [HttpGet("labels-unit-of-measure")]
        public IActionResult GetLabelsUnitOfMeasure()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateSettings([FromBody] SettingsDTO settings)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                return NotFound();

            _settingsService.UpdateSettings(new Guid(userId), settings);

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteSettings()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                return NotFound();

            _settingsService.DeleteSettings(new Guid(userId));

            return NoContent();
        }
    }
}
