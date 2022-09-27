using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IWebHostEnvironment webHostEnvironment;
        public UserProfileController(IUserProfileService userProfileService, IWebHostEnvironment webHostEnvironment)
        {
            this.userProfileService = userProfileService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        [HttpGet(Constants.Requests.UserProfile.GetProfileInfo)]
        public async Task<UserProfileResponseModel> GetProfileInfo(Guid userId)
        {
            return await userProfileService.GetUserInfo(userId);
        }

        [Authorize]
        [HttpPost(Constants.Requests.UserProfile.UploadProfilePhoto)]
        public async Task<IActionResult> UploadProfilePhoto([FromForm] UploadProfilePhotoRequestModel requestModel)
        {
            var photo = requestModel.ProfilePhoto;

            var response = await userProfileService.UploadProfilePhoto(requestModel.UserId, photo);

            if (!response.Success)
            {
                return BadRequest(response.Errors);
            }

            return Ok();
        }

        [Authorize]
        [HttpPost(Constants.Requests.UserProfile.UploadProfileInfo)]
        public async Task<IActionResult> UploadProfileInfo([FromBody] UploadUserProfileRequestModel requestModel)
        {
            var response = await userProfileService.UploadProfileInfo(requestModel.UserId, requestModel.FirstName, requestModel.LastName, requestModel.Patronymic, requestModel.DateOfBirth);

            if (!response.Success)
            {
                return BadRequest(response.Errors);
            }

            return Ok();
        }

        [Authorize]
        [HttpPost(Constants.Requests.UserProfile.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestModel requestModel)
        {
            if(requestModel.NewPassword != requestModel.ConfirmPassword)
            {
                return BadRequest("Passwords do not match");
            }

            var response = await userProfileService.ChangePassword(requestModel.UserId, requestModel.OldPassword, requestModel.NewPassword);

            if (!response.Success)
            {
                return BadRequest(response.Errors);
            }

            return Ok();
        }

        [Authorize]
        [HttpPost(Constants.Requests.UserProfile.UploadPhoneNumber)]
        public async Task<IActionResult> UploadPhoneNumber([FromBody] ChangePhoneNumberRequestModel requestModel)
        {
            var response = await userProfileService.UploadPhoneNumber(requestModel.UserId, requestModel.NewPhoneNumber);

            if (!response.Success)
            {
                return BadRequest(response.Errors);
            }

            return Ok();
        }

        [Authorize]
        [HttpPost(Constants.Requests.UserProfile.ChangeEmailAdress)]
        public async Task<IActionResult> ChangeEmailAdress([FromBody] ChangeEmailRequestModel requestModel)
        {
            var response = await userProfileService.ChangeEmailAdress(requestModel.UserId, requestModel.NewEmailAdress);

            if (!response.Success)
            {
                return BadRequest(response.Errors);
            }

            return Ok();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAction()
        //{
        //    var res1 = webHostEnvironment.WebRootPath;
        //    var res2 = webHostEnvironment.ContentRootPath;
        //    return Ok(res1 + res2);
        //}
    }
}
