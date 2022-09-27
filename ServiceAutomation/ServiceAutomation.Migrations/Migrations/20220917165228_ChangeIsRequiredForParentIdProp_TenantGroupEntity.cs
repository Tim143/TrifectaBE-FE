using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class ChangeIsRequiredForParentIdProp_TenantGroupEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenantGroups_TenantGroups_ParentId",
                table: "TenantGroups");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "TenantGroups",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_TenantGroups_TenantGroups_ParentId",
                table: "TenantGroups",
                column: "ParentId",
                principalTable: "TenantGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE public.\"TenantGroups\" SET \"ParentId\" = group1.\"Id\" from(select \"Id\" from public.\"TenantGroups\"" +
                     "where \"ParentId\" is null) as group1 where public.\"TenantGroups\".\"Id\" = group1.\"Id\"");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantGroups_TenantGroups_ParentId",
                table: "TenantGroups");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "TenantGroups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantGroups_TenantGroups_ParentId",
                table: "TenantGroups",
                column: "ParentId",
                principalTable: "TenantGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
