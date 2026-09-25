// para çekme için 
namespace DigitalWallet.Application.DTOs.Transactions
{
    public class WithdrawRequestDTO
    {
        public Guid WalletId { get; set; }

        public decimal Amount { get; set; }
    }
}
