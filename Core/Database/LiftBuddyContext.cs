using Lift.Buddy.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lift.Buddy.Core.Database;

public class LiftBuddyContext : DbContext
{
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<PersonalRecord> PersonalRecords { get; set; }
    public DbSet<SecurityQuestion> SecurityQuestions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<WorkoutDay> WorkoutDays { get; set; }
    public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<Frontpage> Frontpages { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public LiftBuddyContext(DbContextOptions<LiftBuddyContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);
            entity.Property(u => u.UserId).ValueGeneratedNever();
            entity.Property(u => u.Username).IsRequired().HasMaxLength(32);
            entity.Property(u => u.Password).IsRequired();
            entity.Property(u => u.Email).IsRequired();
            entity.Property(u => u.Private).IsRequired();
            entity.Property(u => u.IsTrainer).IsRequired().HasDefaultValue(false);
            entity.Property(u => u.IsAdmin).IsRequired().HasDefaultValue(false);

            entity.HasMany(u => u.SecurityQuestions)
                .WithOne(s => s.User)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(u => u.PersonalRecords)
                .WithOne(pr => pr.User)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(u => u.AssignedPlans)
                .WithMany(p => p.Users);

            entity.HasMany(u => u.SubscribedAthletes)
                .WithOne(u => u.Athlete);
            entity.HasMany(u => u.Trainers)
                .WithOne(u => u.Trainer);
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(u => new { u.TrainerId, u.AthleteId });
            entity.Property(u => u.SubscriptionType);
            entity.Property(u => u.Expiration)
                .HasConversion(x => x.ToString(), x => DateTime.Parse(x));
        });

        modelBuilder.Entity<WorkoutPlan>(entity =>
        {
            entity.HasKey(p => p.WorkoutPlanId);

            entity.Property(p => p.ReviewAverage);

            entity.HasOne(p => p.Creator)
                .WithMany(p => p.CreatedPlans)
                .HasForeignKey(p => p.CreatorId);

            entity.HasMany(p => p.WorkoutDays)
                .WithOne(p => p.WorkoutPlan)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(p => new { p.WorkoutPlanId, p.UserId });
            entity.Property(x => x.Value);

            entity.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);

            entity.HasOne(p => p.WorkoutPlan)
                .WithMany(p => p.Reviews)
                .HasForeignKey(p => p.WorkoutPlanId);
        });

        modelBuilder.Entity<WorkoutDay>(entity =>
        {
            entity.HasKey(d => d.Id);

            entity.HasMany(d => d.Exercises)
                .WithOne(e => e.WorkoutDay)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.WorkoutPlan)
                .WithMany(p => p.WorkoutDays);
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId);

        });

        modelBuilder.Entity<PersonalRecord>(entity =>
        {
            entity.HasKey(r => r.PersonalRecordId);
        });

        modelBuilder.Entity<SecurityQuestion>(entity =>
        {
            entity.HasKey(sq => sq.SecurityQuestionId);
        });

        modelBuilder.Entity<Settings>(b =>
        {
            b.HasKey(x => x.UserId);

            b.Property(x => x.UnitOfMeasure)
                .IsRequired();

            b.HasOne(x => x.User)
                .WithOne(x => x.Settings)
                .HasForeignKey<Settings>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

        modelBuilder.Entity<Frontpage>(b =>
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Description)
                .IsRequired();

            b.HasOne(x => x.User)
                .WithOne(x => x.Frontpage)
                .HasForeignKey<Frontpage>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}

