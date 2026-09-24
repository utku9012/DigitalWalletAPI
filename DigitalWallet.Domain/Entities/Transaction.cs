using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.Interfaces;

//bakiye harekelerinin tutulduğu append only tablo 
namespace DigitalWallet.Domain.Entities
{
    public class Transaction : IEntity
    {
        public Guid Id { get; set; }

        public Guid WalletId { get; set; }

        public Wallet Wallet { get; set; }

        public Guid? SenderWalletId { get; set; }

        public Guid? ReceiverWalletId { get; set; }

        public Guid? PaymentId { get; set; } // her transaction payment içermek zorunda değil.

        public Payment? Payment { get; set; }

        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public TransactionType TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }

        public Guid ReferenceId { get; set; } // unique olmalı

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        // Navigation Property, cüzdanın tüm özelliklerine (Bakiye, Kullanıcı vs.) doğrudan erişebilmesi için kullanılır.
        // EF Core varsayılan olarak Foreign Key ile Navigation Property isimlerini birbiriyle eşleştirir. Eğer sütun adın SenderWalletId ise, EF Core otomatik olarak Id'yi kaldırır ve SenderWallet ile de navigation propertydeki SenderWallet ismi eşleşir.
        // Eğer NP ismi farklı olsaydı "X" için SenderWalletId sütununu kullan diyecek kodu yazmamız gerekirdi.
        public virtual Wallet? SenderWallet { get; set; }
        public virtual Wallet? ReceiverWallet { get; set; }

    }
}
