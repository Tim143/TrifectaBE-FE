using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class rolesfvdvым : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserAccuralsVerificationEntityId",
                table: "Accruals",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserAccuralsVerifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccuralsVerifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccuralsVerifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accruals_UserAccuralsVerificationEntityId",
                table: "Accruals",
                column: "UserAccuralsVerificationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccuralsVerifications_UserId",
                table: "UserAccuralsVerifications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accruals_UserAccuralsVerifications_UserAccuralsVerification~",
                table: "Accruals",
                column: "UserAccuralsVerificationEntityId",
                principalTable: "UserAccuralsVerifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accruals_UserAccuralsVerifications_UserAccuralsVerification~",
                table: "Accruals");

            migrationBuilder.DropTable(
                name: "UserAccuralsVerifications");

            migrationBuilder.DropIndex(
                name: "IX_Accruals_UserAccuralsVerificationEntityId",
                table: "Accruals");

            migrationBuilder.DropColumn(
                name: "UserAccuralsVerificationEntityId",
                table: "Accruals");
        }
    }
}
