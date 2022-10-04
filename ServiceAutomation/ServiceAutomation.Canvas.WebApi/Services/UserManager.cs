using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceAutomaion.Services.Interfaces;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Common.Models;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class UserManager : IUserManager
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IIdentityGenerator identityGenerator;
        private readonly IUserReferralService userReferralService;
        private readonly ITenantGroupService tenantGroupService;
        private readonly ILevelStatisticService levelStatisticService;

        public UserManager(AppDbContext dbContext,
                            IMapper mapper,
                            IIdentityGenerator identityGenerator,
                            IUserReferralService userReferralService,
                            ITenantGroupService tenantGroupService,
                            ILevelStatisticService levelStatisticService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.identityGenerator = identityGenerator;
            this.userReferralService = userReferralService;
            this.tenantGroupService = tenantGroupService;
            this.levelStatisticService = levelStatisticService;
        }

        public async Task<UserModel> AddUserAsync(UserModel user)
        {
            var firstBasicLevel = await dbContext.BasicLevels.SingleOrDefaultAsync(x => x.Level == DataAccess.Models.Enums.Level.FirstLevel);

            var addedUser = new UserEntity()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                Email = user.Email.ToLower(),
                PhoneNumber = user.PhoneNumber,
                Country = user.Country,
                InviteReferral = user.InviteCode,
                PersonalReferral = userReferralService.GenerateIviteCode(),
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
                BasicLevel = firstBasicLevel,
                IsVerifiedUser = false,
                Role = "User"
            };


            await dbContext.Users.AddAsync(addedUser);
            await dbContext.SaveChangesAsync();

            var userModel = mapper.Map<UserModel>(addedUser);
            
            await tenantGroupService.CreateTenantGroupForUserAsync(userModel);
            await levelStatisticService.AddLevelsInfoForNewUserAsync(addedUser.Id);

            await dbContext.SaveChangesAsync();

            return userModel;
        }

        public async Task<UserModel> GetByEmailAsync(string email)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email.ToLower());

            if(user == null)
            {
                return null;
            }

            return mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return null;
            }

            return mapper.Map<UserModel>(user);
        }

        public async Task<bool> IsReferraValidAsync(string referralCode)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.PersonalReferral == referralCode);

            if(user != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsUserAlreadyExistsAsync(string email)
        {
            var user = await dbContext.Users.Where(x => x.Email == email.ToLower()).ToListAsync();

            if(user.Count > 0)
            {
                return true;
            }

            return false;
        }       
    }
}
