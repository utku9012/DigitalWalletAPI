using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.DTOs.Payments
{
    public class PaymentRequestDTO
    {
        public Guid WalletId { get; set; }       
        public decimal Amount { get; set; }       
    }
}
