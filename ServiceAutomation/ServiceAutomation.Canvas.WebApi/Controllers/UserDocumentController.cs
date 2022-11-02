using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "User")]
    public class UserDocumentController : ApiBaseController
    {
        private readonly IDocumentVerificationService verificationService;

        public UserDocumentController(IDocumentVerificationService verificationService)
        {
            this.verificationService = verificationService;
        }

        
        [HttpPost(Constants.Requests.UserDocument.SendDataForVerification)]
        public async Task<IActionResult> SendDataForVerification([FromBody] DocumentVerificationRequestModel requestModel)
        {
            var response = await verificationService.SendUserVerificationData(requestModel);

            if (!response.Success)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response);
        }

        [HttpPost(Constants.Requests.UserDocument.SendPhotoForVerification)]
        public async Task<IActionResult> SendPhotoForVerification([FromForm] UploadProfilePhotoRequestModel requestModel)
        {
            var response = await verificationService.SendUserVerificationPhoto(requestModel.ProfilePhoto, requestModel.UserId);

            if (!response.Success)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response);
        }

        [HttpPost(Constants.Requests.UserDocument.SendPhotoForVerification2)]
        public async Task<IActionResult> SendPhotoForVerification2([FromForm] UploadProfilePhotoRequestModel requestModel)
        {
            var response = await verificationService.SendUserVerificationPhoto2(requestModel.ProfilePhoto, requestModel.UserId);

            if (!response.Success)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response);
        }

        [HttpPost(Constants.Requests.UserDocument.SendPhotoForVerification3)]
        public async Task<IActionResult> SendPhotoForVerification3([FromForm] UploadProfilePhotoRequestModel requestModel)
        {
            var response = await verificationService.SendUserVerificationPhoto3(requestModel.ProfilePhoto, requestModel.UserId);

            if (!response.Success)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response);
        }

        [HttpPost(Constants.Requests.UserDocument.SendPhotoForVerification4)]
        public async Task<IActionResult> SendPhotoForVerification4([FromForm] UploadProfilePhotoRequestModel requestModel)
        {
            var response = await verificationService.SendUserVerificationPhoto4(requestModel.ProfilePhoto, requestModel.UserId);

            if (!response.Success)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response);
        }

        [HttpGet(Constants.Requests.UserDocument.GetVerifiedData)]
        public async Task<IActionResult> GetVerifiedData(Guid userId)
        {
            var data = await verificationService.GetUserVerifiedData(userId);
            
            if(data.IsT0)
            {
                return Ok(data.AsT0);
            }
            else if(data.IsT1)
            {
                return Ok(data.AsT1);
            }
            else if (data.IsT2)
            {
                return Ok(data.AsT2);
            }
            else if (data.IsT3)
            {
                return Ok(data.AsT3);
            }
            else
            {
                return BadRequest("Errors ocured while data was processing");
            }
        }
    }
}
