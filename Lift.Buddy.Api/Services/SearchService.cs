using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.Database;
using Lift.Buddy.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Lift.Buddy.API.Services
{
    public class SearchService : ISearchService
    {
        private readonly LiftBuddyContext _context;
        private readonly IDatabaseMapper _mapper;
        private readonly IUserService _userService;

        public SearchService(LiftBuddyContext context, IDatabaseMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Response<UserDTO>> GetUsersByUsername(string username)
        {
            return await _userService.GetUsersByUsername(username, 100);
        }
    }
}
