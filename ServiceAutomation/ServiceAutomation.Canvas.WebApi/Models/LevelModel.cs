using System;

namespace ServiceAutomation.Canvas.WebApi.Models
{
    public class LevelModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal? Turnover { get; set; }

        public int Level { get; set; }
    }
}
