using System;
using Microsoft.EntityFrameworkCore.Migrations;
using ServiceAutomation.DataAccess.DataSeeding;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class AddTeamBonusRewards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            CreateTeamBonusRewardsTable(migrationBuilder);
            FillTeamBonusRewardsTable(migrationBuilder);
        }

        private void CreateTeamBonusRewardsTable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamBonusRewards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MonthlyLevelId = table.Column<Guid>(type: "uuid", nullable: false),
                    Percent = table.Column<double>(type: "double precision", nullable: false),
                    CommonTurnover = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamBonusRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamBonusRewards_MonthlyLevels_MonthlyLevelId",
                        column: x => x.MonthlyLevelId,
                        principalTable: "MonthlyLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamBonusRewards_MonthlyLevelId",
                table: "TeamBonusRewards",
                column: "MonthlyLevelId");
        }

        private void FillTeamBonusRewardsTable(MigrationBuilder migrationBuilder)
        {
            var teamBonusRewards = TeamBonusRewardsInitialData.TeamBonusRewardSeeds;
            foreach (var teamBonusReward in teamBonusRewards)
            {
                migrationBuilder.Sql($"insert into public.\"TeamBonusRewards\" (\"Id\", \"MonthlyLevelId\", \"CommonTurnover\", \"Percent\") values ('{Guid.NewGuid()}', '{teamBonusReward.MonthlyLevelId}', {teamBonusReward.CommonTurnover}, '{(string.Format("{0:0.00}", teamBonusReward.Percent).Replace(',', '.'))}')");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamBonusRewards");
        }
    }
}
