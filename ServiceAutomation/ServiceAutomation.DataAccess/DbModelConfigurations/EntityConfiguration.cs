using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
                                            where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.Id).IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName(EntityDBConstants.Id);
            ConfigureKey(builder);
        }

        protected virtual void ConfigureKey(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }

    #region [DB]

    internal static class EntityDBConstants
    {
        internal const string Id = nameof(Entity.Id);
    }

    #endregion
}
