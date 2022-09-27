using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using System.Threading.Tasks;
using System;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IPersonalDataService
    {
        Task<HomePageResponseModel> GetHomeUserData(Guid userId);
    }
}
