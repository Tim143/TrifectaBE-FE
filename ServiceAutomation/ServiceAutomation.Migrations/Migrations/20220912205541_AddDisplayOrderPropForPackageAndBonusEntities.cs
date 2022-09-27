using Microsoft.EntityFrameworkCore.Migrations;
using ServiceAutomation.DataAccess.DataSeeding;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class AddDisplayOrderPropForPackageAndBonusEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AddDisplayOrderColumnForPackagesTable(migrationBuilder);
            AddDisplayOrderColumnForBonusesTable(migrationBuilder);
            FillDisplayPropForPackagesTable(migrationBuilder);
            FillDisplayPropForBonusesTable(migrationBuilder);
        }

        private void AddDisplayOrderColumnForPackagesTable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Packages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        private void AddDisplayOrderColumnForBonusesTable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Bonuses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        private void FillDisplayPropForPackagesTable(MigrationBuilder migrationBuilder)
        {
            var packages = PackagesInitialData.PackageSeeds;
            foreach (var package in packages)
            {
                migrationBuilder.Sql($"update public.\"Packages\" set \"DisplayOrder\" = {package.DisplayOrder} where \"Id\"='{package.Id}'");
            }
        }

        private void FillDisplayPropForBonusesTable(MigrationBuilder migrationBuilder)
        {
            var bonuses = BonusesInitialData.BonusSeeds;
            foreach (var bonus in bonuses)
            {
                migrationBuilder.Sql($"update public.\"Bonuses\" set \"DisplayOrder\" = {bonus.DisplayOrder} where \"Id\"='{bonus.Id}'");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Bonuses");
        }
    }
}
