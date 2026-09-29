//para transferi için gerekli olan minimal 3 property
namespace DigitalWallet.Application.DTOs.Transactions
{
    public class TransferRequestDTO
    {
        public Guid UserId { get; set; }

        public string SenderWalletName { get; set; }

        public string ReceiverWalletName { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

    }
}
