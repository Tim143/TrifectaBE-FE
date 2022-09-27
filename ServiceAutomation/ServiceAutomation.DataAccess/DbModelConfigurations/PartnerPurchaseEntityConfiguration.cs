using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    class PartnerPurchaseEntityConfiguration : IEntityTypeConfiguration<PartnerPurchaseEntity>
    {
        public void Configure(EntityTypeBuilder<PartnerPurchaseEntity> builder)
        {
            builder.HasNoKey();
            builder.Property(x => x.PurchasePrice)
                   .HasColumnName("purchaseprice");
        }
    }
}
