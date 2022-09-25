using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class BonusEntityConfiguration: EntityConfiguration<BonusEntity>
    {
        public override void Configure(EntityTypeBuilder<BonusEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(BonusEntityDBConstants.TableName);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }

    internal static class BonusEntityDBConstants
    {
        public const string TableName = "Bonuses";
    }
}
