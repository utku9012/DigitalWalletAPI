using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.Interfaces;

//bakiye harekelerinin tutulduğu append only tablo 
namespace DigitalWallet.Domain.Entities
{
    public class Transaction : IEntity
    {
        public int Id { get; set; }

        public int WalletId { get; set; }

        public Wallet Wallet { get; set; } 

        public int? PaymentId { get; set; } // her transaction payment içermek zorunda değil.

        public Payment? Payment { get; set; }

        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public TransactionType TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }

        public int ReferenceId { get; set; } // unique olmalı

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
