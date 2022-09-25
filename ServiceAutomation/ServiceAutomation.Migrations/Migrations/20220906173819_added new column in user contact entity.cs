using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class addednewcolumninusercontactentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "UserContacts");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "UserContacts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "UserContacts");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "UserContacts",
                type: "text",
                nullable: true);
        }
    }
}
