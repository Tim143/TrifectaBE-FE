using Microsoft.EntityFrameworkCore;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class DynamicBonusRewardEntityConfiguration : EntityConfiguration<DynamicBonusRewardEntity>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DynamicBonusRewardEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(DynamicBonusRewardEntityDBConstant.TableName);
            builder.HasOne(x => x.Package)
                   .WithMany()
                   .HasForeignKey(x => x.PackageId)
                   .IsRequired();
        }
    }

    internal static class DynamicBonusRewardEntityDBConstant
    {
        public const string TableName = "DynamicBonusRewards";
    }
}
