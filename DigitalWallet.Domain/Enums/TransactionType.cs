using System;
using System.Collections.Generic;
using System.Text;
// işlem durumu
namespace DigitalWallet.Domain.Enums
{
    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        Payment,
        Refund,
        Fee
    }
}
