using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class RefreshTokenEntity : Entity
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
    }
}
