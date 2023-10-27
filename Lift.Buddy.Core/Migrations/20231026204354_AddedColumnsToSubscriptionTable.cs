using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lift.Buddy.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnsToSubscriptionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUser");

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    AthleteId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TrainerId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SubscriptionType = table.Column<int>(type: "int", nullable: false),
                    Expiration = table.Column<string>(type: "longtext", nullable: true)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_AthleteId",
                table: "Subscriptions",
                column: "AthleteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.CreateTable(
                name: "UserUser",
                columns: table => new
                {
                    SubscribedAthletesUserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TrainersUserId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUser", x => new { x.SubscribedAthletesUserId, x.TrainersUserId });
                    table.ForeignKey(
                        name: "FK_UserUser_Users_SubscribedAthletesUserId",
                        column: x => x.SubscribedAthletesUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUser_Users_TrainersUserId",
                        column: x => x.TrainersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_TrainersUserId",
                table: "UserUser",
                column: "TrainersUserId");
        }
    }
}
