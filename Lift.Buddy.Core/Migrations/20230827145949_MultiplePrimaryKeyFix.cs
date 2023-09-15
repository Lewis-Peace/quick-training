using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lift.Buddy.Core.Migrations
{
    /// <inheritdoc />
    public partial class MultiplePrimaryKeyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutAssignments",
                table: "WorkoutAssignments");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutAssignments_WorkoutId",
                table: "WorkoutAssignments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutAssignments",
                table: "WorkoutAssignments",
                columns: new[] { "WorkoutId", "WorkoutUser" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutAssignments_WorkoutUser",
                table: "WorkoutAssignments",
                column: "WorkoutUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutAssignments",
                table: "WorkoutAssignments");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutAssignments_WorkoutUser",
                table: "WorkoutAssignments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutAssignments",
                table: "WorkoutAssignments",
                column: "WorkoutUser");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutAssignments_WorkoutId",
                table: "WorkoutAssignments",
                column: "WorkoutId");
        }
    }
}
