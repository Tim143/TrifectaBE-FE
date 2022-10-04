using Microsoft.AspNetCore.Http;
using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileResponseModel> GetUserInfo(Guid userId);
        Task<ResultModel> UploadProfilePhoto(Guid userId, IFormFile data);
        Task<ResultModel> UploadProfileInfo(Guid userId, string firstName, string lastName, string patronymic, DateTime dateOfBirth);
        Task<ResultModel> ChangePassword(Guid userId, string oldPassword, string newPassword);
        Task<ResultModel> ChangeEmailAdress(Guid userId, string newEmail);
        Task<ResultModel> UploadPhoneNumber(Guid userId, string newPhoneNumber);
    }
}
