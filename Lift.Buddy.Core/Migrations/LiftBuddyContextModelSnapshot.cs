﻿// <auto-generated />
using System;
using Lift.Buddy.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lift.Buddy.Core.Migrations
{
    [DbContext(typeof(LiftBuddyContext))]
    partial class LiftBuddyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.Exercise", b =>
                {
                    b.Property<Guid>("ExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("Repetitions")
                        .HasColumnType("integer");

                    b.Property<int?>("Rest")
                        .HasColumnType("integer");

                    b.Property<int?>("Series")
                        .HasColumnType("integer");

                    b.Property<TimeSpan?>("Time")
                        .HasColumnType("interval");

                    b.Property<Guid?>("WorkoutDayId")
                        .HasColumnType("uuid");

                    b.HasKey("ExerciseId");

                    b.HasIndex("WorkoutDayId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.Frontpage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("Id");

                    b.ToTable("Frontpages");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.PersonalRecord", b =>
                {
                    b.Property<Guid>("PersonalRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ExerciseName")
                        .HasColumnType("text");

                    b.Property<int>("ExerciseType")
                        .HasColumnType("integer");

                    b.Property<int>("Repetitions")
                        .HasColumnType("integer");

                    b.Property<int>("Series")
                        .HasColumnType("integer");

                    b.Property<int?>("UOM")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<double?>("Weight")
                        .HasColumnType("double precision");

                    b.HasKey("PersonalRecordId");

                    b.HasIndex("UserId");

                    b.ToTable("PersonalRecords");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.Review", b =>
                {
                    b.Property<Guid>("WorkoutPlanId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("WorkoutPlanId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.SecurityQuestion", b =>
                {
                    b.Property<Guid>("SecurityQuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Answer")
                        .HasColumnType("text");

                    b.Property<string>("Question")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("SecurityQuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("SecurityQuestions");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.Settings", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("UnitOfMeasure")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.Subscription", b =>
                {
                    b.Property<Guid>("TrainerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AthleteId")
                        .HasColumnType("uuid");

                    b.Property<string>("Expiration")
                        .HasColumnType("text");

                    b.Property<int>("SubscriptionType")
                        .HasColumnType("integer");

                    b.HasKey("TrainerId", "AthleteId");

                    b.HasIndex("AthleteId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsTrainer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Private")
                        .HasColumnType("boolean");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.WorkoutDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Day")
                        .HasColumnType("integer");

                    b.Property<Guid?>("WorkoutPlanId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutPlanId");

                    b.ToTable("WorkoutDays");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.WorkoutPlan", b =>
                {
                    b.Property<Guid>("WorkoutPlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<double>("ReviewAverage")
                        .HasColumnType("double precision");

                    b.HasKey("WorkoutPlanId");

                    b.HasIndex("CreatorId");

                    b.ToTable("WorkoutPlans");
                });

            modelBuilder.Entity("UserWorkoutPlan", b =>
                {
                    b.Property<Guid>("AssignedPlansWorkoutPlanId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersUserId")
                        .HasColumnType("uuid");

                    b.HasKey("AssignedPlansWorkoutPlanId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("UserWorkoutPlan");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.Exercise", b =>
                {
                    b.HasOne("Lift.Buddy.Core.Database.Entities.WorkoutDay", "WorkoutDay")
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutDayId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("WorkoutDay");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.Frontpage", b =>
                {
                    b.HasOne("Lift.Buddy.Core.Database.Entities.User", "User")
                        .WithOne("Frontpage")
                        .HasForeignKey("Lift.Buddy.Core.Database.Entities.Frontpage", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.PersonalRecord", b =>
                {
                    b.HasOne("Lift.Buddy.Core.Database.Entities.User", "User")
                        .WithMany("PersonalRecords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.Review", b =>
                {
                    b.HasOne("Lift.Buddy.Core.Database.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lift.Buddy.Core.Database.Entities.WorkoutPlan", "WorkoutPlan")
                        .WithMany("Reviews")
                        .HasForeignKey("WorkoutPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WorkoutPlan");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.SecurityQuestion", b =>
                {
                    b.HasOne("Lift.Buddy.Core.Database.Entities.User", "User")
                        .WithMany("SecurityQuestions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.Settings", b =>
                {
                    b.HasOne("Lift.Buddy.Core.Database.Entities.User", "User")
                        .WithOne("Settings")
                        .HasForeignKey("Lift.Buddy.Core.Database.Entities.Settings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.Subscription", b =>
                {
                    b.HasOne("Lift.Buddy.Core.Database.Entities.User", "Athlete")
                        .WithMany("SubscribedAthletes")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lift.Buddy.Core.Database.Entities.User", "Trainer")
                        .WithMany("Trainers")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.WorkoutDay", b =>
                {
                    b.HasOne("Lift.Buddy.Core.Database.Entities.WorkoutPlan", "WorkoutPlan")
                        .WithMany("WorkoutDays")
                        .HasForeignKey("WorkoutPlanId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("WorkoutPlan");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.WorkoutPlan", b =>
                {
                    b.HasOne("Lift.Buddy.Core.Database.Entities.User", "Creator")
                        .WithMany("CreatedPlans")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("UserWorkoutPlan", b =>
                {
                    b.HasOne("Lift.Buddy.Core.Database.Entities.WorkoutPlan", null)
                        .WithMany()
                        .HasForeignKey("AssignedPlansWorkoutPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lift.Buddy.Core.Database.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.User", b =>
                {
                    b.Navigation("CreatedPlans");

                    b.Navigation("Frontpage");

                    b.Navigation("PersonalRecords");

                    b.Navigation("SecurityQuestions");

                    b.Navigation("Settings");

                    b.Navigation("SubscribedAthletes");

                    b.Navigation("Trainers");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.WorkoutDay", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.WorkoutPlan", b =>
                {
                    b.Navigation("Reviews");

                    b.Navigation("WorkoutDays");
                });
#pragma warning restore 612, 618
        }
    }
}
