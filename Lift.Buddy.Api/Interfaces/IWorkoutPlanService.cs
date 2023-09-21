using Lift.Buddy.Core.DB.Models;
using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface IWorkoutPlanService
    {
        Task<Response<WorkoutPlan>> GetWorkoutPlan(int id);
        Task<Response<WorkoutPlan>> GetWorkoutPlanCreatedByUsername(string username);
        Task<Response<WorkoutPlan>> GetWorkoutPlanAssignedToUsername(string username);
        Task<Response<int>> GetWorkoutPlanSubscribersNumber(int workoutPlanId);
        Task<Response<WorkoutPlan>> AddWorkoutPlan(WorkoutPlan schedule);
        Task<Response<WorkoutPlan>> DeleteWorkoutPlan(WorkoutPlan schedule);
        Task<Response<WorkoutPlan>> UpdateWorkoutPlan(WorkoutPlan schedule);
    }
}
