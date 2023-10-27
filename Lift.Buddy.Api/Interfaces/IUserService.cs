using Lift.Buddy.Core.Database.Entities;
using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface IUserService
    {
        Task<Response<UserDTO>> GetUserData(Guid userId);
        Task<Response<UserDTO>> UpdateUserData(UserDTO userData);
        Task<Response<UserDTO>> GetUsersByUsername(string username, int amount = 10);
        Response<SubscriptionDTO> GetSubscriptionsRelatedToUser(Guid user);
        Task<Response<UserDTO>> SubscribeToTrainer(Guid user, SubscriptionDTO userDTO);
        Task<Response<UserDTO>> UnsubscribeToTrainer(Guid user, SubscriptionDTO userDTO);
    }
}
