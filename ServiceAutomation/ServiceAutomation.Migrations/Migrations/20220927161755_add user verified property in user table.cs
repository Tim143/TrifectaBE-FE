using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class adduserverifiedpropertyinusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerifiedUser",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserLevelsInfos",
                columns: table => new
                {
                    BranchCount = table.Column<int>(type: "integer", nullable: false),
                    BasicLevelId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_UserLevelsInfos_BasicLevels_BasicLevelId",
                        column: x => x.BasicLevelId,
                        principalTable: "BasicLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLevelsInfos_BasicLevelId",
                table: "UserLevelsInfos",
                column: "BasicLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLevelsInfos");

            migrationBuilder.DropColumn(
                name: "IsVerifiedUser",
                table: "Users");
        }
    }
}
