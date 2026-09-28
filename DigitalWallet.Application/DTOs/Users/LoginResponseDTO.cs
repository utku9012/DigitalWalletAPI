using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.DTOs.Users
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;

        public DateTime Expiration { get; set; }

        public UserResponseDTO User { get; set; } = null!;

    }
}
