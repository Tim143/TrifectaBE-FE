using System;
using Microsoft.EntityFrameworkCore.Migrations;
using ServiceAutomation.DataAccess.DataSeeding;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class AddTravelBonusRequirements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            CreateTravelBonusRequirementsTable(migrationBuilder);
            FillTravelBonusRequirementsTable(migrationBuilder);
        }

        protected void CreateTravelBonusRequirementsTable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelBonusRequirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PackageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Turnover = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelBonusRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelBonusRequirements_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelBonusRequirements_PackageId",
                table: "TravelBonusRequirements",
                column: "PackageId");
        }

        protected void FillTravelBonusRequirementsTable(MigrationBuilder migrationBuilder)
        {
            var travelBonusRequirements = TravelBonusRequirementInitialData.TravelBonusRequirementSeeds;
            foreach (var requirement in travelBonusRequirements)
            {
                migrationBuilder.Sql($"insert into public.\"TravelBonusRequirements\" (\"Id\", \"PackageId\", \"Turnover\") values ('{Guid.NewGuid()}', '{requirement.PackageId}', {requirement.Turnover})");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelBonusRequirements");
        }
    }
}
