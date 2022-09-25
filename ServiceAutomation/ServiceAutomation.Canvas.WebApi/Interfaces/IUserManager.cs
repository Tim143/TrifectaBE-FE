using System;
using System.Threading.Tasks;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.Common.Models;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IUserManager
    {
        Task<UserModel> GetByEmailAsync(string email);
        Task<UserModel> GetByIdAsync(Guid id);
        Task<UserModel> AddUserAsync(UserModel user);
        Task<bool> IsUserAlreadyExistsAsync(string email);
    }
}
