using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.DTOs
{
    public class CreateUserDTO
    {
        public string IdentityNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string Password { get; set; }

    }
}
