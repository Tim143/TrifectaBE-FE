using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class newmigartion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserOrganization",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "LegalEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserOrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<int>(type: "integer", nullable: false),
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
                    Region = table.Column<string>(type: "text", nullable: true),
                    Locality = table.Column<string>(type: "text", nullable: true),
                    BankStreet = table.Column<string>(type: "text", nullable: true),
                    BankHouseNumber = table.Column<string>(type: "text", nullable: true),
                    BeneficiaryBankName = table.Column<string>(type: "text", nullable: true),
                    CheckingAccount = table.Column<string>(type: "text", nullable: true),
                    SWIFT = table.Column<string>(type: "text", nullable: true),
                    OrganizationRegion = table.Column<string>(type: "text", nullable: true),
                    OrganizationLocality = table.Column<string>(type: "text", nullable: true),
                    Index = table.Column<string>(type: "text", nullable: true),
                    OrganizationStreet = table.Column<string>(type: "text", nullable: true),
                    HouseNumber = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<int>(type: "integer", nullable: false),
                    RoomNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LegalEntities");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserOrganization");
        }
    }
}
