using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAutomation.DataAccess.Models.EntityModels;


namespace ServiceAutomation.DataAccess.DbModelConfigurations
{
    internal class ProfilePhotoEntityConfiguration: EntityConfiguration<ProfilePhotoEntity>
    {
        public override void Configure(EntityTypeBuilder<ProfilePhotoEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(ProfilePhotoEntityDBConstants.TableName);
            builder.HasOne(x => x.User)
                .WithOne(x => x.ProfilePhoto)
                .HasForeignKey<ProfilePhotoEntity>(x => x.UserId)
                .IsRequired();
        }
    }

    internal static class ProfilePhotoEntityDBConstants
    {
        public const string TableName = "ProfilePhotos";
    }
}
