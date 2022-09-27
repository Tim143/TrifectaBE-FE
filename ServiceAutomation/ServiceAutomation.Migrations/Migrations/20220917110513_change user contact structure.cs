using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class changeusercontactstructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "UserContacts");

            migrationBuilder.DropColumn(
                name: "IdentityCode",
                table: "UserContacts");

            migrationBuilder.DropColumn(
                name: "PassportNumber",
                table: "UserContacts");

            migrationBuilder.DropColumn(
                name: "PassportSeries",
                table: "UserContacts");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "UserContacts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "UserContacts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityCode",
                table: "UserContacts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNumber",
                table: "UserContacts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportSeries",
                table: "UserContacts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "UserContacts",
                type: "text",
                nullable: true);
        }
    }
}
