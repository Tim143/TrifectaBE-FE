namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class Rates
    {
        public int Cur_ID { get; set; }
        public string Date { get; set; }
        public string Cur_Abbreviation { get; set; }
        public int Cur_Scale { get; set; }
        public string Cur_Name { get; set; }
        public decimal Cur_OfficialRate { get; set; }
    }

    public class RatesArr
    {
        public Rates[] Rates { get; set; }
    }
}
