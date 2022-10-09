using AutoMapper;
using ServiceAutomation.Canvas.WebApi.Extensions;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.Canvas.WebApi.Models.AdministratorResponseModels;
using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.Common.Models;
using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Models.Enums;
using System.Linq;

namespace ServiceAutomation.Canvas.AutoMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserModel>()
                .ForMember(x => x.InviteCode, opt => opt.MapFrom(x => x.InviteReferral));

            CreateMap<RegisterRequestModel, UserModel>()
                .ForMember(x => x.InviteCode, opt => opt.MapFrom(x => x.ReferralCode));

            CreateMap<RefreshTokenEntity, RefreshToken>();
            CreateMap<ThumbnailTemplateEntity, ThumbnailResponseModel>();

            CreateMap<WithdrawTransactionEntity, WithdrawResponseModel>()
                .ForMember(x => x.CardCode, opt => opt.MapFrom(x => x.CheckingAccount))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.TransactionStatus.ToString()))
                .ForMember(x => x.Amount, opt => opt.MapFrom(x => x.Value))
                .ForMember(x => x.DateTime, opt => opt.MapFrom(x => x.Date));

            CreateMap<VideoLessonTemplateEntity, VideoLessonResponseModel>();

            CreateMap<UserEntity, UserProfileResponseModel>()
                .ForMember(x => x.Patronymic, opt => opt.MapFrom(x => x.Patronymic))
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(x => x.UserContact.DateOfBirth))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.ProfilePhoto, opt => opt.MapFrom(x => x.ProfilePhoto.FullPath))
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.PhoneNumber))
                .ForMember(x => x.Level, opt => opt.MapFrom(x => x.BasicLevel.Name))
                .ForMember(x => x.PersonalReferral, opt => opt.MapFrom(x => x.PersonalReferral));


            CreateMap<PackageBonusAssociationEntity, BonusModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Bonus.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.GetPackageBonusTitle()));

            CreateMap<PackageEntity, PackageModel>()
                .ForMember(x => x.Bonuses, opt => opt.MapFrom(x => x.PackageBonuses.OrderBy(pb => pb.Bonus.DisplayOrder)));

            CreateMap<PackageEntity, UserPackageModel>()
                .ForMember(x => x.Bonuses, opt => opt.MapFrom(x => x.PackageBonuses.OrderBy(pb => pb.Bonus.DisplayOrder)));

            CreateMap<IndividualUserOrganizationDataEntity, IndividualEntityDataResponseModel>();
            CreateMap<IndividualEntrepreneurUserOrganizationDataEntity, IndividualEntrepreneurEntityDataResponseModel>();
            CreateMap<LegalUserOrganizationDataEntity, LegalEntityDataResponseModel>();

            CreateMap<AccrualsEntity, AccuralResponseModel>()
                .ForMember(x => x.AccuralName, opt => opt.MapFrom(x => $"Начисление {x.Bonus.Name}"))
                .ForMember(x => x.ReferralName, opt => opt.MapFrom(x => x.ForWhom.Email))
                .ForMember(x => x.AccuralPercent, opt => opt.MapFrom(x => x.AccuralPercent))
                .ForMember(x => x.InitialAmount, opt => opt.MapFrom(x => x.InitialAmount))
                .ForMember(x => x.AccuralAmount, opt => opt.MapFrom(x => x.AccuralAmount))
                .ForMember(x => x.AccuralDate, opt => opt.MapFrom(x => x.AccuralDate))
                .ForMember(x => x.TransactionStatus, opt => opt.MapFrom(x => x.TransactionStatus.ToString()));

            CreateMap<LevelEntity, LevelModel>()
                .ForMember(x => x.Level, opt => opt.MapFrom(x => (int)x.Level));

            CreateMap<IndividualUserOrganizationDataEntity, UserVerificationResponseModel>()
                .ForMember(x => x.RequestId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.UserId))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.LegalEntityFullName))
                .ForMember(x => x.SWIFT, opt => opt.MapFrom(x => x.SWIFT))
                .ForMember(x => x.CheckingAccount, opt => opt.MapFrom(x => x.CheckingAccount))
                .ForMember(x => x.BaseOrganization, opt => opt.MapFrom(x => x.BaseOrganization))
                .ForMember(x => x.UNP, opt => opt.MapFrom(x => x.UNP))
                .ForMember(x => x.RegistrationAuthority, opt => opt.MapFrom(x => x.RegistrationAuthority))
                .ForMember(x => x.VerivicationPhoto, opt => opt.MapFrom(x => x.VerificationPhotoPath))
                .ForMember(x => x.CertificateNumber, opt => opt.MapFrom(x => x.CertificateNumber));

            CreateMap<IndividualEntrepreneurUserOrganizationDataEntity, UserVerificationResponseModel>()
                .ForMember(x => x.RequestId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.UserId))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.LegalEntityFullName))
                .ForMember(x => x.SWIFT, opt => opt.MapFrom(x => x.SWIFT))
                .ForMember(x => x.CheckingAccount, opt => opt.MapFrom(x => x.CheckingAccount))
                .ForMember(x => x.BaseOrganization, opt => opt.MapFrom(x => x.BaseOrganization))
                .ForMember(x => x.UNP, opt => opt.MapFrom(x => x.UNP))
                .ForMember(x => x.RegistrationAuthority, opt => opt.MapFrom(x => x.RegistrationAuthority))
                .ForMember(x => x.VerivicationPhoto, opt => opt.MapFrom(x => x.VerificationPhotoPath))
                .ForMember(x => x.CertificateNumber, opt => opt.MapFrom(x => x.CertificateNumber));

            CreateMap<LegalUserOrganizationDataEntity, UserVerificationResponseModel>()
                .ForMember(x => x.RequestId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.UserId))
               .ForMember(x => x.SWIFT, opt => opt.MapFrom(x => x.SWIFT))
                .ForMember(x => x.Number, opt => opt.MapFrom(x => x.Number))
                .ForMember(x => x.IdentityNumber, opt => opt.MapFrom(x => x.IdentityNumber))
               .ForMember(x => x.RegistrationAuthority, opt => opt.MapFrom(x => x.RegistrationAuthority))
               .ForMember(x => x.CertificateDateIssue, opt => opt.MapFrom(x => x.CertificateDateIssue));

            CreateMap<UserContactVerificationEntity, UserContactsVerificationResponseModel>()
                .ForMember(x => x.RequestId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.UserId))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.User.LastName))
                .ForMember(x => x.EmailAddress, opt => opt.MapFrom(x => x.User.Email))
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.User.PhoneNumber))
                .ForMember(x => x.ContactVerificationType, opt => opt.MapFrom(x => x.VerificationType.ToString()))
                .ForMember(x => x.NewData, opt => opt.MapFrom(x => x.NewData));

            CreateMap<UserAccuralsVerificationEntity, WithdrawVerifictionResponseModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.User.FirstName))
                .ForMember(x => x.Surname, opt => opt.MapFrom(x => x.User.LastName))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.User.Email))
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.User.PhoneNumber));
        }
    }
}
