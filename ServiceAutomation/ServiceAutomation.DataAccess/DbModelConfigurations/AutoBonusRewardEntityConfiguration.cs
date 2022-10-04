using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class AutoBonusRewardEntityConfiguration: EntityConfiguration<AutoBonusRewardEntity>
    {
        public override void Configure(EntityTypeBuilder<AutoBonusRewardEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(AutoBonusRewardEntityDBConstants.TableName);

            builder.HasOne(x => x.Package)
                          .WithMany()
                          .HasForeignKey(x => x.PackageId)
                          .IsRequired();

            builder.HasOne(x => x.BasicLevel)
                   .WithMany()
                   .HasForeignKey(x => x.BasicLevelId)
                   .IsRequired();
        }
    }

    internal static class AutoBonusRewardEntityDBConstants
    {
        public const string TableName = "AutoBonusRewards";
    }
}

