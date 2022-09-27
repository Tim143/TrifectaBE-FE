using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class PurchaseEntityConfiguration: EntityConfiguration<PurchaseEntity>
    {
        public override void Configure(EntityTypeBuilder<PurchaseEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(PurchaseEntityDBConstants.TableName);

            builder.HasOne(x => x.Package)
                .WithMany(x => x.UsersPurchases)
                .HasForeignKey(x => x.PackageId)
                .IsRequired();

             builder.HasOne(x => x.User)
                .WithMany(x => x.UserPurchases)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.PurchaseDate)
                .IsRequired();
        }
    }

    internal static class PurchaseEntityDBConstants
    {
        public const string TableName = "Purchases";
    }
}
