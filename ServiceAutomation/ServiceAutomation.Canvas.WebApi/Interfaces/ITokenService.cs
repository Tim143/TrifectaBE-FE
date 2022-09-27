using ServiceAutomation.Common.Models;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface ITokenService
    {
        Task<Guid> CreateAsync(RefreshToken token);
        Task<RefreshToken> GetRefreshTokenAsync(string token);
        Task<RefreshToken> GetRefreshTokenAsync(Guid userId);
        Task DeleteRefreshTokenAsync(Guid id);
        Task DeleteAllAsync(Guid userId);
    }
}
