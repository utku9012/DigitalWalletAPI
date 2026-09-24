using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.Interfaces;

//Ödemelerin bilgisi

namespace DigitalWallet.Domain.Entities
{
    public class Payment : IEntity
    {
        public Guid Id { get; set; }

        public Guid WalletId { get; set; }

        public Wallet Wallet { get; set; }

        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public int ReferenceId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
