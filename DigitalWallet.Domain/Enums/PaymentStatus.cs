using System;
using System.Collections.Generic;
using System.Text;
// ödeme durumu
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
