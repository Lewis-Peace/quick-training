using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lift.Buddy.Core.Migrations
{
    /// <inheritdoc />
    public partial class AdderCreatedByToWorkoutPlanAndRenaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "WorkoutSchedules",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserAssociation",
                columns: table => new
                {
                    TrainerUsername = table.Column<string>(type: "TEXT", nullable: false),
                    AthleteUsername = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssociation", x => new { x.TrainerUsername, x.AthleteUsername });
                    table.ForeignKey(
                        name: "FK_UserAssociation_Users_AthleteUsername",
                        column: x => x.AthleteUsername,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAssociation_Users_TrainerUsername",
                        column: x => x.TrainerUsername,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSchedules_CreatedBy",
                table: "WorkoutSchedules",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssociation_AthleteUsername",
                table: "UserAssociation",
                column: "AthleteUsername",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAssociation_TrainerUsername",
                table: "UserAssociation",
                column: "TrainerUsername",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSchedules_Users_CreatedBy",
                table: "WorkoutSchedules",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSchedules_Users_CreatedBy",
                table: "WorkoutSchedules");

            migrationBuilder.DropTable(
                name: "UserAssociation");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutSchedules_CreatedBy",
                table: "WorkoutSchedules");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WorkoutSchedules");
        }
    }
}
