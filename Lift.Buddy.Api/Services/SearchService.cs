using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.Database;
using Lift.Buddy.Core.Database.Entities;
using Lift.Buddy.Core.Models;
using Lift.Buddy.Core.Models.Enums;
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

        public async Task<Response<UserDTO>> GetExtraDataOfUsers(IEnumerable<UserDTO> users, Guid userId)
        {
            var response = new Response<UserDTO>();

            try
            {

                var u = await _context.Users.Include(x => x.Trainers).FirstOrDefaultAsync(x => x.UserId == userId);

                if (u == null)
                {
                    throw new Exception("Failed to find user.");
                }

                foreach (var user in users)
                {
                    var isSubscribed = u.Trainers.FirstOrDefault(x => x.UserId == user.UserId) != null;
                    if (isSubscribed)
                    {
                        user.SubscriptionState = SubscriptionState.Subscribed;
                    } else
                    {
                        user.SubscriptionState = SubscriptionState.Unsubscribed;
                    }
                    var frontpage = await _context.Frontpages.FirstOrDefaultAsync(x => x.Id == user.UserId);
                    if (frontpage != null)
                    {
                        user.Description = frontpage.Description;
                    }
                }

                response.Result = true;
                response.Body = users;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetUsersByUsername), ex);
            }

            return response;
        }

        public async Task<Response<UserDTO>> GetUsersByUsername(string username)
        {
            return await _userService.GetUsersByUsername(username, 100);
        }
    }
}
