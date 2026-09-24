using DigitalWallet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.DTOs
{
    public class WalletResponseDTO
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public Currency Currency { get; set; }
    }
}
