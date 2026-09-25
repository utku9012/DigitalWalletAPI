using DigitalWallet.Application.DTOs.Transactions;
using DigitalWallet.Application.DTOs.Wallets;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.Interfaces
{
    public interface IWalletService
    {
        Task<WalletResponseDTO> CreateWallet(CreateWalletDTO request);

        Task<WalletResponseDTO> GetWalletById(Guid Id);

        Task<IEnumerable<WalletResponseDTO>> GetUserWalletsAsync(Guid userId);

        Task<bool> DepositAsync(DepositRequestDTO request);
        Task<bool> WithdrawAsync(WithdrawRequestDTO request);
        Task<bool> TransferAsync(TransferRequestDTO request);

    }
}
