using AutoMapper;
using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.Common.Models;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.Canvas.AutoMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserModel>();
            CreateMap<RegisterRequestModel, UserModel>();
            CreateMap<RefreshTokenEntity, RefreshToken>();
            CreateMap<ThumbnailTemplateEntity, ThumbnailResponseModel>();
            CreateMap<WithdrawTransactionEntity, WithdrawResponseModel>();
            CreateMap<VideoLessonTemplateEntity, VideoLessonResponseModel>();

            CreateMap<UserContactEntity, UserProfileResponseModel>()
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.User.Email))
                .ForMember(x => x.ProfilePhoto, opt => opt.MapFrom(x => x.User.ProfilePhoto.Data))
                .ForMember(x => x.PersonalReferral, opt => opt.MapFrom(x => x.User.PersonalReferral));
        }
    }
}
