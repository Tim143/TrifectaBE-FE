using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Linq;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    public abstract class ApiBaseController : ControllerBase
    {
        protected Guid GetCurrentUserId()
        {
            var claimsPrincipal = HttpContext.User as ClaimsPrincipal;

            if (claimsPrincipal == null)
            {
                throw new UnauthorizedAccessException();
            }
                
            var claimEmployeeId = claimsPrincipal.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claimEmployeeId == null)
            {
                throw new UnauthorizedAccessException();
            }
                
            if (!Guid.TryParse(claimEmployeeId.Value, out Guid employeeId))
            {
                throw new UnauthorizedAccessException();
            }
                
            return employeeId;
        }
    }
}
