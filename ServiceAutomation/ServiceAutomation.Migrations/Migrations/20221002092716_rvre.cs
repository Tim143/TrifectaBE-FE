using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class rvre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewContact",
                table: "UserContactVerifications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldContact",
                table: "UserContactVerifications",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NewContact",
                table: "UserContactVerifications");

            migrationBuilder.DropColumn(
                name: "OldContact",
                table: "UserContactVerifications");

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
    }
}
