using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.Interfaces;
using DigitalWallet.Domain.ValueObjects;

//kullanıcı 
namespace DigitalWallet.Domain.Entities
{
    public class User : IEntity
    {
        
        public Guid Id { get; set; }

        public string IdentityNumber { get; set; }

        public ICollection<Wallet> Wallets { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Email Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public KYCLevel KYC { get; set; }

        public UserStatus IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public string PasswordHash { get; set; }
    }
}
