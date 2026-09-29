using DigitalWallet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.DTOs.Transactions
{
    public class TransactionResponseDTO
    {
        public Guid Id { get; set; }

        public string SenderWalletName { get; set; }

        public string ReceiverWalletName { get; set; }

        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public TransactionType TransactionType { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}
