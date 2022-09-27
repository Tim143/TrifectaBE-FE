using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Factory
{
    public class CountryVerificationService : ICountrVerificationService
    {
        public IUserVerificationService CreateCountryService(Country country)
        {
            throw new NotImplementedException();
        }

        public Task<IUserVerificationService> CreateCountryServiceByEmployeeAsync(Guid employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
