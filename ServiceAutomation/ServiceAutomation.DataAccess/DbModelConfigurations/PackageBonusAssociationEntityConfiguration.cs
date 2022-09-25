using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class PackageBonusAssociationEntityConfiguration : EntityConfiguration<PackageBonusAssociationEntity>
    {
        public override void Configure(EntityTypeBuilder<PackageBonusAssociationEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(PackageBonusAssociationEntityDbConstants.TableName);

            builder.HasOne(x => x.Package)
                .WithMany(x => x.PackageBonuses)
                .HasForeignKey(x => x.PackageId)
                .IsRequired();

            builder.HasOne(x => x.Bonus)
                .WithMany(x => x.PackageBonuses)
                .HasForeignKey(x => x.BonusId)
                .IsRequired();
        }

        protected override void ConfigureKey(EntityTypeBuilder<PackageBonusAssociationEntity> builder)
        {
            builder.HasKey(x => new { x.PackageId, x.BonusId });
        }
    }

    internal static class PackageBonusAssociationEntityDbConstants{
        internal const string TableName = "Package:Bonuse";
    }
}
