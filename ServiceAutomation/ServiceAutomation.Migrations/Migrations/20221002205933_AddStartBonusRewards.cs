using System;
using Microsoft.EntityFrameworkCore.Migrations;
using ServiceAutomation.DataAccess.DataSeeding;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class AddStartBonusRewards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            CreateStartBonusRewardsTable(migrationBuilder);
            FillStartBonusRewardsTable(migrationBuilder);
        }

        protected void CreateStartBonusRewardsTable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StartBonusRewards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DurationOfDays = table.Column<int>(type: "integer", nullable: false),
                    PackageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountOfSale = table.Column<int>(type: "integer", nullable: false),
                    Percent = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartBonusRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StartBonusRewards_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StartBonusRewards_PackageId",
                table: "StartBonusRewards",
                column: "PackageId");
        }

        private void FillStartBonusRewardsTable(MigrationBuilder migrationBuilder)
        {
            var startBonusRewards = StartBonusRewardsInitialData.StartBonusRewardSeeds;
            foreach (var startBonusReward in startBonusRewards)
            {
                migrationBuilder.Sql($"insert into public.\"StartBonusRewards\" (\"Id\", \"PackageId\", \"Percent\", \"DurationOfDays\", \"CountOfSale\") values ('{Guid.NewGuid()}', '{startBonusReward.PackageId}', {startBonusReward.Percent}, {startBonusReward.DurationOfDays}, {startBonusReward.CountOfSale})");
            }
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StartBonusRewards");
        }
    }
}
