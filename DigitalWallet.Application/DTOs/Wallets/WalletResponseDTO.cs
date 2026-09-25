using DigitalWallet.Domain.Enums;


// Cüzdan Oluştur, Cüzdanları Listele, Seçili Cüzdanı Listeleme, Cüzdan aktif/pasif endpointlerine reponse   
namespace DigitalWallet.Application.DTOs.Wallets
{
    public class WalletResponseDTO
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public Currency Currency { get; set; }
    }
}
