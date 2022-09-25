using System;
using Microsoft.EntityFrameworkCore.Migrations;
using ServiceAutomation.DataAccess.DataSeeding;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class AddPackagesAndBonusesEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            CreateTables(migrationBuilder);
            FillTables(migrationBuilder);
        }

        #region TableCreation

        private void CreateTables(MigrationBuilder migrationBuilder)
        {
            CreatePackagesTable(migrationBuilder);
            CreateBonusesTable(migrationBuilder);
            CraetePackageBonusAssociationTable(migrationBuilder);
        }

        private void CreatePackagesTable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Packages",
               columns: table => new
               {
                   Id = table.Column<Guid>(type: "uuid", nullable: false),
                   Type = table.Column<int>(type: "integer", nullable: false),
                   Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                   Price = table.Column<decimal>(type: "numeric", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Packages", x => x.Id);
               });
        }

        private void CreateBonusesTable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bonuses",
                columns: table => new
                    {
                        Id = table.Column<Guid>(type: "uuid", nullable: false),
                        Type = table.Column<int>(type: "integer", nullable: false),
                        Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                    },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_Bonuses", x => x.Id);
                    });
        }

        private void CraetePackageBonusAssociationTable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Package:Bonuse",
               columns: table => new
               {
                   PackageId = table.Column<Guid>(type: "uuid", nullable: false),
                   BonusId = table.Column<Guid>(type: "uuid", nullable: false),
                   FromLevel = table.Column<int>(type: "integer", nullable: false),
                   PayablePercent = table.Column<int>(type: "integer", nullable: true),
                   Id = table.Column<Guid>(type: "uuid", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Package:Bonuse", x => new { x.PackageId, x.BonusId });
                   table.ForeignKey(
                       name: "FK_Package:Bonuse_Bonuses_BonusId",
                       column: x => x.BonusId,
                       principalTable: "Bonuses",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_Package:Bonuse_Packages_PackageId",
                       column: x => x.PackageId,
                       principalTable: "Packages",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateIndex(
                name: "IX_Package:Bonuse_BonusId",
                table: "Package:Bonuse",
                column: "BonusId");
        }

        #endregion

        #region TableFilling
        private void FillTables(MigrationBuilder migrationBuilder)
        {
            FillBonusesTable(migrationBuilder);
            FillPackesTableWithAssociations(migrationBuilder);
        }

        private void FillBonusesTable(MigrationBuilder migrationBuilder)
        {
            var bonuses = BonusesInitialData.BonusSeeds;
            foreach (var bonus in bonuses)
            {
                migrationBuilder.Sql($"insert into public.\"Bonuses\" (\"Id\", \"Name\", \"Type\") values ('{bonus.Id}', '{bonus.Name}', {(int)bonus.Type})");
            }
        }

        private void FillPackesTableWithAssociations(MigrationBuilder migrationBuilder)
        {
            var packages = PackagesInitialData.PackageSeeds;
            foreach (var package in packages)
            {
                migrationBuilder.Sql($"insert into public.\"Packages\" (\"Id\", \"Name\", \"Type\", \"Price\") values ('{package.Id}', '{package.Name}', {(int)package.Type}, {package.Price})");
            }

            var associations = PackagesInitialData.PackageBonusAssociationSeeds;
            foreach(var association in associations)
            {
                migrationBuilder.Sql($"insert into public.\"Package:Bonuse\" (\"Id\", \"PackageId\", \"BonusId\", \"FromLevel\", \"PayablePercent\") values ('{association.Id}', '{association.PackageId}', '{association.BonusId}', {association.FromLevel}, { (association.PayablePercent.HasValue ? association.PayablePercent.Value : "Null")})");
            }
        }

        #endregion

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Package:Bonuse");

            migrationBuilder.DropTable(
                name: "Bonuses");

            migrationBuilder.DropTable(
                name: "Packages");
        }
    }
}
