namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class Rates
    {
        public decimal Rate { get; set; }
        public string Iso { get; set; }
        public int Code { get; set; }
        public int Quantity { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
    }

    public class RatesArr
    {
        public Rates[] Rates { get; set; }
    }
}
