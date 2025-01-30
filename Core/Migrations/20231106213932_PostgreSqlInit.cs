using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lift.Buddy.Core.Migrations
{
    /// <inheritdoc />
    public partial class PostgreSqlInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Private = table.Column<bool>(type: "boolean", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsTrainer = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Frontpages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frontpages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Frontpages_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalRecords",
                columns: table => new
                {
                    PersonalRecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseName = table.Column<string>(type: "text", nullable: true),
                    Series = table.Column<int>(type: "integer", nullable: false),
                    Repetitions = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: true),
                    UOM = table.Column<int>(type: "integer", nullable: true),
                    ExerciseType = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalRecords", x => x.PersonalRecordId);
                    table.ForeignKey(
                        name: "FK_PersonalRecords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityQuestions",
                columns: table => new
                {
                    SecurityQuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: true),
                    Answer = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityQuestions", x => x.SecurityQuestionId);
                    table.ForeignKey(
                        name: "FK_SecurityQuestions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnitOfMeasure = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Settings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    AthleteId = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriptionType = table.Column<int>(type: "integer", nullable: false),
                    Expiration = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => new { x.TrainerId, x.AthleteId });
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlans",
                columns: table => new
                {
                    WorkoutPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ReviewAverage = table.Column<double>(type: "double precision", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlans", x => x.WorkoutPlanId);
                    table.ForeignKey(
                        name: "FK_WorkoutPlans_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkoutPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => new { x.WorkoutPlanId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_WorkoutPlans_WorkoutPlanId",
                        column: x => x.WorkoutPlanId,
                        principalTable: "WorkoutPlans",
                        principalColumn: "WorkoutPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWorkoutPlan",
                columns: table => new
                {
                    AssignedPlansWorkoutPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkoutPlan", x => new { x.AssignedPlansWorkoutPlanId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_UserWorkoutPlan_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWorkoutPlan_WorkoutPlans_AssignedPlansWorkoutPlanId",
                        column: x => x.AssignedPlansWorkoutPlanId,
                        principalTable: "WorkoutPlans",
                        principalColumn: "WorkoutPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    WorkoutPlanId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutDays_WorkoutPlans_WorkoutPlanId",
                        column: x => x.WorkoutPlanId,
                        principalTable: "WorkoutPlans",
                        principalColumn: "WorkoutPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Repetitions = table.Column<int>(type: "integer", nullable: true),
                    Series = table.Column<int>(type: "integer", nullable: true),
                    Time = table.Column<TimeSpan>(type: "interval", nullable: true),
                    Rest = table.Column<int>(type: "integer", nullable: true),
                    WorkoutDayId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.ExerciseId);
                    table.ForeignKey(
                        name: "FK_Exercises_WorkoutDays_WorkoutDayId",
                        column: x => x.WorkoutDayId,
                        principalTable: "WorkoutDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutDayId",
                table: "Exercises",
                column: "WorkoutDayId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRecords_UserId",
                table: "PersonalRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityQuestions_UserId",
                table: "SecurityQuestions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_AthleteId",
                table: "Subscriptions",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkoutPlan_UsersUserId",
                table: "UserWorkoutPlan",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutDays_WorkoutPlanId",
                table: "WorkoutDays",
                column: "WorkoutPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_CreatorId",
                table: "WorkoutPlans",
                column: "CreatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Frontpages");

            migrationBuilder.DropTable(
                name: "PersonalRecords");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SecurityQuestions");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "UserWorkoutPlan");

            migrationBuilder.DropTable(
                name: "WorkoutDays");

            migrationBuilder.DropTable(
                name: "WorkoutPlans");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
