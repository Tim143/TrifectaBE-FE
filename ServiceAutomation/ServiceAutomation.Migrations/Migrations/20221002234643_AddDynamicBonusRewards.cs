using System;
using Microsoft.EntityFrameworkCore.Migrations;
using ServiceAutomation.DataAccess.DataSeeding;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class AddDynamicBonusRewards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            CreateDynamicBonusRewardsTable(migrationBuilder);
            FillCreateDynamicBonusRewardsTable(migrationBuilder);
        }

        protected void CreateDynamicBonusRewardsTable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DynamicBonusRewards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SalesNumber = table.Column<int>(type: "integer", nullable: false),
                    Percent = table.Column<int>(type: "integer", nullable: false),
                    DurationOfDays = table.Column<int>(type: "integer", nullable: true),
                    HasRestriction = table.Column<bool>(type: "boolean", nullable: false),
                    PackageId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicBonusRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicBonusRewards_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicBonusRewards_PackageId",
                table: "DynamicBonusRewards",
                column: "PackageId");
        }

        private void FillCreateDynamicBonusRewardsTable(MigrationBuilder migrationBuilder)
        {
            var dynamicBonusesRewards = DynamicBonusRewardsInitialData.DynamicBonusRewardSeeds;
            foreach( var reward in dynamicBonusesRewards)
            {
                migrationBuilder.Sql($"insert into public.\"DynamicBonusRewards\" (\"Id\", \"PackageId\", \"Percent\", \"DurationOfDays\", \"SalesNumber\", \"HasRestriction\") values ('{Guid.NewGuid()}', '{reward.PackageId}', {reward.Percent}, {(reward.DurationOfDays?.ToString() ?? "Null")}, {reward.SalesNumber}, {reward.HasRestriction})");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicBonusRewards");
        }
    }
}
