using AutoMapper;
using ServiceAutomation.Canvas.WebApi.Extensions;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.Common.Models;
using ServiceAutomation.DataAccess.Models.EntityModels;
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
                .ForMember(x => x.CardCode, opt => opt.MapFrom(x => x.Credential.IBAN))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.TransactionStatus.ToString()))
                .ForMember(x => x.Amount, opt => opt.MapFrom(x => x.Value))
                .ForMember(x => x.DateTime, opt => opt.MapFrom(x => x.Date));

            CreateMap<VideoLessonTemplateEntity, VideoLessonResponseModel>();

            CreateMap<UserEntity, UserProfileResponseModel>()
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.ProfilePhoto, opt => opt.MapFrom(x => x.ProfilePhoto.FullPath))
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.UserPhoneNumber.PhoneNumber))
                .ForMember(x => x.PersonalReferral, opt => opt.MapFrom(x => x.PersonalReferral));


            CreateMap<PackageBonusAssociationEntity, BonusModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Bonus.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.GetPackageBonusTitle()));

            CreateMap<PackageEntity, PackageModel>()
                .ForMember(x => x.Bonuses, opt => opt.MapFrom(x => x.PackageBonuses.OrderBy(pb => pb.Bonus.DisplayOrder)));

            CreateMap<IndividualUserOrganizationDataEntity, IndividualEntityDataResponseModel>();
            CreateMap<LegalUserOrganizationDataEntity, LegalEntityDataResponseModel>();
            CreateMap<AccrualsEntity, AccuralResponseModel>()
                .ForMember(x => x.TransactionStatus, opt => opt.MapFrom(x => x.TransactionStatus.ToString()));

            CreateMap<LevelEntity, LevelModel>()
                .ForMember( x => x.Level, opt => opt.MapFrom(x => (int)x.Level));
        }
    }
}
