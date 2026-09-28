using DigitalWallet.Application.DTOs.Users;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


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
            //E-posta, kimlik kontrolü ve e mail format doğrulaması ValueObject içinde
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
            }; // reponse DTO'lar altında propertyler eklenmeli
        }

        public async Task<UserResponseDTO> LoginAsync(LoginRequestDTO request, UserStatus IsActive)
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

            return new UserResponseDTO
            {
                token = token,
                Expiration = DateTime.UtcNow.AddHours(1),
                FirstName = $"{user.FirstName}",
                LastName = $"{user.LastName}",
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

            };// UserResponseDTO altında token ve expire süresi dönmeli eklenecek
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
            };// reponse DTO'lar altında propertyler eklenmeli
        }

        public Task<UserResponseDTO> UpdateKYC(UpdateKYCRequestDTO request)
        {

            return new UserResponseDTO();  // reponse DTO'lar altında propertyler eklenmeli
        }
    }
}