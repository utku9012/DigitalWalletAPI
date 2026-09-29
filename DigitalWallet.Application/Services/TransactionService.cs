using DigitalWallet.Application.DTOs.Transactions;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
/*
namespace DigitalWallet.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IApplicationDbContext _context;

        public TransactionService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DepositAsync(DepositRequestDTO request)
        {
            if (request.Amount <= 0)
                throw new InvalidOperationException("Yatırılacak tutar 0'dan büyük olmalıdır.");

            var wallet = await _context.Wallets.FirstOrDefaultAsync(w =>
                    w.UserId == request.UserId &&
                    w.Name.ToLower() == request.WalletName.ToLower());

            if (wallet == null || wallet.WalletStatus != WalletStatus.Active)
                throw new Exception($"'{request.WalletName}' adında aktif bir cüzdan bulunamadı.");

            wallet.Balance += request.Amount;

            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                ReceiverWalletId = wallet.Name,
                Amount = request.Amount,
                TransactionType = TransactionType.Deposit,
                CreatedDate = DateTime.UtcNow
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> WithdrawAsync(WithdrawRequestDTO request)
        {
            if (request.Amount <= 0)
                throw new InvalidOperationException("Çekilecek tutar 0'dan büyük olmalıdır.");

            var wallet = await _context.Wallets
                .FindAsync(w => w.UserId == Id && w.Name.ToLower() == Name.ToLower());

            if (wallet == null || wallet.WalletStatus != WalletStatus.Active) // DB'de var mı kontrolü
            {
                throw new Exception($"{Id}'li cüzdan bulunmuyor");
            }

            if (wallet.Balance < request.Amount)
                throw new InvalidOperationException("Yetersiz bakiye.");

            // Bakiye Düşme
            wallet.Balance -= request.Amount;

            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                SenderWalletId = wallet.Id,
                Amount = request.Amount,
                TransactionType = TransactionType.Withdraw,
                CreatedDate = DateTime.UtcNow
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> TransferAsync(TransferRequestDTO request)
        {
            if (request.Amount <= 0)
                throw new InvalidOperationException("Transfer tutarı 0'dan büyük olmalıdır.");

            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Gönderen Cüzdanı Isimle Bul
                var senderWallet = await _context.Wallets.FindAsync(w =>
                    w.UserId == request.UserId &&
                    w.Name.ToLower() == request.SenderWalletName.ToLower());

                // Alıcı Cüzdanı İsimle Bul (Aynı kullanıcının cüzdanı ise)
                var receiverWallet = await _context.Wallets.FindAsync(w =>
                    w.UserId == request.UserId &&
                    w.Name.ToLower() == request.ReceiverWalletName.ToLower());

                if (senderWallet == null || receiverWallet == null)
                    throw new Exception("Belirtilen cüzdanlardan biri bulunamadı.");

                if (senderWallet.Id == receiverWallet.Id)
                    throw new InvalidOperationException("Aynı cüzdana transfer yapılamaz.");

                if (senderWallet.Balance < request.Amount)
                    throw new InvalidOperationException("Yetersiz bakiye.");

                // Bakiyeleri Güncelle
                senderWallet.Balance -= request.Amount;
                receiverWallet.Balance += request.Amount;

                var transactionLog = new Transaction
                {
                    Id = Guid.NewGuid(),
                    SenderWalletId = senderWallet.Id,
                    ReceiverWalletId = receiverWallet.Id,
                    Amount = request.Amount,
                    TransactionType = TransactionType.Transfer,
                    CreatedDate = DateTime.UtcNow
                };

                _context.Transactions.Add(transactionLog);
                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();

                return true;
            }
            catch
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }
        }

        public Task<IEnumerable<TransactionResponseDTO>> GetWalletHistoryAsync(Guid walletId)
        {
            throw new NotImplementedException();
        }
    }
}
*/