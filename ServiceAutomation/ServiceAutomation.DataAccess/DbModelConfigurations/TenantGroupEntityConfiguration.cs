using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class TenantGroupEntityConfiguration: EntityConfiguration<TenantGroupEntity>
    {
        public override void Configure(EntityTypeBuilder<TenantGroupEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(TenantGroupEntityDBConstants.TableName);
            builder.HasOne(x => x.Parent)
                .WithMany(x => x.ChildGroups)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);

            builder.HasOne(x => x.OwnerUser)
                .WithOne(x => x.Group)
                .HasForeignKey<TenantGroupEntity>(x => x.OwnerUserId)
                .IsRequired();
        }
    }

    internal class TenantGroupEntityDBConstants
    {
        internal const string TableName = "TenantGroups";
    }
}
