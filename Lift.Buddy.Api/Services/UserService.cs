using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.Models;
using Lift.Buddy.Core;
using Microsoft.EntityFrameworkCore;
using Lift.Buddy.Core.Database;

namespace Lift.Buddy.API.Services
{
    public class UserService: IUserService
    {
        private readonly LiftBuddyContext _context;
        private readonly IDatabaseMapper _mapper;

        public UserService(LiftBuddyContext context, IDatabaseMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<UserDTO>> GetUserData(Guid userId)
        {
            var response = new Response<UserDTO>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

                if (user == null) throw new KeyNotFoundException($"User '{userId}' doesn't exist");

                var userData = _mapper.Map(user);

                response.Body = new UserDTO[] { userData };
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetUserData), ex);
            }

            return response;
        }

        public async Task<Response<UserDTO>> GetUsersByUsername(string username)
        {
            var response = new Response<UserDTO>();

            try
            {
                var users = await _context.Users
                    .Where(x => x.Username.Contains(username))
                    .Select(x => _mapper.Map(x))
                    .ToArrayAsync();

                response.Result = true;
                response.Body = users.Take(10);
            }
            catch (Exception ex) 
            { 
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetUsersByUsername), ex);
            }

            return response;
        }

        public async Task<Response<UserDTO>> UpdateUserData(UserDTO userData)
        {
            var response = new Response<UserDTO>();

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Username == userData.Credentials.Username);

                if (user == null) throw new KeyNotFoundException($"User '{userData.Credentials.Username}' doesn't exist");

                // TODO check che i campi non diventino vuoti
                user.Username = userData.Userame;
                user.Surname = userData.Surname;
                user.Name = userData.Name;
                user.Email = userData.Email;

                _context.Users.Update(user);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception($"Failed to update database.");
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(UpdateUserData), ex);
            }

            return response;
        }
    }
}
