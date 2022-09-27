using ServiceAutomation.DataAccess.Schemas.Enums;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IUserVerificationService
    {
        Country Country { get; set; }
        Task UploadVerificationData(object data);
    }
}
