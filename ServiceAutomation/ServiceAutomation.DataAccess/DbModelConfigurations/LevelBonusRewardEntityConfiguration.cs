using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class LevelBonusRewardEntityConfiguration: EntityConfiguration<LevelBonusRewardEntity>
    {
        public override void Configure(EntityTypeBuilder<LevelBonusRewardEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(LevelBonusRewardEntityDBConstants.TableName);

            builder.HasOne(x => x.Level)
                   .WithMany()
                   .HasForeignKey(x => x.LevelId)
                   .IsRequired();

            builder.Property(x => x.Reward)
                   .IsRequired();
        }
    }

    internal static class LevelBonusRewardEntityDBConstants
    {
        public const string TableName = "LevelBonusRewards";
    }
}
