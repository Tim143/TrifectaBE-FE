using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface ISalesService
    {
        Task<int> GetUserSalesCountAsync(Guid userId);

        Task<int> GerSalesCountInMonthAsync(Guid userId);

    }
}
