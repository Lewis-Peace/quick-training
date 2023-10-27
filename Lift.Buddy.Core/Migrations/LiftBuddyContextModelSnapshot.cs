﻿// <auto-generated />
using System;
using Lift.Buddy.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.Exercise", b =>
                {
                    b.Property<Guid>("ExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int?>("Repetitions")
                        .HasColumnType("int");

                    b.Property<int?>("Rest")
                        .HasColumnType("int");

                    b.Property<int?>("Series")
                        .HasColumnType("int");

                    b.Property<int?>("Time")
                        .HasColumnType("int");

                    b.Property<Guid?>("WorkoutDayId")
                        .HasColumnType("char(36)");

                    b.HasKey("ExerciseId");

                    b.HasIndex("WorkoutDayId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.Frontpage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Frontpages");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.PersonalRecord", b =>
                {
                    b.Property<Guid>("PersonalRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ExerciseName")
                        .HasColumnType("longtext");

                    b.Property<int>("ExerciseType")
                        .HasColumnType("int");

                    b.Property<int>("Repetitions")
                        .HasColumnType("int");

                    b.Property<int>("Series")
                        .HasColumnType("int");

                    b.Property<int?>("UOM")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<double?>("Weight")
                        .HasColumnType("double");

                    b.HasKey("PersonalRecordId");

                    b.HasIndex("UserId");

                    b.ToTable("PersonalRecords");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.SecurityQuestion", b =>
                {
                    b.Property<Guid>("SecurityQuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Answer")
                        .HasColumnType("longtext");

                    b.Property<string>("Question")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("SecurityQuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("SecurityQuestions");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.Settings", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<int>("UnitOfMeasure")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.Subscription", b =>
                {
                    b.Property<Guid>("TrainerId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AthleteId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Expiration")
                        .HasColumnType("longtext");

                    b.Property<int>("SubscriptionType")
                        .HasColumnType("int");

                    b.HasKey("TrainerId", "AthleteId");

                    b.HasIndex("AthleteId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsTrainer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Private")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.WorkoutDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<Guid?>("WorkoutPlanId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutPlanId");

                    b.ToTable("WorkoutDays");
                });

            modelBuilder.Entity("Lift.Buddy.Core.Database.Entities.WorkoutPlan", b =>
                {
                    b.Property<Guid>("WorkoutPlanId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<float>("ReviewAverage")
                        .HasColumnType("float");

                    b.Property<int>("ReviewCount")
                        .HasColumnType("int");

                    b.HasKey("WorkoutPlanId");

                    b.HasIndex("CreatorId");

                    b.ToTable("WorkoutPlans");
                });

            modelBuilder.Entity("UserWorkoutPlan", b =>
                {
                    b.Property<Guid>("AssignedPlansWorkoutPlanId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UsersUserId")
                        .HasColumnType("char(36)");

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
                    b.Navigation("WorkoutDays");
                });
#pragma warning restore 612, 618
        }
    }
}
