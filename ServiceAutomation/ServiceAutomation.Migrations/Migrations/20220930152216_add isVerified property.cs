using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class addisVerifiedproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerivied",
                table: "LegalUserOrganizationsData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "IndividualUserOrganizationsData",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "CertificateDateIssue",
                table: "IndividualUserOrganizationsData",
                type: "text",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerivied",
                table: "IndividualUserOrganizationsData",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerivied",
                table: "LegalUserOrganizationsData");

            migrationBuilder.DropColumn(
                name: "IsVerivied",
                table: "IndividualUserOrganizationsData");

            migrationBuilder.AlterColumn<int>(
                name: "Location",
                table: "IndividualUserOrganizationsData",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CertificateDateIssue",
                table: "IndividualUserOrganizationsData",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
