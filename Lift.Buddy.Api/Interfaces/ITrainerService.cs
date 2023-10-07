using Lift.Buddy.Core.Database.Entities;
using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface ITrainerService
    {
        public Task<Response<UserDTO>> GetAthletes(Guid trainerGuid);

        public Task<Response<WorkoutPlan>> RemoveFollower(Guid trainerGuid, User athlete);
    }
}
