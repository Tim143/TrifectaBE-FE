using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class rolesfvdv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VerificationPhotoPath",
                table: "IndividualUserOrganizationsData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationPhotoPath",
                table: "IndividualEntrepreneurUserOrganizationsData",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationPhotoPath",
                table: "IndividualUserOrganizationsData");

            migrationBuilder.DropColumn(
                name: "VerificationPhotoPath",
                table: "IndividualEntrepreneurUserOrganizationsData");
        }
    }
}
