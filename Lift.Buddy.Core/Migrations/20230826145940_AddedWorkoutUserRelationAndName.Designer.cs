﻿// <auto-generated />
using Lift.Buddy.Core.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lift.Buddy.Core.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20230826145940_AddedWorkoutUserRelationAndName")]
    partial class AddedWorkoutUserRelationAndName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("Lift.Buddy.Core.DB.Models.User", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Answers")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsTrainer")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Questions")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("UserName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Lift.Buddy.Core.DB.Models.WorkoutAssignment", b =>
                {
                    b.Property<string>("WorkoutUser")
                        .HasColumnType("TEXT");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("INTEGER");

                    b.HasKey("WorkoutUser");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutAssignments");
                });

            modelBuilder.Entity("Lift.Buddy.Core.DB.Models.WorkoutSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("WorkoutDays")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "workoutDays");

                    b.HasKey("Id");

                    b.ToTable("WorkoutSchedules");
                });

            modelBuilder.Entity("Lift.Buddy.Core.DB.Models.WorkoutAssignment", b =>
                {
                    b.HasOne("Lift.Buddy.Core.DB.Models.WorkoutSchedule", "WorkoutSchedule")
                        .WithMany("WorkoutAssignments")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lift.Buddy.Core.DB.Models.User", "User")
                        .WithMany("WorkoutAssignments")
                        .HasForeignKey("WorkoutUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WorkoutSchedule");
                });

            modelBuilder.Entity("Lift.Buddy.Core.DB.Models.User", b =>
                {
                    b.Navigation("WorkoutAssignments");
                });

            modelBuilder.Entity("Lift.Buddy.Core.DB.Models.WorkoutSchedule", b =>
                {
                    b.Navigation("WorkoutAssignments");
                });
#pragma warning restore 612, 618
        }
    }
}
