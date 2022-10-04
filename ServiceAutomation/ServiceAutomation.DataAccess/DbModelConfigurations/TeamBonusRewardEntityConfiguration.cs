using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class TeamBonusRewardEntityConfiguration: EntityConfiguration<TeamBonusRewardEntity>
    {
        public override void Configure(EntityTypeBuilder<TeamBonusRewardEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(TeamBonusRewardEntityDBConstants.TableName);

            builder.HasOne(x => x.MonthlyLevel)
                    .WithMany()
                    .HasForeignKey(x => x.MonthlyLevelId)
                    .IsRequired();
        }
    }

    internal static class TeamBonusRewardEntityDBConstants
    {
        public const string TableName = "TeamBonusRewards";
    }
}
