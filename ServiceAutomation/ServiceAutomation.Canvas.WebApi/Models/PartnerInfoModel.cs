using System;

namespace ServiceAutomation.Canvas.WebApi.Models
{
    public class PartnerInfoModel
    {
        public Guid UserId { get; set; }
        public int Wave { get; set; }
        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Email { get; init; }

        public string Phone { get; init; }

        public string PackageType { get; init; }

        public double PersonalTurnover { get; set; }

        public double FirstLineTurnover { get; init; }

        public double GroupTurnover { get; set; }

        public int BaseLevel { get; set; }
    }
}
