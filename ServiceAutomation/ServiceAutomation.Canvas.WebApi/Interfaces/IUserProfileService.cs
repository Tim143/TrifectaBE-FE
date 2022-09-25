using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileResponseModel> GetUserInfo(Guid userId);
        Task<ResultModel> UploadProfilePhoto(Guid userId, byte[] data);
        Task<ResultModel> UploadProfileInfo(UploadUserProfileRequestModel requestModel);
    }
}
