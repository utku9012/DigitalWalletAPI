using DigitalWallet.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.DTOs.Users
{
    public class LoginRequestDTO
    {
        public Email Email { get; set; }

        public string Password { get; set; }
    }
}
