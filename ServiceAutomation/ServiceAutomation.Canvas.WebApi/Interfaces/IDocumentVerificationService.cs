using OneOf;
using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.DataAccess.Models.EntityModels;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IDocumentVerificationService
    {
        Task<ResultModel> SendUserVerificationData(DocumentVerificationRequestModel requestModel);
        Task<OneOf<IndividualEntityDataResponseModel, LegalEntityDataResponseModel, ResultModel>> GetUserVerifiedData(Guid userId);
    }
}
