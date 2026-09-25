using DigitalWallet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Payment> Payments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); // CancellationToken cancellationToken = default: Devam eden DB işlemini iptal edebilmek için kullanılır. Örneğin, kullanıcı web sayfasını işlem bitmeden kapatırsa veya istek zaman aşımına uğrarsa (timeout), gereksiz DB yükünü önlemek için işlem iptal edilir.
    }
}
