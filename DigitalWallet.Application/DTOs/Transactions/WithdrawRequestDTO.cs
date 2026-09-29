// para çekme için 
namespace DigitalWallet.Application.DTOs.Transactions
{
    public class WithdrawRequestDTO
    {
        public Guid UserId { get; set; }

        public string WalletName { get; set; }

        public decimal Amount { get; set; }
    }
}
