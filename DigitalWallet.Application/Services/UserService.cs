using DigitalWallet.Application.DTOs.Users;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;


namespace DigitalWallet.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;

        public UserService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponseDTO> RegisterAsync(RegisterRequestDTO request)
        {
            //E-posta, kimlik kontrolü ve e mail format doğrulaması ValueObject içinde
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null) {
               throw new Exception("E-posta adresi kullanılıyor.");
            }

            var existingUserTC = await _context.Users.FirstOrDefaultAsync(u => u.IdentityNumber == request.IdentityNumber);
            if (existingUserTC != null)
            {
                throw new Exception("TC Kimlik numarası kullanılıyor.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                IdentityNumber = request.IdentityNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber = string.Empty,
                KYC = KYCLevel.Standart,
                CreatedDate = DateTime.UtcNow,
                IsActive = UserStatus.Active
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return new UserResponseDTO();
        }

        public async Task<UserResponseDTO> LoginAsync(LoginRequestDTO request, UserStatus IsActive)
        {
            // E-posta veya şifre hatalı
        
            if ( IsActive == UserStatus.Passive)
            {
                throw new Exception("Hesabınız aktif değil.");
            }
            return new UserResponseDTO();
        }

        public async Task<UserResponseDTO?> GetByIdAsync(Guid Id) {

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

            return new UserResponseDTO();
        }

        public Task<UserResponseDTO> UpdateKYC(UpdateKYCRequestDTO request)
        {
            throw new NotImplementedException();
        }
    }
