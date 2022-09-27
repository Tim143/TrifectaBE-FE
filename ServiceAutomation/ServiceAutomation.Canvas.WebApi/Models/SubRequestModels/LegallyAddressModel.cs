using ServiceAutomation.DataAccess.Models.Enums;

namespace ServiceAutomation.Canvas.WebApi.Models.SubRequestModels
{
    public class LegallyAddressModel 
    {
        public string Region { get; set; }
        public string Locality { get; set; }
        public string Index { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public Location Location { get; set; }
        public string RoomNumber { get; set; }
    }
}
