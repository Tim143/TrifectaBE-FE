using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class AdministratorController : ApiBaseController
    {
        private readonly IAdministratorService administratorService;
        public AdministratorController(IAdministratorService administratorService)
        {
            this.administratorService = administratorService;
        }

        [HttpGet(Constants.Requests.Administrator.GetDocumentVerificationList)]
        public async Task<IActionResult> GetDocumentRequestListAsync()
        {
            return Ok(await administratorService.GetVerificationRequest());
        }

        [HttpPost(Constants.Requests.Administrator.AcceptUserVerification)]
        public async Task AcceptUserVerification(Guid requestId, Guid userId)
        {
            await administratorService.AcceptVerificationRequest(requestId, userId);
        }

        [HttpPost(Constants.Requests.Administrator.RejectUserVerification)]
        public async Task RejectUserVerification(Guid requestId, Guid userId)
        {
            await administratorService.RejectVerificationRequest(requestId, userId);
        }

        [HttpGet(Constants.Requests.Administrator.GetUserContactsVerivicationList)]
        public async Task<IActionResult> GetUserContactsVerivicationList()
        {
            return Ok(await administratorService.GetContactVerificationRequest());
        }

        [HttpPost(Constants.Requests.Administrator.AcceptUserContactVerification)]
        public async Task AcceptUserContactVerification(Guid requestId, Guid userId)
        {
            await administratorService.AcceptContactVerificationRequest(requestId, userId);
        }

        [HttpPost(Constants.Requests.Administrator.RejectUserContactVerification)]
        public async Task RejectUserContactVerification(Guid requestId, Guid userId)
        {
            await administratorService.RejectContactVerificationRequest(requestId, userId);
        }

        [HttpGet(Constants.Requests.Administrator.GetWithdrawRequestList)]
        public async Task<IActionResult> GetWithdrawRequestList()
        {
            return Ok(await administratorService.GetWitdrawRequests());
        }

        [HttpPost(Constants.Requests.Administrator.AcceptUserWithdraw)]
        public async Task AcceptUserWithdraw(Guid requestId)
        {
            await administratorService.AccepWitdrawRequest(requestId);
        }

        [HttpPost(Constants.Requests.Administrator.RejectUserWithdraw)]
        public async Task RejectUserWithdraw(Guid requestId)
        {
            await administratorService.RejectWitdrawRequest(requestId);
        }
    }
}
