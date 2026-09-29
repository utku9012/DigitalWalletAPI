using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Infrastructure
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Payment> Payments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region User

            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.IdentityNumber)
                .HasMaxLength(11);

            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .HasMaxLength(30);

            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .HasMaxLength(30);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasConversion(
                    e => e.Value,                   // DB'ye yazarken: Email -> string
                    s => new Email(s)               // DB'den okurken: string -> Email
                );

            modelBuilder.Entity<User>()
                .Property(u => u.PhoneNumber);

            modelBuilder.Entity<User>()
                .Property(u => u.Password);

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash);

            modelBuilder.Entity<User>() // enum mapleme
                .Property(u => u.KYC)
                .HasConversion<string>();

            modelBuilder.Entity<User>() 
                .Property(u => u.IsActive)
                .HasConversion<string>(); // olmazsa BoolToZeroOneConverter kullan

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedDate);

            #endregion

            #region Wallet

            modelBuilder.Entity<Wallet>()
                .ToTable("Wallets")
                .HasKey(u => u.Id);

            modelBuilder.Entity<Wallet>()
                .Property(u => u.Name);

            modelBuilder.Entity<Wallet>()
                .HasOne(u => u.User)
                .WithMany(b => b.Wallets)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Wallet>()
                .HasMany(w => w.Payments)
                .WithOne(p => p.Wallet)
                .HasForeignKey(p => p.WalletId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wallet>()
                .HasMany(w => w.Transactions)
                .WithOne(p => p.Wallet)
                .HasForeignKey(p => p.WalletId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wallet>()
                .Property(u => u.IBAN)
                .HasMaxLength(26);

            modelBuilder.Entity<Wallet>()
                .Property(u => u.Balance)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Wallet>()
                .Property(u => u.Currency)
                .HasConversion<string>();

            modelBuilder.Entity<Wallet>()
                .Property(u => u.CreatedDate);

            modelBuilder.Entity<Wallet>()
                .Property(u => u.WalletStatus)
                .HasConversion<string>();

            #endregion

            #region Payment
            modelBuilder.Entity<Payment>()
                .ToTable("Payments")
                .HasKey(u => u.Id);

            modelBuilder.Entity<Payment>()
               .Property(p => p.WalletId)
               .IsRequired();

            modelBuilder.Entity<Payment>()
               .Property(u => u.Amount)
               .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
               .Property(u => u.Currency)
               .HasConversion<string>()
               .IsRequired();

            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentStatus)
                .HasConversion<string>()
                .IsRequired();

            modelBuilder.Entity<Payment>()
               .Property(u => u.ReferenceId);

            modelBuilder.Entity<Payment>()
               .Property(u => u.CreatedDate);

            modelBuilder.Entity<Payment>()
               .HasOne(p => p.Wallet)
               .WithMany(w => w.Payments)
               .HasForeignKey(p => p.WalletId)
               .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Transaction

            modelBuilder.Entity<Transaction>()
                .ToTable("Transactions")
                .HasKey(t => t.Id);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.WalletId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.ReceiverWallet)
                .WithMany() 
                .HasForeignKey(t => t.ReceiverWalletId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SenderWallet)
                .WithMany() 
                .HasForeignKey(t => t.SenderWalletId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Payment)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.PaymentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Currency)
                .HasConversion<string>();

            modelBuilder.Entity<Transaction>()
                .Property(t => t.TransactionType)
                .HasConversion<string>();

            modelBuilder.Entity<Transaction>()
                .Property(t => t.CreatedDate);

            modelBuilder.Entity<Transaction>()
                .Property(u => u.ReferenceId);

            modelBuilder.Entity<Transaction>()
                .Property(u => u.Description)
                .HasMaxLength(100);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Payment)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.PaymentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict); // silinmesini engelliyor.
            #endregion
        }
    }
}