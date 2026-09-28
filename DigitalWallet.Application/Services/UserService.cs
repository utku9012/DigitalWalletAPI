using DigitalWallet.Application.DTOs.Users;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;


namespace DigitalWallet.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        private readonly JWTService _jwtService;

        public UserService(IApplicationDbContext context, IConfiguration configuration, JWTService jwtService)
        {
            _context = context;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        public async Task<UserResponseDTO> RegisterAsync(RegisterRequestDTO request)
        {
            //E-posta, kimlik kontrolü. Email format doğrulaması ValueObject içinde
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                throw new Exception("E-posta adresi kullanılıyor.");
            }

            var existingUserTC = await _context.Users.FirstOrDefaultAsync(u => u.IdentityNumber == request.IdentityNumber);
            if (existingUserTC != null)
            {
                throw new Exception("TC Kimlik numarası kullanılıyor.");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                IdentityNumber = request.IdentityNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = hashedPassword,
                PhoneNumber = request.PhoneNumber = string.Empty,
                KYC = KYCLevel.Standart,
                IsActive = UserStatus.Active,
                CreatedDate = DateTime.UtcNow

            };

            _context.Users.Add(user);

            var defaultWallet = new Wallet
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Balance = 0,
                Currency = Currency.TRY,
                WalletStatus = WalletStatus.Active,
                CreatedDate = DateTime.UtcNow
            };

            _context.Wallets.Add(defaultWallet);

            await _context.SaveChangesAsync();

            return new UserResponseDTO
            {
                FirstName = $"{user.FirstName}",
                LastName = $"{user.LastName}",
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                KYC = user.KYC,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate
            };
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request, UserStatus IsActive)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                throw new Exception("E-posta veya şifre hatalı.");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isPasswordValid)
                throw new Exception("E-posta veya şifre hatalı.");

            if (IsActive == UserStatus.Passive)
            {
                throw new Exception("Hesabınız aktif değil.");
            }

            string token = _jwtService.GenerateToken(user);

            return new LoginResponseDTO
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1),

                User = new UserResponseDTO
                {
                    FirstName = $"{user.FirstName}",
                    LastName = $"{user.LastName}",
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                }
            };
        }
            

        public async Task<bool> LogOffAsync(Guid Id) 
        {
            var user = await _context.Users.FindAsync(Id);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            await _context.SaveChangesAsync(); // Login mi kontrolü controller içinde [Authorize] ile yapılacak
            return true;
        }

        public async Task<UserResponseDTO?> GetByIdAsync(Guid Id)
        {

            if (Id == Guid.Empty) // id geçerli mi kontrolü
            {
                throw new Exception("Geçersiz kullanıcı Id");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (user == null) // DB'de var mı kontrolü
            {
                throw new Exception($"{Id}'li kullanıcı bulunmuyor");
            }

            return new UserResponseDTO
            {
                FirstName = $"{user.FirstName}",
                LastName = $"{user.LastName}",
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
        }

        public async Task<UserResponseDTO> GetMeAsync(Guid Id)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user == null) return null;

            return new UserResponseDTO
            {
                FirstName = $"{user.FirstName}",
                LastName = $"{user.LastName}",
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

        }

        /*
        public Task<UserResponseDTO> UpdateKYC(UpdateKYCRequestDTO request)
        {
            return new UserResponseDTO();  
        }
        */

        public async Task<UserResponseDTO> UpdateUserAsync(Guid Id, UpdateUserRequestDTO request)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();


            return new UserResponseDTO
            {
                FirstName = $"{user.FirstName}",
                LastName = $"{user.LastName}",
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
        }

        public async Task<bool> DeleteUserAsync(Guid Id)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            var activeWallets = await _context.Wallets
                    .Where(w => w.UserId == Id && w.WalletStatus == WalletStatus.Active)
                    .ToListAsync();

            if (activeWallets.Any(w => w.Balance > 0))
                throw new Exception("İçinde bakiye bulunan bir cüzdan silinemez.");


            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}