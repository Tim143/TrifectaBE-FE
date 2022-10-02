using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;


namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class BasicLevelStatisticEntityConfiguration : LevelStatisticEntityConfiguration<BasicLevelStatisticEntity>
    {
        public override void Configure(EntityTypeBuilder<BasicLevelStatisticEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(BasicLevelStatisticEntityDbConstants.TableName);

            builder.HasOne(x => x.Level)
                   .WithMany()
                   .HasForeignKey(x => x.LevelId)
                   .IsRequired();
        }
    }

    internal static class BasicLevelStatisticEntityDbConstants
    {
        internal const string TableName = "BasicLevelStatistics";
    }
}
