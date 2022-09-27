using System;
using System.Collections.Generic;

namespace ServiceAutomation.Canvas.WebApi.Models
{
    public class PackageModel
    {
        public Guid Id { get; internal set; }

        public string Name { get; internal set; }

        public decimal Price { get; internal set; }

        public IList<BonusModel> Bonuses { get; internal set; }
    }
}
