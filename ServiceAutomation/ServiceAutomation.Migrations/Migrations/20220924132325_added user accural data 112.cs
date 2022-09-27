using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class addeduseraccuraldata112 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccrualsEntity_Users_UserId",
                table: "AccrualsEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccrualsEntity",
                table: "AccrualsEntity");

            migrationBuilder.RenameTable(
                name: "AccrualsEntity",
                newName: "Accruals");

            migrationBuilder.RenameIndex(
                name: "IX_AccrualsEntity_UserId",
                table: "Accruals",
                newName: "IX_Accruals_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accruals",
                table: "Accruals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accruals_Users_UserId",
                table: "Accruals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accruals_Users_UserId",
                table: "Accruals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accruals",
                table: "Accruals");

            migrationBuilder.RenameTable(
                name: "Accruals",
                newName: "AccrualsEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Accruals_UserId",
                table: "AccrualsEntity",
                newName: "IX_AccrualsEntity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccrualsEntity",
                table: "AccrualsEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccrualsEntity_Users_UserId",
                table: "AccrualsEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
