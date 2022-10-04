using System;

namespace ServiceAutomation.Canvas.WebApi.Models
{
    public class ReferralGroupModel
    {
        public Guid Id { get; init; }

        public PartnerInfoModel GroupOwner { get; init; }

        public ReferralGroupModel[] PartnersGroups { get; set; }

        public bool HasPartners { get; init; }
    }
}
