using System;
using Microsoft.EntityFrameworkCore.Migrations;
using ServiceAutomation.DataAccess.DataSeeding;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class AddLevelBonusRewards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            CreateLevelBonusRewardPercentsTable(migrationBuilder);
            FillLevelBonusRewardPercentsTable(migrationBuilder);
            CreateLevelBonusRewardsTable(migrationBuilder);
            FillLevelBonusRewardsTable(migrationBuilder);
        }

        private void CreateLevelBonusRewardPercentsTable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LevelBonusRewardPercents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PackageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Percent = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelBonusRewardPercents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LevelBonusRewardPercents_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LevelBonusRewardPercents_PackageId",
                table: "LevelBonusRewardPercents",
                column: "PackageId");
        }

        private void FillLevelBonusRewardPercentsTable(MigrationBuilder migrationBuilder)
        {
            var LevelBonusRewardPercents = LevelBonusRewardPercentsInitialData.LevelBonusRewardPercentSeeds;
            foreach (var LevelBonusRewardPercent in LevelBonusRewardPercents)
            {
                migrationBuilder.Sql($"insert into public.\"LevelBonusRewardPercents\" (\"Id\", \"PackageId\", \"Percent\") values ('{Guid.NewGuid()}', '{LevelBonusRewardPercent.PackageId}', {LevelBonusRewardPercent.Percent})");
            }
        }

        private void CreateLevelBonusRewardsTable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "LevelBonusRewards",
               columns: table => new
               {
                   Id = table.Column<Guid>(type: "uuid", nullable: false),
                   LevelId = table.Column<Guid>(type: "uuid", nullable: false),
                   Reward = table.Column<decimal>(type: "numeric", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_LevelBonusRewards", x => x.Id);
                   table.ForeignKey(
                       name: "FK_LevelBonusRewards_BasicLevels_LevelId",
                       column: x => x.LevelId,
                       principalTable: "BasicLevels",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateIndex(
                name: "IX_LevelBonusRewards_LevelId",
                table: "LevelBonusRewards",
                column: "LevelId");
        }

        private void FillLevelBonusRewardsTable(MigrationBuilder migrationBuilder)
        {
            var LevelBonusRewards = LevelBonusRewardsInitialData.LevelBonusRewardSeeds;
            foreach (var LevelBonusReward in LevelBonusRewards)
            {
                migrationBuilder.Sql($"insert into public.\"LevelBonusRewards\" (\"Id\", \"LevelId\", \"Reward\") values ('{Guid.NewGuid()}', '{LevelBonusReward.LevelId}', {LevelBonusReward.Reward})");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LevelBonusRewardPercents");

            migrationBuilder.DropTable(
                name: "LevelBonusRewards");
        }
    }
}
