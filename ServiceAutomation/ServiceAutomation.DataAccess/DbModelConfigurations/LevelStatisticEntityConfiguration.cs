using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal abstract class LevelStatisticEntityConfiguration<TStatistic> : EntityConfiguration<TStatistic>
        where TStatistic: LevelStatisticEntity
    {
        public override void Configure(EntityTypeBuilder<TStatistic> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.ReachingLevelDate)
                   .IsRequired();

            builder.Property(x => x.Turnover)
                   .IsRequired(false);
        }
    }
}
