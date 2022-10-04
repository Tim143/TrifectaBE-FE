using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class TravelBonusRequirementEntityConfiguration: EntityConfiguration<TravelBonusRequirementEntity>
    {
        public override void Configure(EntityTypeBuilder<TravelBonusRequirementEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(TravelBonusRequirementEntityDBConstants.TableName);

            builder.HasOne(x => x.Package)
                   .WithMany()
                   .HasForeignKey(x => x.PackageId)
                   .IsRequired();
        }
    }

    internal static class TravelBonusRequirementEntityDBConstants
    {
        public const string TableName = "TravelBonusRequirements";
    }
}
