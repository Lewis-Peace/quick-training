using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lift.Buddy.Core.Migrations
{
    /// <inheritdoc />
    public partial class MySQLDBChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Username = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Surname = table.Column<string>(type: "longtext", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false),
                    IsAdmin = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    IsTrainer = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PersonalRecords",
                columns: table => new
                {
                    PersonalRecordId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ExerciseName = table.Column<string>(type: "longtext", nullable: true),
                    Series = table.Column<int>(type: "int", nullable: false),
                    Repetitions = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "double", nullable: true),
                    UOM = table.Column<int>(type: "int", nullable: true),
                    ExerciseType = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SecurityQuestions",
                columns: table => new
                {
                    SecurityQuestionId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Question = table.Column<string>(type: "longtext", nullable: true),
                    Answer = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkoutPlans",
                columns: table => new
                {
                    WorkoutPlanId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    ReviewAverage = table.Column<float>(type: "float", nullable: false),
                    ReviewCount = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserWorkoutPlan",
                columns: table => new
                {
                    AssignedPlansWorkoutPlanId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UsersUserId = table.Column<Guid>(type: "char(36)", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkoutDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    WorkoutPlanId = table.Column<Guid>(type: "char(36)", nullable: true)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    ExerciseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Repetitions = table.Column<int>(type: "int", nullable: true),
                    Series = table.Column<int>(type: "int", nullable: true),
                    Time = table.Column<int>(type: "int", nullable: true),
                    Rest = table.Column<int>(type: "int", nullable: true),
                    WorkoutDayId = table.Column<Guid>(type: "char(36)", nullable: true)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutDayId",
                table: "Exercises",
                column: "WorkoutDayId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRecords_UserId",
                table: "PersonalRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityQuestions_UserId",
                table: "SecurityQuestions",
                column: "UserId");

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
                name: "PersonalRecords");

            migrationBuilder.DropTable(
                name: "SecurityQuestions");

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
