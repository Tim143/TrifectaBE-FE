using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class AccrualsEntityConfiguration: EntityConfiguration<AccrualsEntity>
    {
        public override void Configure(EntityTypeBuilder<AccrualsEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(AccrualsEntityDBConstants.TableName);

            builder.HasOne(x => x.User)
                  .WithMany()
                  .HasForeignKey(x => x.UserId)
                  .IsRequired();

            builder.HasOne(x => x.ForWhom)
                   .WithMany()
                   .HasForeignKey( x => x.ForWhomId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .IsRequired(false);

            builder.HasOne(x => x.Bonus)
                .WithMany()
                .HasForeignKey(x => x.BonusId)
                .IsRequired();

            builder.HasOne(x => x.ForBsicLevel)
                   .WithMany()
                   .HasForeignKey(x => x.ForBsicLevelId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .IsRequired(false);
        }
    }

    internal static class AccrualsEntityDBConstants
    {
        public const string TableName = "Accruals";
    }
}
