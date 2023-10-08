using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface ISearchService
    {
        public Task<Response<UserDTO>> GetUsersByUsername(string username);
    }
}
