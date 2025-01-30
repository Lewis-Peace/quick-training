namespace Lift.Buddy.Core.Database.Entities;

public class Exercise
{
    public Guid ExerciseId { get; set; }
    public string Name { get; set; }
    public int? Repetitions { get; set; }
    public int? Series { get; set; }
    public TimeSpan? Time { get; set; }
    public int? Rest { get; set; }

    public virtual WorkoutDay WorkoutDay { get; set; }
}
