using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal abstract class LevelEntityConfiguration<TLevelEntity>: EntityConfiguration<TLevelEntity>
        where TLevelEntity : LevelEntity
    {
        public override void Configure(EntityTypeBuilder<TLevelEntity> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .HasMaxLength(50);
        }
    }
}
