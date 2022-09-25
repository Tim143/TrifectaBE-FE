using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class PackageEntityConfiguration: EntityConfiguration<PackageEntity>
    {
        public override void Configure(EntityTypeBuilder<PackageEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(PackageEntityDBConstants.TableName);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
        }        
    }

    internal static class PackageEntityDBConstants
    {
        public const string TableName = "Packages";
    }
}
