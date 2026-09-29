using DigitalWallet.Domain.Enums;


// Cüzdan Oluştur, Cüzdanları Listele, Seçili Cüzdanı Listeleme, Cüzdan aktif/pasif updateleme ve cüzdan silme endpointlerine reponse   
namespace DigitalWallet.Application.DTOs.Wallets
{
    public class WalletResponseDTO
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public Currency? Currency { get; set; }

        public WalletStatus? WalletStatus { get; set; }

        public DateTime CreatedDate { get; set; }
        public string IBAN { get; internal set; }
    }
}
