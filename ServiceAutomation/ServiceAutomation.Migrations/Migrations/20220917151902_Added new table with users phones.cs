using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class Addednewtablewithusersphones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserPhoneNumberId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserPhones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPhones", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserPhoneNumberId",
                table: "Users",
                column: "UserPhoneNumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserPhones_UserPhoneNumberId",
                table: "Users",
                column: "UserPhoneNumberId",
                principalTable: "UserPhones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserPhones_UserPhoneNumberId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserPhones");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserPhoneNumberId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserPhoneNumberId",
                table: "Users");
        }
    }
}
