using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class RefreshTokenEntityConfiguration: EntityConfiguration<RefreshTokenEntity>
    {
        public override void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(RefreshTokenEntityDBConstants.TableName);
        }
    }

    internal static class RefreshTokenEntityDBConstants
    {
        internal const string TableName = "RefreshTokens";
    }
}
