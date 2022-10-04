using System;
using System.Collections;
using System.Collections.Generic;

namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class WithdrawVerifictionResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal WithdrawSum { get; set; }
        public string CheckingAccount { get; set; }
        public DateTime Date { get; set; }
        public List<AccuralResponseModel> Accurals { get; set; }
    }
}
