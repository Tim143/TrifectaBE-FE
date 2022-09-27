using System;
using Microsoft.EntityFrameworkCore.Migrations;
using ServiceAutomation.DataAccess.DataSeeding;
using ServiceAutomation.DataAccess.Models.Enums;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class AddBasicLevelToUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BasicLevelId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_BasicLevelId",
                table: "Users",
                column: "BasicLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_BasicLevels_BasicLevelId",
                table: "Users",
                column: "BasicLevelId",
                principalTable: "BasicLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            var firstbasicLevel = BasicLevelsEnitialData.BasicLevelIdsByType[Level.FirstLevel];
            migrationBuilder.Sql($"update public.\"Users\" set \"BasicLevelId\" = '{firstbasicLevel}'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_BasicLevels_BasicLevelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_BasicLevelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BasicLevelId",
                table: "Users");
        }
    }
}
