using DigitalWallet.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.DTOs.Users
{
    public class RegisterRequestDTO
    {
        public string IdentityNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Email Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string Password { get; set; }

    }
}
