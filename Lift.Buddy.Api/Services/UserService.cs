using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.Models;
using Lift.Buddy.Core;
using Microsoft.EntityFrameworkCore;
using Lift.Buddy.Core.Database;
using Lift.Buddy.Core.Database.Entities;

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

        #region Subscriptions
        public Response<SubscriptionDTO> GetSubscriptionsRelatedToUser(Guid user)
        {
            var response = new Response<SubscriptionDTO>();

            try
            {
                var subscriptions = _context.Subscriptions.Where(x => x.TrainerId == user);

                response.Body = subscriptions.Select(x => _mapper.Map(x));
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetSubscriptionsRelatedToUser), ex);
            }

            return response;
        }

        public async Task<Response<UserDTO>> SubscribeToTrainer(Guid user, SubscriptionDTO subscriptionDTO)
        {
            var response = new Response<UserDTO>();
            try
            {
                var subscription = _mapper.Map(subscriptionDTO);
                subscription.AthleteId = user;

                _context.Subscriptions.Add(subscription);

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

        public async Task<Response<UserDTO>> UnsubscribeToTrainer(Guid user, SubscriptionDTO subscriptionDTO)
        {
            var response = new Response<UserDTO>();
            try
            {
                var subscription = await _context.Subscriptions
                    .FirstOrDefaultAsync(x => x.TrainerId == subscriptionDTO.TrainerId && x.AthleteId == user);
                if (subscription == null)
                {
                    throw new Exception("Subscription not found.");
                }

                _context.Subscriptions.Remove(subscription);

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

        #endregion

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
