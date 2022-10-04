using System;
using Microsoft.EntityFrameworkCore.Migrations;
using ServiceAutomation.DataAccess.DataSeeding;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class AddLevels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            CreateTables(migrationBuilder);
            FillTables(migrationBuilder);
        }

        #region TableCreating
        private void CreateTables(MigrationBuilder migrationBuilder)
        {
            CreateBasicLevelsTable(migrationBuilder);
            CreateMonthlyLevelsTable(migrationBuilder);
        }

        private void CreateBasicLevelsTable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasicLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PartnersCount = table.Column<int>(type: "integer", nullable: true),
                    PartnersLevelId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Turnover = table.Column<decimal>(type: "numeric", nullable: true),
                    Level = table.Column<int>(type: "integer", nullable: false)
                },

            constraints: table =>
            {
                table.PrimaryKey("PK_BasicLevels", x => x.Id);
                table.ForeignKey(
                    name: "FK_BasicLevels_BasicLevels_PartnersLevelId",
                    column: x => x.PartnersLevelId,
                    principalTable: "BasicLevels",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        }

        private void CreateMonthlyLevelsTable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonthlyLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Turnover = table.Column<decimal>(type: "numeric", nullable: true),
                    Level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyLevels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasicLevels_PartnersLevelId",
                table: "BasicLevels",
                column: "PartnersLevelId");
        }

        #endregion

        #region TableFilling

        private void FillTables(MigrationBuilder migrationBuilder)
        {
            FillBasicLevelsTable(migrationBuilder);
            FillMonthlyLevelsTable(migrationBuilder);
        }

        private void FillBasicLevelsTable(MigrationBuilder migrationBuilder)
        {
            var basicLevels = BasicLevelsInitialData.BasicLevelSeeds;
            foreach (var basicLevel in basicLevels)
            {
                migrationBuilder.Sql($"insert into public.\"BasicLevels\" (\"Id\", \"Name\", \"Level\", \"Turnover\", \"PartnersCount\", \"PartnersLevelId\") values ('{basicLevel.Id}', '{basicLevel.Name}', {(int)basicLevel.Level}, {(basicLevel.Turnover.HasValue ? basicLevel.Turnover : "Null")}, {(basicLevel.PartnersCount.HasValue ? basicLevel.PartnersCount : "Null")}, {(basicLevel.PartnersLevelId.HasValue ? $"'{basicLevel.PartnersLevelId}'" : "Null")})");
            }
        }

        private void FillMonthlyLevelsTable(MigrationBuilder migrationBuilder)
        {
            var monthlyLevels = MonthlyLevelsEnitialData.MonthlyLevelSeeds;
            foreach (var monthlyLevel in monthlyLevels)
            {
                migrationBuilder.Sql($"insert into public.\"MonthlyLevels\" (\"Id\", \"Name\", \"Level\", \"Turnover\") values ('{monthlyLevel.Id}', '{monthlyLevel.Name}', {(int)monthlyLevel.Level}, {(monthlyLevel.Turnover.HasValue ? monthlyLevel.Turnover : "Null")})");
            }
        }

        #endregion

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasicLevels");

            migrationBuilder.DropTable(
                name: "MonthlyLevels");
        }
    }
}
