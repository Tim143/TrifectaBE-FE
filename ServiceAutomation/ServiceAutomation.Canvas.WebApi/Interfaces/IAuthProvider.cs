using System.Threading.Tasks;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using ServiceAutomation.Common.Models;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IAuthProvider
    {
        Task<AuthenticationResult> AuthenticateAsync(LoginRequestModel requestModel);
        Task<AuthenticationResult> RegisterAsync(RegisterRequestModel requestModel);
        Task<AuthenticationResult> RefreshAsync(RefreshRequestModel requestModel);
    }
}
