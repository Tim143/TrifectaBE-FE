using Microsoft.EntityFrameworkCore;
using ServiceAutomation.DataAccess.DbModelConfigurations;
using ServiceAutomation.DataAccess.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAutomation.DataAccess.DbContexts
{
    public abstract class ServiceDbContext : DbContext
    {
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<RefreshTokenEntity> RefresTokens { get; set; }
        public virtual DbSet<ThumbnailTemplateEntity> Thumbnails { get; set; }
        public virtual DbSet<CredentialEntity> Credentials { get; set; }
        public virtual DbSet<WithdrawTransactionEntity> WithdrawTransactions { get; set; }
        public virtual DbSet<UserContactEntity> UserContacts { get; set; }
        public virtual DbSet<TenantGroupEntity> TenantGroups { get; set; }
        public virtual DbSet<VideoLessonTemplateEntity> VideoLessons { get; set; }
        public virtual DbSet<ProfilePhotoEntity> ProfilePhotos { get; set; }
        public virtual DbSet<PackageEntity> Packages { get; set; }

        public ServiceDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CredentialEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProfilePhotoEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TenantGroupEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserContactEntityConfiguration());
            modelBuilder.ApplyConfiguration(new WithdrawTransactionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PackageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BonusEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PackageBonusAssociationEntityConfiguration());
        }
    }
}
