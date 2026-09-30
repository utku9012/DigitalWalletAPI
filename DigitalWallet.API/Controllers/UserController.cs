using DigitalWallet.Application.DTOs.Users;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalWallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request) // [FromBody] = Gelen requestleri Url veya Formda arama, request body içinde paketi oku ve C# objectine çevir. Karmaşık nesneleri URL üzerinden göndermek hem güvensizdir hem de HTTP standartlarına aykırıdır. Bu veriler JSON formatında HTTP Body içinde gelmelidir.
        {
            var result = await _userService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request, UserStatus IsActive)
        {
            var result = await _userService.LoginAsync(request, IsActive);
            return Ok(result);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var user = await _userService.GetByIdAsync(Guid.Parse(userIdClaim));
            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            return Ok(user);
        }

        [HttpPut("me")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserRequestDTO request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var updatedUser = await _userService.UpdateUserAsync(Guid.Parse(userIdClaim), request);
            return Ok(updatedUser);
        }

        [HttpDelete("me")]
        [Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            await _userService.DeleteUserAsync(Guid.Parse(userIdClaim));
            return Ok(new { message = "Hesap başarıyla silindi." });
        }
    }
}
