using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class rvresdv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OldContact",
                table: "UserContactVerifications",
                newName: "OldData");

            migrationBuilder.RenameColumn(
                name: "NewContact",
                table: "UserContactVerifications",
                newName: "NewData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OldData",
                table: "UserContactVerifications",
                newName: "OldContact");

            migrationBuilder.RenameColumn(
                name: "NewData",
                table: "UserContactVerifications",
                newName: "NewContact");
        }
    }
}
