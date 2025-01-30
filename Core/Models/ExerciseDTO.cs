namespace Lift.Buddy.Core.Models;

public class ExerciseDTO
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Repetitions { get; set; }
    public int? Series { get; set; }
    public TimeSpan? Time { get; set; }
    public int? Rest { get; set; }

    public override string ToString() => $"{Name}: {Repetitions}x{Series}";
}
