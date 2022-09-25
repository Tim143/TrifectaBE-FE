using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IUserReferralService
    {
        string GenerateIviteCode();
        Task<string> GetUserRefferal(Guid userId);
    }
}
