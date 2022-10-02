using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class AddedLevelStatistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasicLevelStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LevelId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReachingLevelDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Turnover = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicLevelStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicLevelStatistics_BasicLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "BasicLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasicLevelStatistics_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyLevelStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LevelId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReachingLevelDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Turnover = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyLevelStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyLevelStatistics_MonthlyLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "MonthlyLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonthlyLevelStatistics_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasicLevelStatistics_LevelId",
                table: "BasicLevelStatistics",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicLevelStatistics_UserId",
                table: "BasicLevelStatistics",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyLevelStatistics_LevelId",
                table: "MonthlyLevelStatistics",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyLevelStatistics_UserId",
                table: "MonthlyLevelStatistics",
                column: "UserId");


            migrationBuilder.Sql("INSERT INTO public.\"BasicLevelStatistics\"(\"Id\", \"UserId\", \"LevelId\", \"ReachingLevelDate\", \"Turnover\")\n"
                                + "Select  uuid_in(md5(random()::text || random()::text)::cstring) as \"Id\", u.\"Id\" as \"UserId\",\n"
                                 + $"u.\"BasicLevelId\" as \"LevelId\", '{DateTime.Now}', b.\"Turnover\" as \"Turnover\"\n"
                                 + "from public.\"Users\" as u\n"
                                 + "inner join public.\"BasicLevels\" as b on u.\"BasicLevelId\" = b.\"Id\"");

            migrationBuilder.Sql("INSERT INTO public.\"MonthlyLevelStatistics\"(\"Id\", \"UserId\", \"LevelId\", \"ReachingLevelDate\", \"Turnover\")\n"
                                + "Select  uuid_in(md5(random()::text || random()::text)::cstring) as \"Id\", u.\"Id\" as \"UserId\",\n"
                                + $"	   '02142b58-bba1-4ba2-91ce-bcd1414489f0' as \"LevelId\", '{DateTime.Now}', null as \"Turnover\"\n"
                                + "from public.\"Users\" as u");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasicLevelStatistics");

            migrationBuilder.DropTable(
                name: "MonthlyLevelStatistics");
        }
    }
}
