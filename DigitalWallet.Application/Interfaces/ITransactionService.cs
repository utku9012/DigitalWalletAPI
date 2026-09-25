using DigitalWallet.Application.DTOs.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> DepositAsync(DepositRequestDTO request);
        Task<bool> WithdrawAsync(WithdrawRequestDTO request);
        Task<bool> TransferAsync(TransferRequestDTO request);
        Task<IEnumerable<TransactionResponseDTO>> GetWalletHistoryAsync(Guid walletId);
    }
}
