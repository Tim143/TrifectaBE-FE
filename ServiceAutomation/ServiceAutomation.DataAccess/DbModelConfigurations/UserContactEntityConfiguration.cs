using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class UserContactEntityConfiguration: EntityConfiguration<UserProfileInfoEntity>
    {
        public override void Configure(EntityTypeBuilder<UserProfileInfoEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(UserContactEntityDBConstants.TableName);

            builder.HasOne(x => x.User)
                .WithOne(x => x.UserContact)
                .HasForeignKey<UserProfileInfoEntity>(x => x.UserId)
                .IsRequired();
        }

    }
    
    internal static class UserContactEntityDBConstants
    {
        internal const string TableName = "UserContacts";
    }
}
