using DigitalWallet.Domain.Enums;

namespace DigitalWallet.Application.DTOs.Wallets
{
    public class CreateWalletDTO
    {
        public string FirstName { get; set; }

        public string IdentityNumber { get; set; }

        public Currency Currency { get; set; }

    }
}
