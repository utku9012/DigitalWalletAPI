using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Domain.Enums
{   public enum PaymentStatus
    {
        Pending,
        Processing,
        Completed,
        Failed,
        Cancelled,
        Refunded
    }
}
