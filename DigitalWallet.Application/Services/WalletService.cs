using DigitalWallet.Application.DTOs.Wallets;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IApplicationDbContext _context;

        public WalletService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WalletResponseDTO> CreateWalletAsync(CreateWalletDTO request)
        {
            var userExists = await _context.Users.AnyAsync(w => w.Id == request.UserId);
            if (!userExists)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            var walletExists = await _context.Wallets.AnyAsync(w => w.Currency == request.Currency);
            if (!walletExists)
            {
                throw new InvalidOperationException($"Kullanıcının zaten aktif bir {request.Currency} cüzdanı bulunmaktadır.");
            }

            var wallet = new Wallet
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Name = string.IsNullOrWhiteSpace(request.Name) ? $"{request.Currency} Cüzdanım" : request.Name,
                Balance = 0,
                IBAN = "TR" + new Random().Next(10000000, 99999999).ToString(), 
                Currency = request.Currency,
                WalletStatus = WalletStatus.Active,
                CreatedDate = DateTime.UtcNow
            };

            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();

            return new WalletResponseDTO
            {
                Name = wallet.Name,
                Balance = wallet.Balance,
                Currency = wallet.Currency,
                IBAN = wallet.IBAN,
                WalletStatus = wallet.WalletStatus,
                CreatedDate = wallet.CreatedDate
            };
        }

        public async Task<WalletResponseDTO> GetWalletById(Guid Id, string Name)
        {
            var wallet = await _context.Wallets
                .FirstOrDefaultAsync(w => w.UserId == Id && w.Name.ToLower() == Name.ToLower());

            if (wallet == null)
            {
                throw new Exception("");
            }

            var walletExists = await _context.Wallets
                .FirstOrDefaultAsync(w => w.UserId == Id && w.Name.ToLower() == Name.ToLower());

            if (walletExists == null) // DB'de var mı kontrolü
            {
                throw new Exception($"{Id}'li cüzdan bulunmuyor");
            }

            return new WalletResponseDTO
            {
                Name = wallet.Name,
                Balance = wallet.Balance,
                Currency = wallet.Currency,
                IBAN = wallet.IBAN,
                WalletStatus = wallet.WalletStatus,
                CreatedDate = wallet.CreatedDate
            };
        }

        public async Task<IEnumerable<WalletResponseDTO>> GetAllWalletsAsync(Guid userId, string Name)
        {
            var userWallets = await _context.Wallets
                .Where(w => w.UserId == userId)
                .ToListAsync();

            // cüzdanları (List<Wallet>) DTO listesine (IEnumerable<WalletResponseDTO>) dönüştürüp
            return userWallets.Select(w => new WalletResponseDTO
            {
                Name = w.Name,
                Balance = w.Balance,
                Currency = w.Currency,
                IBAN = w.IBAN,
                WalletStatus = w.WalletStatus,
                CreatedDate = w.CreatedDate
            });
        }

        public async Task<WalletResponseDTO> ToggleStatusAsync(Guid Id, string Name )
        {
            var wallet = await _context.Wallets.FindAsync(Id);
            if (wallet == null)
                throw new Exception("Cüzdan bulunamadı.");

            if (wallet.WalletStatus == WalletStatus.Active)
            {
                wallet.WalletStatus = WalletStatus.Passive; 
            }
            else
            {
                wallet.WalletStatus = WalletStatus.Active;
            }

            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();

            return new WalletResponseDTO
            {
                Name = wallet.Name,
                WalletStatus = wallet.WalletStatus,
            };
        }

        public async Task<bool> DeleteAsync(Guid Id, string Name)
        {
            var wallet = await _context.Wallets.FindAsync(Id);
            if (wallet == null)
                throw new Exception("Cüzdan bulunamadı.");

            if (wallet.Balance > 0)
                throw new InvalidOperationException("Bakiye bulunan bir cüzdan silinemez. Lütfen önce bakiyenizi transfer edin veya çekin.");

            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
