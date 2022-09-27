using LevelHostedService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceAutomaion.Services.Interfaces;
using ServiceAutomaion.Services.Services;
using ServiceAutomation.Canvas.AutoMapping;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Services;
using ServiceAutomation.DataAccess.Extensions;


namespace ServiceAutomation.Canvas.WebApi.DependencyInjection
{
    public abstract class ServiceCollectionSetup
    {
        public virtual IServiceCollection Configure(IConfiguration configuration, IServiceCollection services)
        {
            SetupDatabase(services);
            SetupMapper(services);
            SetupServices(services);

            return services;
        }

        public virtual void SetupDatabase(IServiceCollection services)
        {
            services.ConfigureDataAccessServices((o) => o.CollectSqlQueries = false);
        }

        public virtual void SetupMapper(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<MappingProfile>();
            });
        }

        public virtual void SetupServices(IServiceCollection services)
        {
            services.AddScoped<IIdentityGenerator,IdentityGenerator>();
            services.AddScoped<IAuthProvider, AuthProvider>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IInfoService, InfoService>();
            services.AddScoped<IWithdrawService, WithdrawService>();
            services.AddScoped<ITenantGroupService, TenantGroupService>();
            services.AddScoped<IUserReferralService, UserReferralService>();
            services.AddScoped<IVideoTemplateService, VideoTemplateService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IPackagesService, PackagesService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IDocumentVerificationService, DocumentVerificationService>();
            services.AddScoped<IPersonalDataService, PersonalDataService>();

            services.AddHostedService<LevelHostedServices>();            
            services.AddScoped<ITurnoverService, TurnoverService>();
            services.AddScoped<ILevelsService, LevelsService>();
        }
    }

    public class WebApiServiceCollectionSetup : ServiceCollectionSetup
    {

    }
}
