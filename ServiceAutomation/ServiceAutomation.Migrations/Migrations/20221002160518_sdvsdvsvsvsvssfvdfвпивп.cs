using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class sdvsdvsvsvsvssfvdfвпивп : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    CertificateDateIssue = table.Column<string>(type: "text", nullable: true),
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
                    Location = table.Column<string>(type: "text", nullable: true),
                    RoomNumber = table.Column<string>(type: "text", nullable: true),
                    IsVerivied = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualUserOrganizationsData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndividualUserOrganizationsData");
        }
    }
}
