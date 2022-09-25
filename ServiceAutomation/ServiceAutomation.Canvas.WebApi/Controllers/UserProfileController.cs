using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql.TypeHandlers.DateTimeHandlers;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ApiBaseController
    {
        private readonly IUserProfileService userProfileService;
        public UserProfileController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        [Authorize]
        [HttpGet(Constants.Requests.UserProfile.GetProfileInfo)]
        public async Task<UserProfileResponseModel> GetProfileInfo(Guid userId)
        {
            return await userProfileService.GetUserInfo(userId);
        }

        [Authorize]
        [HttpPost(Constants.Requests.UserProfile.UploadProfilePhoto)]
        public async Task<ResultModel> UploadProfilePhoto([FromForm] UploadProfilePhotoRequestModel requestModel)
        {
            var photo = requestModel.ProfilePhoto;

            await using var memoryStream = new MemoryStream();
            await photo.CopyToAsync(memoryStream);
            var photoData = memoryStream.ToArray();

            return await userProfileService.UploadProfilePhoto(requestModel.UserId, photoData);
        }

        [Authorize]
        [HttpPost(Constants.Requests.UserProfile.UploadProfileInfo)]
        public async Task<ResultModel> UploadProfileInfo([FromBody] UploadUserProfileRequestModel requestModel)
        {
            return await userProfileService.UploadProfileInfo(requestModel);
        }
    }
}
