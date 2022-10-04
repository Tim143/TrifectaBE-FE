using Microsoft.EntityFrameworkCore.Migrations;
using ServiceAutomation.DataAccess.DataSeeding;
using System.Linq;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    public partial class AddStartBonus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //var startBonus = BonusesInitialData.BonusSeeds.First(x => x.Type == Schemas.Enums.BonusType.StartBonus);
            //migrationBuilder.Sql($"insert into public.\"Bonuses\" (\"Id\", \"Name\", \"Type\") values ('{startBonus.Id}', '{startBonus.Name}', {(int)startBonus.Type})");

            //var bonuses = BonusesInitialData.BonusSeeds;
            //foreach (var bonus in bonuses)
            //{
            //    migrationBuilder.Sql($"UPDATE public.\"Bonuses\" SET \"DisplayOrder\" = {bonus.DisplayOrder} WHERE \"Id\" = '{bonus.Id}'; ");
            //}

            //var associations = PackagesInitialData.PackageBonusAssociationSeeds.Where( a => a.BonusId == startBonus.Id);
            //foreach (var association in associations)
            //{
            //    migrationBuilder.Sql($"insert into public.\"Package:Bonuse\" (\"Id\", \"PackageId\", \"BonusId\", \"FromLevel\", \"PayablePercent\") values ('{association.Id}', '{association.PackageId}', '{association.BonusId}', {association.FromLevel}, { (association.PayablePercent.HasValue ? association.PayablePercent.Value : "Null")})");
            //}
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM public.\"Bonuses\" WHERE \"Id\" = '{BonusesInitialData.BonusesIdsByType[Schemas.Enums.BonusType.StartBonus]}';");
        }
    }
}
