using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.DTOs
{
    public class TransferRequestDTO
    {
        public Guid SenderWalletId { get; set; }

        public Guid ReceiverWalletId { get; set; }

        public decimal Amount { get; set; }

    }
}
