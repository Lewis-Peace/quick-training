using Lift.Buddy.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lift.Buddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {

            var response = await _searchService.GetUsersByUsername(username);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                return Ok(response);

            response = await _searchService.GetExtraDataOfUsers(response.Body, Guid.Parse(userId));

            return Ok(response);
        }
    }
}
