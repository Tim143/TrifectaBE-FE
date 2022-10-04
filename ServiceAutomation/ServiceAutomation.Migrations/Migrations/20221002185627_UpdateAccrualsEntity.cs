using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class UpdateAccrualsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccuralName",
                table: "Accruals");

            migrationBuilder.DropColumn(
                name: "ReferralName",
                table: "Accruals");

            migrationBuilder.AlterColumn<int>(
                name: "AccuralPercent",
                table: "Accruals",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Guid>(
                name: "BonusId",
                table: "Accruals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ForWhomId",
                table: "Accruals",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "Accruals",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accruals_BonusId",
                table: "Accruals",
                column: "BonusId");

            migrationBuilder.CreateIndex(
                name: "IX_Accruals_ForWhomId",
                table: "Accruals",
                column: "ForWhomId");

            migrationBuilder.CreateIndex(
                name: "IX_Accruals_UserEntityId",
                table: "Accruals",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accruals_Bonuses_BonusId",
                table: "Accruals",
                column: "BonusId",
                principalTable: "Bonuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accruals_Users_ForWhomId",
                table: "Accruals",
                column: "ForWhomId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accruals_Users_UserEntityId",
                table: "Accruals",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accruals_Bonuses_BonusId",
                table: "Accruals");

            migrationBuilder.DropForeignKey(
                name: "FK_Accruals_Users_ForWhomId",
                table: "Accruals");

            migrationBuilder.DropForeignKey(
                name: "FK_Accruals_Users_UserEntityId",
                table: "Accruals");

            migrationBuilder.DropIndex(
                name: "IX_Accruals_BonusId",
                table: "Accruals");

            migrationBuilder.DropIndex(
                name: "IX_Accruals_ForWhomId",
                table: "Accruals");

            migrationBuilder.DropIndex(
                name: "IX_Accruals_UserEntityId",
                table: "Accruals");

            migrationBuilder.DropColumn(
                name: "BonusId",
                table: "Accruals");

            migrationBuilder.DropColumn(
                name: "ForWhomId",
                table: "Accruals");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Accruals");

            migrationBuilder.AlterColumn<int>(
                name: "AccuralPercent",
                table: "Accruals",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccuralName",
                table: "Accruals",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferralName",
                table: "Accruals",
                type: "text",
                nullable: true);
        }
    }
}
