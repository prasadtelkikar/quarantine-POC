using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace touch_core_internal.Migrations
{
    public partial class RewardsInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Badges_Categories_CategoryId",
                table: "Badges");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Badges",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    RewardId = table.Column<Guid>(nullable: false),
                    BadgeId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.RewardId);
                    table.ForeignKey(
                        name: "FK_Rewards_Badges_BadgeId",
                        column: x => x.BadgeId,
                        principalTable: "Badges",
                        principalColumn: "BadgeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rewards_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rewards_BadgeId",
                table: "Rewards",
                column: "BadgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rewards_EmployeeId",
                table: "Rewards",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Badges_Categories_CategoryId",
                table: "Badges",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Badges_Categories_CategoryId",
                table: "Badges");

            migrationBuilder.DropTable(
                name: "Rewards");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Badges",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Badges_Categories_CategoryId",
                table: "Badges",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
