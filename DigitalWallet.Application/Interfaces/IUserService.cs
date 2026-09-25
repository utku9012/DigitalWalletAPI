using DigitalWallet.Application.DTOs.Users; // DB'deki Users nesnesini public açmak yerine DTO'dan kullanırız bu sayede core bağımlı çalışmaz.
using DigitalWallet.Domain.Enums; 

//  
namespace DigitalWallet.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO> RegisterAsync(RegisterRequestDTO request); // Task<UserResponseDto> Metod asenkrondur (async/await). İşlem bittiğinde, yeni oluşturulan kullanıcının bilgilerini içeren bir yanıt nesnesi döndürür. RegisterAsync'e ise RegisterRequestDto request parametrelerini verir.
        
        Task<UserResponseDTO> LoginAsync(LoginRequestDTO request, UserStatus IsActive);

        Task<UserResponseDTO> UpdateKYC(UpdateKYCRequestDTO request);

        Task<UserResponseDTO?> GetByIdAsync(Guid Id); 
    }
}
