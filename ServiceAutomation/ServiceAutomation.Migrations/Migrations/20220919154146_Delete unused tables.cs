using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class Deleteunusedtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserOrganization_UserOrganizationId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "LegalEntities");

            migrationBuilder.DropTable(
                name: "UserOrganization");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserOrganizationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserOrganizationId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserOrganizationId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LegalEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountantName = table.Column<string>(type: "text", nullable: true),
                    BankHouseNumber = table.Column<string>(type: "text", nullable: true),
                    BankStreet = table.Column<string>(type: "text", nullable: true),
                    BaseOrganization = table.Column<string>(type: "text", nullable: true),
                    BeneficiaryBankName = table.Column<string>(type: "text", nullable: true),
                    CertificateDateIssue = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CertificateNumber = table.Column<string>(type: "text", nullable: true),
                    CheckingAccount = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<int>(type: "integer", nullable: false),
                    HeadFullName = table.Column<string>(type: "text", nullable: true),
                    HeadPosition = table.Column<string>(type: "text", nullable: true),
                    HouseNumber = table.Column<string>(type: "text", nullable: true),
                    Index = table.Column<string>(type: "text", nullable: true),
                    LegalEntityAbbreviatedName = table.Column<string>(type: "text", nullable: true),
                    LegalEntityFullName = table.Column<string>(type: "text", nullable: true),
                    Locality = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<int>(type: "integer", nullable: false),
                    OrganizationLocality = table.Column<string>(type: "text", nullable: true),
                    OrganizationRegion = table.Column<string>(type: "text", nullable: true),
                    OrganizationStreet = table.Column<string>(type: "text", nullable: true),
                    Region = table.Column<string>(type: "text", nullable: true),
                    RegistrationAuthority = table.Column<string>(type: "text", nullable: true),
                    RoomNumber = table.Column<string>(type: "text", nullable: true),
                    SWIFT = table.Column<string>(type: "text", nullable: true),
                    UNP = table.Column<string>(type: "text", nullable: true),
                    UserOrganizationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserOrganization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationType = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrganization", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserOrganizationId",
                table: "Users",
                column: "UserOrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserOrganization_UserOrganizationId",
                table: "Users",
                column: "UserOrganizationId",
                principalTable: "UserOrganization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
