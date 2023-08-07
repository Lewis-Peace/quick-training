using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface ILoginService
    {
        bool CheckCredentials(LoginCredentials credentials);
    }
}
