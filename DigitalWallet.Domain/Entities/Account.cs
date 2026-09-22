using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public int UserId { get; set; } //foreign key

        public User User { get; set; }

        public string IBAN { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
