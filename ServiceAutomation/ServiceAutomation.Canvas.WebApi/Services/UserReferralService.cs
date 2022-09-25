using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.DataAccess.DbContexts;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class UserReferralService : IUserReferralService
    {
        private readonly AppDbContext dbContext;

        public UserReferralService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string GenerateIviteCode()
        {
            var identityGuid = Guid.NewGuid();
            return $"https://trifecta.com/partnerlink/{GenerateIdentityCode(identityGuid)}";
        }

        private string GenerateIdentityCode(Guid guid)
        {
           return guid.ToString().Substring(0, 8);
        }

        public async Task<string> GetUserRefferal(Guid userId)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            return user.PersonalReferral;
        }
    }
}
