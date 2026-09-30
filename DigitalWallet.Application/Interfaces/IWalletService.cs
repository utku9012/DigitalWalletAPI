using DigitalWallet.Application.DTOs.Transactions;
using DigitalWallet.Application.DTOs.Wallets;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.Interfaces
{
    public interface IWalletService
    {
        Task<WalletResponseDTO> CreateWalletAsync(CreateWalletDTO request);

        Task<WalletResponseDTO> GetWalletByName(Guid Id, string Name);

        // Task<WalletResponseDTO> CurrencyExchangeAsync(Guid Id, string Name); eklenecek

        Task<IEnumerable<WalletResponseDTO>> GetAllWalletsAsync(Guid userId, string Name);

        Task<WalletResponseDTO> ToggleStatusAsync(Guid walletId, string Name); 

        Task<bool> DeleteAsync(Guid walletId, string Name);
    }
}
