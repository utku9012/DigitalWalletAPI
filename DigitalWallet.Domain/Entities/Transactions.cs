using DigitalWallet.Domain.Enums;

namespace DigitalWallet.Domain.Entities
{
    public class Transactions
    {
        public int Id { get; set; }

        public int SenderAccountId { get; set; }

        public int ReceiverAccountId { get; set; }

        public decimal Amount { get; set; }

        public ActionType ActionType { get; set; }

        public StatusType StatusType { get; set; }

        public DateTime TransactionDate { get; set; }

        public int ReferenceNumber { get; set; }
    }
}
