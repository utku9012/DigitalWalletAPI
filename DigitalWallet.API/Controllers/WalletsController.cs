using DigitalWallet.Application.DTOs.Wallets;
using DigitalWallet.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalWallet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // tüm cüzdan işlemleri login gerektirdiği için [Authorize] tüm controllerı kapsar.
    public class WalletsController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletsController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyWallets(Guid userId, string Name)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var wallets = await _walletService.GetAllWalletsAsync(Guid.Parse(userIdClaim), Name);
            return Ok(wallets);
        }

        [HttpGet("by-name")]
        public async Task<IActionResult> GetByName([FromQuery] string name) // 
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var wallet = await _walletService.GetWalletById(Guid.Parse(userIdClaim), name);
            if (wallet == null)
                return NotFound("Bu isimde bir cüzdan bulunamadı.");

            return Ok(wallet);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletDTO request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();


            request.UserId = Guid.Parse(userIdClaim);

            var wallet = await _walletService.CreateWalletAsync(request);
            return Ok(wallet);
        }

        [HttpPatch("{walletId}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(Guid walletId, string Name)
        {
            var wallet = await _walletService.ToggleStatusAsync(walletId, Name);
            return Ok(wallet);
        }

        [HttpDelete("{walletId}")]
        public async Task<IActionResult> DeleteWallet(Guid walletId, string Name)
        {
            await _walletService.DeleteAsync(walletId, Name);
            return Ok(new { message = "Cüzdan başarıyla silindi." });
        }
    }
}
