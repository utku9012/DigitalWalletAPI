using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.DTOs.Users
{
    public class UserResponseDTO
    {
        internal DateTime Expiration;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedDate { get; set; }

        public Email Email { get; set; }

        public KYCLevel KYC { get; set; }

        public UserStatus IsActive { get; set; }

        public string PhoneNumber { get; internal set; }

        public string token { get; internal set; }
    }
}
