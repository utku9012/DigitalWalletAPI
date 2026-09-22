using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.Interfaces;
using DigitalWallet.Domain.ValueObjects;

namespace DigitalWallet.Domain.Entities
{
    public class Transactions : IEntity
    {
        public int Id { get; set; }

        public int WalletId { get; set; }

        public Wallet Wallet { get; set; }

        public Money Money { get; set; } 

        public ActionType ActionType { get; set; }

        public StatusType StatusType { get; set; }

        public DateTime TransactionDate { get; set; }

        public int ReferenceId { get; set; } // unique olmalı

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
