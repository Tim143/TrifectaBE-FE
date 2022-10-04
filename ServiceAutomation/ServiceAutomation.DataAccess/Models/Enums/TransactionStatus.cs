using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAutomation.DataAccess.Schemas.Enums
{
    public enum TransactionStatus
    {
        Accept = 1,
        Failed = 2,
        Pending = 3,
        ReadyForWithdraw = 4,
    }
}
