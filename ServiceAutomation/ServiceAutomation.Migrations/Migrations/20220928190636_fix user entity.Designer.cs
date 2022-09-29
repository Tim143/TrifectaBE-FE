﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ServiceAutomation.DataAccess.DbContexts;

namespace ServiceAutomation.DataAccess.Migrations.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220928190636_fix user entity")]
    partial class fixuserentity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.AccrualsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("AccuralAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("AccuralDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("AccuralName")
                        .HasColumnType("text");

                    b.Property<int>("AccuralPercent")
                        .HasColumnType("integer");

                    b.Property<decimal>("InitialAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("ReferralName")
                        .HasColumnType("text");

                    b.Property<int>("TransactionStatus")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Accruals");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.BasicLevelEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int?>("PartnersCount")
                        .HasColumnType("integer");

                    b.Property<Guid?>("PartnersLevelId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Turnover")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("PartnersLevelId");

                    b.ToTable("BasicLevels");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.BonusEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Bonuses");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.CredentialEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("IBAN")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.IndividualUserOrganizationDataEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccountantName")
                        .HasColumnType("text");

                    b.Property<string>("BankHouseNumber")
                        .HasColumnType("text");

                    b.Property<string>("BankLocality")
                        .HasColumnType("text");

                    b.Property<string>("BankRegion")
                        .HasColumnType("text");

                    b.Property<string>("BankStreet")
                        .HasColumnType("text");

                    b.Property<string>("BaseOrganization")
                        .HasColumnType("text");

                    b.Property<string>("BeneficiaryBankName")
                        .HasColumnType("text");

                    b.Property<string>("CertificateDateIssue")
                        .HasColumnType("text");

                    b.Property<string>("CertificateNumber")
                        .HasColumnType("text");

                    b.Property<string>("CheckingAccount")
                        .HasColumnType("text");

                    b.Property<string>("HeadFullName")
                        .HasColumnType("text");

                    b.Property<string>("HeadPosition")
                        .HasColumnType("text");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("text");

                    b.Property<string>("Index")
                        .HasColumnType("text");

                    b.Property<string>("LegalEntityAbbreviatedName")
                        .HasColumnType("text");

                    b.Property<string>("LegalEntityFullName")
                        .HasColumnType("text");

                    b.Property<string>("Locality")
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("Region")
                        .HasColumnType("text");

                    b.Property<string>("RegistrationAuthority")
                        .HasColumnType("text");

                    b.Property<string>("RoomNumber")
                        .HasColumnType("text");

                    b.Property<string>("SWIFT")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.Property<string>("UNP")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("IndividualUserOrganizationsData");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.LegalUserOrganizationDataEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BankHouseNumber")
                        .HasColumnType("text");

                    b.Property<string>("BankStreet")
                        .HasColumnType("text");

                    b.Property<string>("BeneficiaryBankName")
                        .HasColumnType("text");

                    b.Property<string>("CheckingAccount")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Disctrict")
                        .HasColumnType("text");

                    b.Property<string>("Flat")
                        .HasColumnType("text");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("text");

                    b.Property<string>("Index")
                        .HasColumnType("text");

                    b.Property<string>("Locality")
                        .HasColumnType("text");

                    b.Property<string>("Region")
                        .HasColumnType("text");

                    b.Property<string>("SWIFT")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("LegalUserOrganizationsData");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.MonthlyLevelEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal?>("Turnover")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("MonthlyLevels");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.PackageBonusAssociationEntity", b =>
                {
                    b.Property<Guid>("PackageId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BonusId")
                        .HasColumnType("uuid");

                    b.Property<int>("FromLevel")
                        .HasColumnType("integer");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<int?>("PayablePercent")
                        .HasColumnType("integer");

                    b.HasKey("PackageId", "BonusId");

                    b.HasIndex("BonusId");

                    b.ToTable("Package:Bonuse");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.PackageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.PartnerPurchaseEntity", b =>
                {
                    b.Property<decimal?>("PurchasePrice")
                        .HasColumnType("numeric")
                        .HasColumnName("purchaseprice");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.ToTable("PartnerPurchase");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.ProfilePhotoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("FullPath")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ProfilePhotos");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.PurchaseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid>("PackageId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.HasIndex("UserId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.RefreshTokenEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.TenantGroupEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OwnerUserId")
                        .IsUnique();

                    b.HasIndex("ParentId");

                    b.ToTable("TenantGroups");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.ThumbnailTemplateEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ThumbnailFullPath")
                        .HasColumnType("text");

                    b.Property<string>("ThumbnailName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Thumbnails");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.UserAccountOrganizationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("TypeOfEmployment")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("UserAccountOrganizations");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid?>("BasicLevelId")
                        .HasColumnType("uuid");

                    b.Property<int>("Country")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("InviteReferral")
                        .HasColumnType("text");

                    b.Property<bool>("IsVerifiedUser")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<string>("PersonalReferral")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserAccountOrganizationId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserPhoneNumberId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BasicLevelId");

                    b.HasIndex("UserAccountOrganizationId");

                    b.HasIndex("UserPhoneNumberId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.UserLevelsInfoEntity", b =>
                {
                    b.Property<Guid>("BasicLevelId")
                        .HasColumnType("uuid");

                    b.Property<int>("BranchCount")
                        .HasColumnType("integer");

                    b.HasIndex("BasicLevelId");

                    b.ToTable("UserLevelsInfos");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.UserPhoneNumberEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserPhones");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.UserProfileInfoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserContacts");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.VideoLessonTemplateEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("VideoFullPath")
                        .HasColumnType("text");

                    b.Property<string>("VideoName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("VideoLessons");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.WithdrawTransactionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid>("CredentialId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("TransactionStatus")
                        .HasColumnType("integer");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CredentialId");

                    b.ToTable("WithdrawTransactions");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.AccrualsEntity", b =>
                {
                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.UserEntity", "User")
                        .WithMany("UserAccruals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.BasicLevelEntity", b =>
                {
                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.BasicLevelEntity", "PartnersLevel")
                        .WithMany()
                        .HasForeignKey("PartnersLevelId");

                    b.Navigation("PartnersLevel");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.CredentialEntity", b =>
                {
                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.UserEntity", "User")
                        .WithOne("Credential")
                        .HasForeignKey("ServiceAutomation.DataAccess.Models.EntityModels.CredentialEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.PackageBonusAssociationEntity", b =>
                {
                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.BonusEntity", "Bonus")
                        .WithMany("PackageBonuses")
                        .HasForeignKey("BonusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.PackageEntity", "Package")
                        .WithMany("PackageBonuses")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bonus");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.ProfilePhotoEntity", b =>
                {
                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.UserEntity", "User")
                        .WithOne("ProfilePhoto")
                        .HasForeignKey("ServiceAutomation.DataAccess.Models.EntityModels.ProfilePhotoEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.PurchaseEntity", b =>
                {
                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.PackageEntity", "Package")
                        .WithMany("UsersPurchases")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.UserEntity", "User")
                        .WithMany("UserPurchases")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Package");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.TenantGroupEntity", b =>
                {
                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.UserEntity", "OwnerUser")
                        .WithOne("Group")
                        .HasForeignKey("ServiceAutomation.DataAccess.Models.EntityModels.TenantGroupEntity", "OwnerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.TenantGroupEntity", "Parent")
                        .WithMany("ChildGroups")
                        .HasForeignKey("ParentId");

                    b.Navigation("OwnerUser");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.UserEntity", b =>
                {
                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.BasicLevelEntity", "BasicLevel")
                        .WithMany()
                        .HasForeignKey("BasicLevelId");

                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.UserAccountOrganizationEntity", "UserAccountOrganization")
                        .WithMany()
                        .HasForeignKey("UserAccountOrganizationId");

                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.UserPhoneNumberEntity", "UserPhoneNumber")
                        .WithMany()
                        .HasForeignKey("UserPhoneNumberId");

                    b.Navigation("BasicLevel");

                    b.Navigation("UserAccountOrganization");

                    b.Navigation("UserPhoneNumber");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.UserLevelsInfoEntity", b =>
                {
                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.BasicLevelEntity", "BasicLevel")
                        .WithMany()
                        .HasForeignKey("BasicLevelId")
                        .IsRequired();

                    b.Navigation("BasicLevel");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.UserProfileInfoEntity", b =>
                {
                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.UserEntity", "User")
                        .WithOne("UserContact")
                        .HasForeignKey("ServiceAutomation.DataAccess.Models.EntityModels.UserProfileInfoEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.WithdrawTransactionEntity", b =>
                {
                    b.HasOne("ServiceAutomation.DataAccess.Models.EntityModels.CredentialEntity", "Credential")
                        .WithMany("WithdrawTransactions")
                        .HasForeignKey("CredentialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Credential");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.BonusEntity", b =>
                {
                    b.Navigation("PackageBonuses");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.CredentialEntity", b =>
                {
                    b.Navigation("WithdrawTransactions");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.PackageEntity", b =>
                {
                    b.Navigation("PackageBonuses");

                    b.Navigation("UsersPurchases");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.TenantGroupEntity", b =>
                {
                    b.Navigation("ChildGroups");
                });

            modelBuilder.Entity("ServiceAutomation.DataAccess.Models.EntityModels.UserEntity", b =>
                {
                    b.Navigation("Credential");

                    b.Navigation("Group");

                    b.Navigation("ProfilePhoto");

                    b.Navigation("UserAccruals");

                    b.Navigation("UserContact");

                    b.Navigation("UserPurchases");
                });
#pragma warning restore 612, 618
        }
    }
}
