using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class UserVerificationPhotoEntity : Entity
    {
        public Guid UserId { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
