using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class MonthlyLevelStatisticEntityConfiguration : LevelStatisticEntityConfiguration<MonthlyLevelStatisticEntity>
    {
        public override void Configure(EntityTypeBuilder<MonthlyLevelStatisticEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(MonthlyLevelStatisticEntityDbConstants.TableName);

            builder.HasOne(x => x.Level)
                   .WithMany()
                    .HasForeignKey(x => x.LevelId)
                    .IsRequired();
        }
    }

    internal static class MonthlyLevelStatisticEntityDbConstants
    {
        internal const string TableName = "MonthlyLevelStatistics";
    }
}
