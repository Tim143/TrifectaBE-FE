using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class LevelBonusRewardPercentEntityConfiguration: EntityConfiguration<LevelBonusRewardPercentEntity>
    {
        public override void Configure(EntityTypeBuilder<LevelBonusRewardPercentEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(LevelBonusRewardPercentEntityDBConstants.TableName);

            builder.HasOne(x => x.Package)
                .WithMany()
                .HasForeignKey(x => x.PackageId)
                .IsRequired();

            builder.Property(x => x.Percent)
                .IsRequired();
        }
    }

    internal static class LevelBonusRewardPercentEntityDBConstants
    {
        public const string TableName = "LevelBonusRewardPercents";
    }
}
