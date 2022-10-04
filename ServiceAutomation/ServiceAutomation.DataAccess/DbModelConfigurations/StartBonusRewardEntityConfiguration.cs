using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class StartBonusRewardEntityConfiguration : EntityConfiguration<StartBonusRewardEntity>
    {
        public override void Configure(EntityTypeBuilder<StartBonusRewardEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(StartBonusRewardEntityDBConstants.TableName);

            builder.HasOne(x => x.Package)
                   .WithMany()
                   .HasForeignKey(x => x.PackageId)
                   .IsRequired();

        }
    }

    internal static class StartBonusRewardEntityDBConstants
    {
        public const string TableName = "StartBonusRewards";
    }
}


