using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class added4columnsinlegalentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CertificateDateIssue",
                table: "LegalUserOrganizationsData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                table: "LegalUserOrganizationsData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "LegalUserOrganizationsData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationAuthority",
                table: "LegalUserOrganizationsData",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateDateIssue",
                table: "LegalUserOrganizationsData");

            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "LegalUserOrganizationsData");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "LegalUserOrganizationsData");

            migrationBuilder.DropColumn(
                name: "RegistrationAuthority",
                table: "LegalUserOrganizationsData");
        }
    }
}
