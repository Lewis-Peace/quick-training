using Lift.Buddy.Core.Models;
using MigraDoc.DocumentObjectModel;

namespace Lift.Buddy.API.Interfaces
{
    public interface IWorkoutPlanService
    {
        Task<Response<WorkoutPlanDTO>> GetAssignedWorkoutPlans(Guid userId);
        Task<Response<WorkoutPlanDTO>> GetWorkoutPlanById(Guid id);
        Task<Response<WorkoutPlanDTO>> GetWorkoutPlanCreatedByUser(Guid userId);
        Task<Response<WorkoutPlanDTO>> GetUserWorkoutPlans(Guid userId);
        Task<Response<UserDTO>> GetWorkoutPlanSubscribers(Guid workoutPlanId);
        Task<Response<Document>> GetWorkoutPlanPdf(Guid workoutPlanId);
        Task<Response<UserDTO>> SetWorkoutPlanAssignment(Guid workoutPlanId, UserDTO[] userDTO);
        Task<Response<WorkoutPlanDTO>> AddWorkoutPlan(WorkoutPlanDTO workoutPlan);
        Task<Response<WorkoutPlanDTO>> UpdateWorkoutPlan(WorkoutPlanDTO workoutPlan);
        Task<Response<WorkoutPlanDTO>> DeleteWorkoutPlan(Guid workoutPlan);
        Task<Response<WorkoutPlanDTO>> ReviewWorkoutPlan(Guid user, ReviewDTO reviewDTO);
    }
}
