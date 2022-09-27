using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class UserAccountOrganizationEntity : Entity
    {
        public Guid UserId { get; set; }
        public TypeOfEmployment TypeOfEmployment { get; set; }
    }
}
