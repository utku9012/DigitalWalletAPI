using DigitalWallet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Domain.ValueObjects
{
    public sealed class Money
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }

        public Money(decimal amount, Currency currency)
        {
            if (amount == 0)
                throw new ArgumentException(
                    "Money amount can't be negative",
                    nameof(amount));

                Amount = amount;
                Currency = currency;
        }
    }
}
