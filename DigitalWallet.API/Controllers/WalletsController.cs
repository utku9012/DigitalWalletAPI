using DigitalWallet.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalWallet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Cüzdan işlemlerinin tümü login gerektirir
    public class WalletsController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletsController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        // Kullanıcının kendi cüzdanlarını listelemesi
        [HttpGet]
        public async Task<IActionResult> GetMyWallets()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var wallets = await _walletService.GetAllByUserIdAsync(Guid.Parse(userIdClaim));
            return Ok(wallets);
        }

        // İsme göre cüzdan getirme (örn: /api/wallets/by-name?name=TRY Cüzdanım)
        [HttpGet("by-name")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var wallet = await _walletService.GetByNameAsync(Guid.Parse(userIdClaim), name);
            if (wallet == null)
                return NotFound("Bu isimde bir cüzdan bulunamadı.");

            return Ok(wallet);
        }

        // Yeni cüzdan açma (USD, EUR vb.)
        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletRequestDTO request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            // Güvenlik: DTO içindeki UserId'yi token'dan gelen gerçek UserId ile eziyoruz
            request.UserId = Guid.Parse(userIdClaim);

            var wallet = await _walletService.CreateWalletAsync(request);
            return Ok(wallet);
        }

        // Cüzdanı Aktif/Pasif Yapma (PATCH)
        [HttpPatch("{walletId}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(Guid walletId)
        {
            var wallet = await _walletService.ToggleStatusAsync(walletId);
            return Ok(wallet);
        }

        // Cüzdan Silme
        [HttpDelete("{walletId}")]
        public async Task<IActionResult> DeleteWallet(Guid walletId)
        {
            await _walletService.DeleteAsync(walletId);
            return Ok(new { message = "Cüzdan başarıyla silindi." });
        }
    }
}
