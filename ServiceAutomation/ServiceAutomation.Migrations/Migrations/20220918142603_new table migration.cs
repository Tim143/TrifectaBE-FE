using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class newtablemigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserOrganizationId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserOrganization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrganization", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserOrganizationId",
                table: "Users",
                column: "UserOrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserOrganization_UserOrganizationId",
                table: "Users",
                column: "UserOrganizationId",
                principalTable: "UserOrganization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserOrganization_UserOrganizationId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserOrganization");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserOrganizationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserOrganizationId",
                table: "Users");
        }
    }
}
