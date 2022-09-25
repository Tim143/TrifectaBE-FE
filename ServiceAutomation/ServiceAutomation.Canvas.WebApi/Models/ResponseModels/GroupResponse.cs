using System.Collections.Generic;

namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class GroupResponse
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        //тут надо добавить поля тип пакета + обороты ( начала надо продумать как все будет работать )

        public ICollection<GroupResponse> children { get; set; }
    }
}
