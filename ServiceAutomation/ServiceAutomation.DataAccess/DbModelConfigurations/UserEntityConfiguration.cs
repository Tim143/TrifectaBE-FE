using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal sealed class UserEntityConfiguration: EntityConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(UserEntityDBConstants.TableName);
        }
    }

    internal static class UserEntityDBConstants
    {
        internal const string TableName = "Users";
    }
}
