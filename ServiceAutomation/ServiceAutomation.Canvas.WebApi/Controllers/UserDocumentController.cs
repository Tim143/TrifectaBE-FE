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
    public class UserDocumentController : ApiBaseController
    {
        private readonly IDocumentVerificationService verificationService;

        public UserDocumentController(IDocumentVerificationService verificationService)
        {
            this.verificationService = verificationService;
        }

        [Authorize]
        [HttpPost(Constants.Requests.UserDocument.SendDataForVerification)]
        public async Task<IActionResult> SendDataForVerification([FromForm] DocumentVerificationRequestModel requestModel)
        {
            var response = await verificationService.SendUserVerificationData(requestModel);

            if (!response.Success)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response);
        }

        [Authorize]
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
            else
            {
                return BadRequest("Errors ocured while data was processing");
            }
        }
    }
}
