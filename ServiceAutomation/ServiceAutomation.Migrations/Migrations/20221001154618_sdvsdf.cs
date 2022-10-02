using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class sdvsdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserAccountOrganizations_UserAccountOrganizationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserAccountOrganizationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserAccountOrganizationId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountOrganizations_UserId",
                table: "UserAccountOrganizations",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccountOrganizations_Users_UserId",
                table: "UserAccountOrganizations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccountOrganizations_Users_UserId",
                table: "UserAccountOrganizations");

            migrationBuilder.DropIndex(
                name: "IX_UserAccountOrganizations_UserId",
                table: "UserAccountOrganizations");

            migrationBuilder.AddColumn<Guid>(
                name: "UserAccountOrganizationId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserAccountOrganizationId",
                table: "Users",
                column: "UserAccountOrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserAccountOrganizations_UserAccountOrganizationId",
                table: "Users",
                column: "UserAccountOrganizationId",
                principalTable: "UserAccountOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
