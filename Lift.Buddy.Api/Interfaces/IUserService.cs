using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface IUserService
    {
        Task<Response<UserDTO>> GetUserData(Guid userId);
        Task<Response<UserDTO>> UpdateUserData(UserDTO userData);
        Task<Response<UserDTO>> GetUsersByUsername(string username, int amount = 10);
    }
}
