using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class fixfintech : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WithdrawTransactions_Credentials_CredentialId",
                table: "WithdrawTransactions");

            migrationBuilder.DropIndex(
                name: "IX_WithdrawTransactions_CredentialId",
                table: "WithdrawTransactions");

            migrationBuilder.DropColumn(
                name: "CredentialId",
                table: "WithdrawTransactions");

            migrationBuilder.AddColumn<string>(
                name: "CheckingAccount",
                table: "WithdrawTransactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CredentialEntityId",
                table: "WithdrawTransactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawTransactions_CredentialEntityId",
                table: "WithdrawTransactions",
                column: "CredentialEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_WithdrawTransactions_Credentials_CredentialEntityId",
                table: "WithdrawTransactions",
                column: "CredentialEntityId",
                principalTable: "Credentials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WithdrawTransactions_Credentials_CredentialEntityId",
                table: "WithdrawTransactions");

            migrationBuilder.DropIndex(
                name: "IX_WithdrawTransactions_CredentialEntityId",
                table: "WithdrawTransactions");

            migrationBuilder.DropColumn(
                name: "CheckingAccount",
                table: "WithdrawTransactions");

            migrationBuilder.DropColumn(
                name: "CredentialEntityId",
                table: "WithdrawTransactions");

            migrationBuilder.AddColumn<Guid>(
                name: "CredentialId",
                table: "WithdrawTransactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawTransactions_CredentialId",
                table: "WithdrawTransactions",
                column: "CredentialId");

            migrationBuilder.AddForeignKey(
                name: "FK_WithdrawTransactions_Credentials_CredentialId",
                table: "WithdrawTransactions",
                column: "CredentialId",
                principalTable: "Credentials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
