using ServiceAutomation.Canvas.WebApi.Models.SubRequestModels;
using ServiceAutomation.DataAccess.Models.Enums;
using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Collections.Generic;

namespace ServiceAutomation.Canvas.WebApi.Models.RequestsModels
{
    public class DocumentVerificationRequestModel
    {
        public Guid UserId { get; set; }
        public Country Country { get; set; }
        public TypeOfEmployment EmploymentType { get; set; }
        public DocumentModel DocumentVerificationModels { get; set; }
    }
}
