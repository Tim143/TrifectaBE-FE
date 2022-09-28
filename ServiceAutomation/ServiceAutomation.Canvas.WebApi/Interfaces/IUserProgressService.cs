using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IUserProgressService
    {
        Task<ProgressResponseModel> GetUserProgress(Guid userId);
    }
}
