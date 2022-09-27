using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class CredentialEntityConfiguration: EntityConfiguration<CredentialEntity>
    {
        public override void Configure(EntityTypeBuilder<CredentialEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(CredentialEntityDBConstants.TableName);
            builder.HasOne(x => x.User)
                .WithOne(x => x.Credential)
                .HasForeignKey<CredentialEntity>(x => x.UserId)
                .IsRequired();
        }
    }

    internal static class CredentialEntityDBConstants
    {
        internal const string TableName = "Credentials";
    }
}
