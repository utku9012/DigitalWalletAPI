using DigitalWallet.Domain.Enums;

namespace DigitalWallet.Application.DTOs.Wallets
{
    public class CreateWalletDTO
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public Currency Currency { get; set; }
    }
}
