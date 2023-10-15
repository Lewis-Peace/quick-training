using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.Models;
using Lift.Buddy.Core.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lift.Buddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
            var response = new Response<string>();
            var enums = ((Languages[]) Enum.GetValues(typeof(Languages))).Select(x => x.ToString()).Skip(1);
            response.Body = enums;
            response.Result = true;
            return Ok(response);
        }

        [HttpGet("labels-unit-of-measures")]
        public IActionResult GetLabelsUnitOfMeasure()
        {
            var response = new Response<string>();
            var enums = ((UnitOfMeasure[])Enum.GetValues(typeof(UnitOfMeasure))).Select(x => x.ToString()).Skip(1);
            response.Body = enums;
            response.Result = true;
            return Ok(response);
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
