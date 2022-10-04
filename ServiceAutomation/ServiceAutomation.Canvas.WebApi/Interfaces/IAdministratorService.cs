using ServiceAutomation.Canvas.WebApi.Models.AdministratorResponseModels;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IAdministratorService
    {
        Task<ICollection<UserVerificationResponseModel>> GetVerificationRequest();
        Task AcceptVerificationRequest(Guid requestId, Guid userId);
        Task RejectVerificationRequest(Guid requestId, Guid userId);
        Task<ICollection<UserContactsVerificationResponseModel>> GetContactVerificationRequest();
        Task AcceptContactVerificationRequest(Guid requestId, Guid userId);
        Task RejectContactVerificationRequest(Guid requestId, Guid userId);
        Task<ICollection<WithdrawVerifictionResponseModel>> GetWitdrawRequests();
        Task AccepWitdrawRequest(Guid requestId);
        Task RejectWitdrawRequest(Guid requestId);
    }
}
