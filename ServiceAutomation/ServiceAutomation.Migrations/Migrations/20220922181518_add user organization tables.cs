using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class adduserorganizationtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserAccountOrganizationId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IndividualUserOrganizationsData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LegalEntityFullName = table.Column<string>(type: "text", nullable: true),
                    HeadFullName = table.Column<string>(type: "text", nullable: true),
                    LegalEntityAbbreviatedName = table.Column<string>(type: "text", nullable: true),
                    HeadPosition = table.Column<string>(type: "text", nullable: true),
                    UNP = table.Column<string>(type: "text", nullable: true),
                    BaseOrganization = table.Column<string>(type: "text", nullable: true),
                    AccountantName = table.Column<string>(type: "text", nullable: true),
                    CertificateNumber = table.Column<string>(type: "text", nullable: true),
                    RegistrationAuthority = table.Column<string>(type: "text", nullable: true),
                    CertificateDateIssue = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BankRegion = table.Column<string>(type: "text", nullable: true),
                    BankLocality = table.Column<string>(type: "text", nullable: true),
                    BankStreet = table.Column<string>(type: "text", nullable: true),
                    BankHouseNumber = table.Column<string>(type: "text", nullable: true),
                    BeneficiaryBankName = table.Column<string>(type: "text", nullable: true),
                    CheckingAccount = table.Column<string>(type: "text", nullable: true),
                    SWIFT = table.Column<string>(type: "text", nullable: true),
                    Region = table.Column<string>(type: "text", nullable: true),
                    Locality = table.Column<string>(type: "text", nullable: true),
                    Index = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    HouseNumber = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<int>(type: "integer", nullable: false),
                    RoomNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualUserOrganizationsData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalUserOrganizationsData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: true),
                    Locality = table.Column<string>(type: "text", nullable: true),
                    BankStreet = table.Column<string>(type: "text", nullable: true),
                    BankHouseNumber = table.Column<string>(type: "text", nullable: true),
                    BeneficiaryBankName = table.Column<string>(type: "text", nullable: true),
                    CheckingAccount = table.Column<string>(type: "text", nullable: true),
                    SWIFT = table.Column<string>(type: "text", nullable: true),
                    Disctrict = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Index = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    HouseNumber = table.Column<string>(type: "text", nullable: true),
                    Flat = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalUserOrganizationsData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccountOrganizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeOfEmployment = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountOrganizations", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserAccountOrganizations_UserAccountOrganizationId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "IndividualUserOrganizationsData");

            migrationBuilder.DropTable(
                name: "LegalUserOrganizationsData");

            migrationBuilder.DropTable(
                name: "UserAccountOrganizations");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserAccountOrganizationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserAccountOrganizationId",
                table: "Users");
        }
    }
}
