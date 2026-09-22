using System;
using System.Collections.Generic;
using System.Text;
using DigitalWallet.Domain.Enums;

namespace DigitalWallet.Domain.Entities
{
    public class Wallet
    {
        public int Id { get; set; }

        public int UserId { get; set; } //foreign key

        public User User { get; set; }

        public string IBAN { get; set; }

        public decimal Balance { get; set; }

        public Currency currency { get; set; }

        public StatusType status { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
