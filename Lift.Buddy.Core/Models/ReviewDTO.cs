namespace Lift.Buddy.Core.Models
{
    public class ReviewDTO
    {
        public Guid UserId { get; set; }
        public Guid WorkoutPlanId { get; set; }
        public int Value { get; set; }
    }
}
