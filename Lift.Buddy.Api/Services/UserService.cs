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

        public async Task<Response<UserDTO>> GetUsersByUsername(string username, int amount = 10)
        {
            var response = new Response<UserDTO>();

            try
            {
                var users = await _context.Users
                    .Where(x => x.Username.Contains(username))
                    .Select(x => _mapper.Map(x))
                    .ToArrayAsync();

                response.Result = true;
                response.Body = users.Take(amount);
            }
            catch (Exception ex) 
            { 
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetUsersByUsername), ex);
            }

            return response;
        }

        public async Task<Response<UserDTO>> SubscribeToTrainer(Guid user, UserDTO userDTO)
        {
            var response = new Response<UserDTO>();
            try
            {
                var athlete = await _context.Users.Include(x => x.Trainers).FirstOrDefaultAsync(x => x.UserId == user);
                if (athlete == null)
                {
                    throw new Exception("Athlete not found.");
                }
                var trainer = await _context.Users.Include(x => x.SubscribedAthletes).FirstOrDefaultAsync(x => x.UserId == userDTO.UserId);
                if (trainer == null)
                {
                    throw new Exception("Trainer not found.");
                }
                athlete.Trainers.Add(trainer);
                trainer.SubscribedAthletes.Add(athlete);

                if (await _context.SaveChangesAsync() < 1)
                {
                    throw new Exception("Failed to save into database.");
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(SubscribeToTrainer), ex);
            }
            return response;
        }

        public async Task<Response<UserDTO>> UnsubscribeToTrainer(Guid user, UserDTO userDTO)
        {
            var response = new Response<UserDTO>();
            try
            {
                var athlete = await _context.Users.Include(x => x.Trainers).FirstOrDefaultAsync(x => x.UserId == user);
                if (athlete == null)
                {
                    throw new Exception("Athlete not found.");
                }
                var trainer = await _context.Users.Include(x => x.SubscribedAthletes).FirstOrDefaultAsync(x => x.UserId == userDTO.UserId);
                if (trainer == null)
                {
                    throw new Exception("Trainer not found.");
                }
                athlete.Trainers.Remove(trainer);
                trainer.SubscribedAthletes.Remove(athlete);

                if (await _context.SaveChangesAsync() < 1)
                {
                    throw new Exception("Failed to save into database.");
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(SubscribeToTrainer), ex);
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
