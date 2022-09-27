using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Factory
{
    public interface ICountrVerificationService
    {
        IUserVerificationService CreateCountryService(Country country);
        Task<IUserVerificationService> CreateCountryServiceByEmployeeAsync(Guid employeeId);
    }
}
