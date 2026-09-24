using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.Interfaces;
//kullanıcı cüzdanı

namespace DigitalWallet.Domain.Entities
{
    public class Wallet : IEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; } //foreign key

        public User User { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public ICollection<Transaction> Transactions { get; set; } 

        public string IBAN { get; set; }

        public decimal Balance { get; set; }

        public Currency Currency { get; set; }

        public DateTime RowVersion { get; set; }

        public DateTime CreatedDate { get; set; }

        public WalletStatus WalletStatus { get; set; }
        
    }
}
