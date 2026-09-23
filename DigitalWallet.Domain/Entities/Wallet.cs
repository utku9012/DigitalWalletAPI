using DigitalWallet.Domain.Enums;
//kullanıcı cüzdanı

namespace DigitalWallet.Domain.Entities
{
    public class Wallet
    {
        public int Id { get; set; }

        public int UserId { get; set; } //foreign key

        public User User { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public ICollection<Transaction> Transactions { get; set; } 

        public string IBAN { get; set; }

        public decimal Balance { get; set; }

        public Currency Currency { get; set; }

        public DateTime RowVersion { get; set; }

    }
}
