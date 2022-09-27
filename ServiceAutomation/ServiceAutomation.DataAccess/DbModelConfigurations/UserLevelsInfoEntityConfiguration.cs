using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class UserLevelsInfoEntityConfiguration : IEntityTypeConfiguration<UserLevelsInfoEntity>
    {
        public void Configure(EntityTypeBuilder<UserLevelsInfoEntity> builder)
        {
            builder.HasNoKey();

            builder.HasOne(x => x.BasicLevel)
                   .WithMany()
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasForeignKey(x => x.BasicLevelId);
        }
    }
}
