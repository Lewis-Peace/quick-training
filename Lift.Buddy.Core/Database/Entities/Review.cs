namespace Lift.Buddy.Core.Database.Entities
{
    public class Review
    {
        public Guid UserId { get; set; }
        public Guid WorkoutPlanId { get; set; }
        public int Value { get; set; }

        public virtual User User { get; set; }
        public virtual WorkoutPlan WorkoutPlan { get; set; }
    }
}
