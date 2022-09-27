using Microsoft.EntityFrameworkCore;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class MonthlyLevelEntityConfiguration: LevelEntityConfiguration<MonthlyLevelEntity>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MonthlyLevelEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(MonthlyLevelEntityDBConstants.TableName);
        }
    }

    internal class MonthlyLevelEntityDBConstants
    {
        public const string TableName = "MonthlyLevels";
    }

}
