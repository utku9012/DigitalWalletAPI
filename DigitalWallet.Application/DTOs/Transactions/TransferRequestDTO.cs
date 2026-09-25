//para transferi için gerekli olan minimal 3 property
namespace DigitalWallet.Application.DTOs.Transactions
{
    public class TransferRequestDTO
    {
        public Guid SenderWalletId { get; set; }

        public Guid ReceiverWalletId { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

    }
}
