using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class WithdrawTransactionEntityConfiguration : EntityConfiguration<WithdrawTransactionEntity>
    {
        public override void Configure(EntityTypeBuilder<WithdrawTransactionEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(WithdrawTransactionEntityDBConstants.TableName);

            builder.HasOne(x => x.Credential)
                .WithMany(x => x.WithdrawTransactions)
                .HasForeignKey(x => x.CredentialId)
                .IsRequired();
        }
    }

    internal static class WithdrawTransactionEntityDBConstants
    {
        internal const string TableName = "WithdrawTransactions";
    }
}
