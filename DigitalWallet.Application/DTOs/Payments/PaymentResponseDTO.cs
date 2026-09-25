using DigitalWallet.Domain.Enums;

namespace DigitalWallet.Application.DTOs.Payments
{
    public class PaymentResponseDTO
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string? ReferenceId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
}
