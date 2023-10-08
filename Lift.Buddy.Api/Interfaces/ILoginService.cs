using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface ILoginService
    {
        Task<Response<SecurityQuestionDTO>> GetSecurityQuestions(string username);
        Task<(Guid UserId, bool IsValid, bool? isTrainer)> CheckCredentials(Credentials credentials);
        Task<Response<UserDTO>> RegisterUser(UserDTO registerCredentials);
        Task<Response<Credentials>> ChangePassword(Credentials loginCredentials);
    }
}
