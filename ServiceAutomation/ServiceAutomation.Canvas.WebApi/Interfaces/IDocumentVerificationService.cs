using Microsoft.AspNetCore.Http;
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
        Task<ResultModel> SendUserVerificationPhoto(IFormFile file, Guid userId);
        Task<OneOf<IndividualEntityDataResponseModel, IndividualEntrepreneurEntityDataResponseModel, LegalEntityDataResponseModel, ResultModel>> GetUserVerifiedData(Guid userId);
    }
}
