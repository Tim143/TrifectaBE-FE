using System;
using Microsoft.EntityFrameworkCore.Migrations;
using ServiceAutomation.DataAccess.DataSeeding;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class AddAutoBonusRewards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ForBsicLevelId",
                table: "Accruals",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AutoBonusRewards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BasicLevelId = table.Column<Guid>(type: "uuid", nullable: false),
                    PackageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reward = table.Column<decimal>(type: "numeric", nullable: false),
                    MonthlyTurnover = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoBonusRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutoBonusRewards_BasicLevels_BasicLevelId",
                        column: x => x.BasicLevelId,
                        principalTable: "BasicLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoBonusRewards_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accruals_ForBsicLevelId",
                table: "Accruals",
                column: "ForBsicLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoBonusRewards_BasicLevelId",
                table: "AutoBonusRewards",
                column: "BasicLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoBonusRewards_PackageId",
                table: "AutoBonusRewards",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accruals_BasicLevels_ForBsicLevelId",
                table: "Accruals",
                column: "ForBsicLevelId",
                principalTable: "BasicLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            FillAutoBonusRewardsTable(migrationBuilder);
        }


        protected void FillAutoBonusRewardsTable(MigrationBuilder migrationBuilder)
        {
            var autoBonusRewards = AutoBonusRewardInitialData.AutoBonusRewardSeeds;
            foreach (var reward in autoBonusRewards)
            {
                migrationBuilder.Sql($"insert into public.\"AutoBonusRewards\" (\"Id\", \"PackageId\", \"Reward\", \"BasicLevelId\", \"MonthlyTurnover\") values ('{Guid.NewGuid()}', '{reward.PackageId}', {reward.Reward}, '{reward.BasicLevelId}', {reward.MonthlyTurnover})");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accruals_BasicLevels_ForBsicLevelId",
                table: "Accruals");

            migrationBuilder.DropTable(
                name: "AutoBonusRewards");

            migrationBuilder.DropIndex(
                name: "IX_Accruals_ForBsicLevelId",
                table: "Accruals");

            migrationBuilder.DropColumn(
                name: "ForBsicLevelId",
                table: "Accruals");
        }
    }
}
