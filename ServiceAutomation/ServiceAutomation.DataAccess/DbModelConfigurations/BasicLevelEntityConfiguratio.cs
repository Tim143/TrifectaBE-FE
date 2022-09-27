using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class BasicLevelEntityConfiguration : LevelEntityConfiguration<BasicLevelEntity>
    {
        public override void Configure(EntityTypeBuilder<BasicLevelEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(BasicLevelDBConstants.TableName);

            builder.HasOne(x => x.PartnersLevel)
                .WithMany()
                .HasForeignKey(x => x.PartnersLevelId)
                .IsRequired(false);
        }
    }

    internal class BasicLevelDBConstants
    {
        public const string TableName = "BasicLevels";
    }
}
