using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Services
{
    public class LoginService : ILoginService
    {
        public bool CheckCredentials(LoginCredentials credentials)
        {
            var username = credentials.Username;
            var password = credentials.Password;
            if (username == "admin" && password == "lb")
            {
                return true;
            }
            return false;
        }
    }
}
