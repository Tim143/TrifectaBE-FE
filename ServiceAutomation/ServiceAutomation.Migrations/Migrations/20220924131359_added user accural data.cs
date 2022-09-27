using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class addeduseraccuraldata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "ProfilePhotos");

            migrationBuilder.AddColumn<string>(
                name: "FullPath",
                table: "ProfilePhotos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProfilePhotos",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccrualsEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccuralName = table.Column<string>(type: "text", nullable: true),
                    ReferralName = table.Column<string>(type: "text", nullable: true),
                    AccuralPercent = table.Column<int>(type: "integer", nullable: false),
                    TransactionStatus = table.Column<int>(type: "integer", nullable: false),
                    InitialAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    AccuralAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    AccuralDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccrualsEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccrualsEntity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccrualsEntity_UserId",
                table: "AccrualsEntity",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccrualsEntity");

            migrationBuilder.DropColumn(
                name: "FullPath",
                table: "ProfilePhotos");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProfilePhotos");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "ProfilePhotos",
                type: "bytea",
                nullable: true);
        }
    }
}
