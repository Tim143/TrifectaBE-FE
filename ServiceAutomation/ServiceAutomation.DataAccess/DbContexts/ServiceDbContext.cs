using Microsoft.EntityFrameworkCore;
using ServiceAutomation.DataAccess.DbModelConfigurations;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.DataAccess.DbContexts
{
    public abstract class ServiceDbContext : DbContext
    {
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<RefreshTokenEntity> RefresTokens { get; set; }
        public virtual DbSet<ThumbnailTemplateEntity> Thumbnails { get; set; }
        public virtual DbSet<CredentialEntity> Credentials { get; set; }
        public virtual DbSet<WithdrawTransactionEntity> WithdrawTransactions { get; set; }
        public virtual DbSet<UserProfileInfoEntity> UserContacts { get; set; }
        public virtual DbSet<TenantGroupEntity> TenantGroups { get; set; }
        public virtual DbSet<VideoLessonTemplateEntity> VideoLessons { get; set; }
        public virtual DbSet<ProfilePhotoEntity> ProfilePhotos { get; set; }
        public virtual DbSet<PackageEntity> Packages { get; set; }
        public virtual DbSet<PurchaseEntity> UsersPurchases { get; set; }
        public virtual DbSet<BasicLevelEntity> BasicLevels { get; set; }
        public virtual DbSet<MonthlyLevelEntity> MonthlyLevels { get; set; }
        public virtual DbSet<UserAccountOrganizationEntity> UserAccountOrganizations { get; set; }
        public virtual DbSet<LegalUserOrganizationDataEntity> LegalUserOrganizationsData { get; set; }
        public virtual DbSet<IndividualUserOrganizationDataEntity> IndividualUserOrganizationsData { get; set; }
        public virtual DbSet<IndividualEntrepreneurUserOrganizationDataEntity> IndividualEntrepreneurUserOrganizationsData { get; set; }
        public virtual DbSet<AccrualsEntity> Accruals { get; set; }
        public virtual DbSet<PartnerPurchaseEntity> PartnerPurchase { get; set; }
        public virtual DbSet<UserLevelsInfoEntity> UserLevelsInfos { get; set; }
        public virtual DbSet<UserContactVerificationEntity> UserContactVerifications { get; set; }
        public virtual DbSet<MonthlyLevelStatisticEntity> MonthlyLevelStatistics { get; set; }
        public virtual DbSet<BasicLevelStatisticEntity> BasicLevelStatistics { get; set; }
        public virtual DbSet<LevelBonusRewardEntity> LevelBonusRewards { get; set; }
        public virtual DbSet<LevelBonusRewardPercentEntity> LevelBonusRewardPercents { get; set; }
        public virtual DbSet<BonusEntity> Bonuses { get; set; }
        public virtual DbSet<StartBonusRewardEntity> StartBonusRewards { get; set; }
        public virtual DbSet<DynamicBonusRewardEntity> DynamicBonusRewards { get; set; }
        public virtual DbSet<UserAccuralsVerificationEntity> UserAccuralsVerifications { get; set; }
        public virtual DbSet<AutoBonusRewardEntity> AutoBonusRewards { get; set; }
        public virtual DbSet<TravelBonusRequirementEntity> TravelBonusRequirements { get; set; }
        public virtual DbSet<TeamBonusRewardEntity> TeamBonusRewards { get; set; }
        public virtual DbSet<UserVerificationPhotoEntity> UserVerificationPhotos { get; set; }
        public virtual DbSet<CashPurchaseEntity> CashPurchases { get; set; }

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
            modelBuilder.ApplyConfiguration(new PurchaseEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BasicLevelEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MonthlyLevelEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PartnerPurchaseEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserLevelsInfoEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MonthlyLevelStatisticEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BasicLevelStatisticEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LevelBonusRewardEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LevelBonusRewardPercentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AccrualsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StartBonusRewardEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicBonusRewardEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AutoBonusRewardEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TravelBonusRequirementEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TeamBonusRewardEntityConfiguration());
        }
    }
}
